using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SFSAcademy.HtmlHelpers
{
    public class LocationHelper
    {
        public static bool IsCurrentControllerAndAction(string controllerName, string actionName, ViewContext viewContext)
        {
            bool result = false;
            string normalizedControllerName = controllerName.EndsWith("Controller") ? controllerName : String.Format("{0}Controller", controllerName);

            if (viewContext == null) return false;
            if (String.IsNullOrEmpty(actionName)) return false;

            if (viewContext.Controller.GetType().Name.Equals(normalizedControllerName, StringComparison.InvariantCultureIgnoreCase) &&
                viewContext.Controller.ValueProvider.GetValue("action").AttemptedValue.Equals(actionName, StringComparison.InvariantCultureIgnoreCase))
            {
                result = true;
            }

            return result;
        }
    }

    public static class ApplicationHelper
    {
        public static void get_stylesheets(this HtmlHelper HtmlHelper, Page Page)
        {
            var link = new HtmlLink();
            link.Href = "~/styles/main.css";
            link.Attributes.Add("rel", "stylesheet");
            link.Attributes.Add("type", "text/css");
            Page.Header.Controls.Add(link);
            return;
        }

        public static bool Permitted_To(this HtmlHelper HtmlHelper, string _Action, string _Controller)
        {

            if (_Controller == null)
            {
                return false;
            }
            else if (_Action == null)
            {
                return false;
            }

            bool result = false;
            HttpContext context = HttpContext.Current;
            int UserId = Convert.ToInt32(context.Session["UserId"]);
            SFSAcademyEntities db = new SFSAcademyEntities();
            
            foreach (var entity in db.USERS_ACCESS.Select(s => new { s.USRS_ID, s.CTL, s.ACTN, s.IS_ACCBLE }).Distinct().Where(a => a.USRS_ID.Equals(UserId)).ToList())
            {
                if (entity.ACTN.ToString().Equals(_Action) && entity.CTL.ToString().Equals(_Controller) && entity.IS_ACCBLE.ToString().Equals("Y"))
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        public static string CurrentUser_UserType(this HtmlHelper HtmlHelper)
        {
            SFSAcademyEntities db = new SFSAcademyEntities();
            HttpContext context = HttpContext.Current;
            int UserId = Convert.ToInt32(context.Session["UserId"]);

            var V = db.USERS.Select(s => new { s.ID, s.ADMIN_IND, s.EMP_IND, s.STDNT_IND, s.PARNT_IND }).Distinct().Where(a => a.ID.Equals(UserId)).FirstOrDefault();
            if (V.ADMIN_IND.ToString().Equals("Y"))
            {
                return "Admin";
            }
            else if (V.EMP_IND.ToString().Equals("Y"))
            {
                return "Empoyee";
            }
            else if (V.STDNT_IND.ToString().Equals("Y"))
            {
                return "Student";
            }
            else if (V.PARNT_IND.ToString().Equals("Y"))
            {
                return "Parent";
            }
            else
            {
                return "Visitor";
            }

        }

        public static DataTable CurrentUser_SubjectBatch(this HtmlHelper HtmlHelper)
        {
            SFSAcademyEntities db = new SFSAcademyEntities();
            HttpContext context = HttpContext.Current;
            int UserId = Convert.ToInt32(context.Session["UserId"]);

            var SubjectBatchTable = new DataTable();

            SubjectBatchTable.Columns.Add("SubjectId", typeof(int));
            SubjectBatchTable.Columns.Add("BatchId", typeof(string));

            var SubjectBatch = (from USR in db.USERS
                                join EMP in db.EMPLOYEEs on USR.ID equals EMP.USRID
                                join ESUB in db.EMPLOYEES_SUBJECT on EMP.ID equals ESUB.EMP_ID
                                join SUB in db.SUBJECTs on ESUB.SUBJ_ID equals SUB.ID
                                join BA in db.BATCHes on SUB.BTCH_ID equals BA.ID
                                where USR.ID == UserId
                                select new { SUBJECT_ID = SUB.ID, BATCH_ID = BA.ID }).ToList();

            foreach (var entity in SubjectBatch.ToList())
            {
                var row = SubjectBatchTable.NewRow();
                row["SubjectId"] = entity.SUBJECT_ID;
                row["BatchId"] = entity.BATCH_ID;
                SubjectBatchTable.Rows.Add(row);
            }
            return SubjectBatchTable;
        }


        public static string Configuration_Value(this HtmlHelper HtmlHelper, string Config_Key)
        {
            SFSAcademyEntities db = new SFSAcademyEntities();

            var SubjectBatch = (from C in db.CONFIGURATIONs
                                where C.CONFIG_KEY == Config_Key
                                select new { CONFIG_VALUE = C.CONFIG_VAL }).FirstOrDefault();

            return SubjectBatch.CONFIG_VALUE.ToString();
        }

        public static int Check_Reminders(this HtmlHelper HtmlHelper)
        {

            SFSAcademyEntities db = new SFSAcademyEntities();
            HttpContext context = HttpContext.Current;
            int UserId = Convert.ToInt32(context.Session["UserId"]);
            int Count = 0;
            var SubjectBatch = (from EV in db.EVENTs
                                join EDE in db.EMPLOYEE_DEPARTMENT_EVENT on EV.ID equals EDE.EV_ID
                                join ED in db.EMPLOYEE_DEPARTMENT on EDE.EMP_DEPT_ID equals ED.ID
                                join EP in db.EMPLOYEEs on ED.ID equals EP.EMP_DEPT_ID
                                where EP.USRID == UserId
                                select new { EVENT_ID = EV.ID }).ToList();

            foreach (var entity in SubjectBatch.ToList())
            {
                Count = Count + 1;
            }
            return Count;
        }

        public static DataTable AutosuggestMenu(this HtmlHelper HtmlHelper)
        {
            var AutoSuggestMenu = new DataTable();
            AutoSuggestMenu.Columns.Add("HyperLink", typeof(string));

            var row = AutoSuggestMenu.NewRow();
            row["HyperLink"] = "<a href=\"@Url.Action(\"Admission1\", \"Student\")\"\">Student Admission</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Admission1\", \"Employee\")\"\">Employee Admission</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Index\", \"Exam\")\"\">Exam</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Index\", \"Grading_Levels\")\"\">Set Grading Levels</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Create_Exam\", \"Exam\")\"\">Exam Management</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Create_Additional_Exam\", \"Additional_Exam\")\"\">Additional_Exams</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Exam_Wise_Report\", \"Exam\")\"\">Exam_Wise_Report</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Subject_Wise_Report\", \"Exam\")\"\">Subject Wise Report</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Grouped_Exam_Report\", \"Exam\")\"\">Grouped Exam Report</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Index\", \"News\")\"\">News</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Index\", \"Event\")\"\">Event</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"All\", \"News\")\"\">View News</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Add\", \"News\")\"\">Add News</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"HR\", \"Employee\")\"\">Employee</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Settings\", \"Employee\")\"\">Employee Settings</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Subject_Assignment\", \"Employee\")\"\">Employee Subject Association</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Employee_Attendance\", \"Employee\")\"\">Employee Leave Management</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Add_Leave_Types\", \"Employee_Attendance\")\"\">Add Leave Types</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Index\", \"Employee_Attendance\")\"\">Attendance Register</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Report\", \"Employee_Attendance\")\"\">Attendance Report</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Manual_Reset\", \"Employee_Attendance\")\"\">Reset Leave</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Payslip\", \"Employee\")\"\">Empolyee Payslip</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Department_Payslip\", \"Employee\")\"\">Department Wise Payslip</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Idex\", \"Finance\")\"\">Finance</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Fees_Index\", \"Finance\")\"\">Manage Fees</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Fee_Collection\", \"Finance\")\"\">Fee Collection</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Fees_Submission_Batch\", \"Finance\")\"\">Fee Submission By Course</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Fees_Student_Search\", \"Finance\")\"\">Fee Submission For Each Student</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Categories\", \"Finance\")\"\">Finance Categories</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Transactions\", \"Finance\")\"\">Transactions</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Expense_Create\", \"Finance\")\"\">Add Expense</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Expense_List\", \"Finance\")\"\">Expense List</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Income_Create\", \"Finance\")\"\">Add Income</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Income_List\", \"Finance\")\"\">Income List</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Monthly_Report\", \"Finance\")\"\">Transaction Report</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Compare_Report\", \"Finance\")\"\">Compare Transactions</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Donation\", \"Finance\")\"\">Donation</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Donors\", \"Finance\")\"\">Donors</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Automatic_Transactions\", \"Finance\")\"\">Automatic Transactions</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"View_Monthly_Payslip\", \"Finance\")\"\">View Payslip</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Asset\", \"Finance\")\"\">Asset</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"View_Asset\", \"Finance\")\"\">View Asset</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Liability\", \"Finance\")\"\">Liability</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"View_Liability\", \"Finance\")\"\">View Liability</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Index\", \"User\")\"\">Manage Users</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"All\", \"User\")\"\">View Users</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Create\", \"User\")\"\">Add_Users</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Index\", \"Timetable\")\"\">Timetable</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Index\", \"Timetable\")\"\">Timetable</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Select_Class2\", \"Timetable\")\"\">Create Timetable</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Index\", \"Class_Timings\")\"\">Set Class Timings</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"View\", \"Timetable\")\"\">View Timetables</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Timetable\", \"Timetable\")\"\">Institutional Timetable</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Index\", \"Weekday\")\"\">Create Weekdays</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Index\", \"Configuration\")\"\">Settings</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Add_Additional_Details\", \"Student\")\"\">Add Admission Additional Detail</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Index\", \"Student_Attendance\")\"\">Student Attendance</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Index\", \"Attendances\")\"\">Attendance Register</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Index\", \"Attendance_Reports\")\"\">Attendance Reports</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"View_All\", \"Student\")\"\">View Students</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Advanced_Search\", \"Student\")\"\">Student Advanced Search</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Master_Fees\", \"Finance\")\"\">Create Fees</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Fees_Defaulters\", \"Finance\")\"\">Fee Defaulters</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Fees_Student_Structure_Search\", \"Finance\")\"\">Fee Structure</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Index\", \"Reminder\")\"\">Messages</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Sent_Reminder\", \"Reminder\")\"\">Sent Messages</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Change_Password\", \"User\")\"\">Change_Password</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"Advanced_Search\", \"Employee\")\"\">Employee Advanced Search</a>";
            AutoSuggestMenu.Rows.Add(row);
            row["HyperLink"] = "<a href=\"@Url.Action(\"View_All\", \"Employee\")\"\">View Employees</a>";
            AutoSuggestMenu.Rows.Add(row);

            return AutoSuggestMenu;
        }

    }

    public class SHA1
    {
        public static string Encode(string value)
        {
            var hash = System.Security.Cryptography.SHA1.Create();
            var encoder = new System.Text.ASCIIEncoding();
            var combined = encoder.GetBytes(value ?? "");
            return BitConverter.ToString(hash.ComputeHash(combined)).ToLower().Replace("-", "");
        }
    }


}