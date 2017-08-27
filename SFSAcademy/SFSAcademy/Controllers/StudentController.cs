using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;
using System.Web.UI.WebControls;
using System;
using SFSAcademy.Models;
using SFSAcademy.HtmlHelpers;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.IO;

namespace SFSAcademy.Controllers
{
    public class StudentController : Controller
    {
        private SFSAcademyEntities db = new SFSAcademyEntities();

        // GET: Student
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var StudentS = (from st in db.STUDENTs
                            join b in db.BATCHes on st.BTCH_ID equals b.ID
                            orderby st.LAST_NAME, b.NAME
                            select new Models.Student { StudentData = st, BatcheData = b }).Distinct();

            if (!String.IsNullOrEmpty(searchString))
            {
                StudentS = StudentS.Where(s => s.StudentData.LAST_NAME.Contains(searchString)
                                       || s.StudentData.FIRST_NAME.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    StudentS = StudentS.OrderByDescending(s => s.StudentData.LAST_NAME);
                    break;
                case "Date":
                    StudentS = StudentS.OrderBy(s => s.StudentData.ADMSN_DATE);
                    break;
                case "date_desc":
                    StudentS = StudentS.OrderByDescending(s => s.StudentData.ADMSN_DATE);
                    break;
                default:  // Name ascending 
                    StudentS = StudentS.OrderBy(s => s.StudentData.LAST_NAME);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(StudentS.ToPagedList(pageNumber, pageSize));
            //return View(db.USERS.ToList());
        }


        // GET: Student
        public ActionResult ViewAll()
        {

            return View(db.BATCHes.ToList());
        }

        // GET: Student
        public ActionResult _ListStudentsByCourse(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
                ///As Drop down list sends Id, we will ahve to convert this to text which is different from text box
                int searchStringId = Convert.ToInt32(searchString);
                searchString = db.BATCHes.Find(searchStringId).NAME.ToString();
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var StudentS = (from st in db.STUDENTs
                           join b in db.BATCHes on st.BTCH_ID equals b.ID
                           orderby st.LAST_NAME, b.NAME
                           select new Models.Student { StudentData = st, BatcheData = b }).Distinct();

            if (!String.IsNullOrEmpty(searchString))
            {
                StudentS = StudentS.Where(s => s.BatcheData.NAME.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    StudentS = StudentS.OrderByDescending(s => s.StudentData.LAST_NAME);
                    break;
                case "Date":
                    StudentS = StudentS.OrderBy(s => s.StudentData.ADMSN_DATE);
                    break;
                case "date_desc":
                    StudentS = StudentS.OrderByDescending(s => s.StudentData.ADMSN_DATE);
                    break;
                default:  // Name ascending 
                    StudentS = StudentS.OrderBy(s => s.StudentData.LAST_NAME);
                    break;
            }

            int pageSize = 100;
            int pageNumber = (page ?? 1);
            return View(StudentS.ToPagedList(pageNumber, pageSize));
            //return View(db.USERS.ToList());
        }

        // GET: Student/Details/5
        public ActionResult Profiles(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var StudentS = (from st in db.STUDENTs
                           join b in db.BATCHes on st.BTCH_ID equals b.ID into gi
                            from subb in gi.DefaultIfEmpty()
                            join c in db.COURSEs on subb.CRS_ID equals c.ID into gj
                            from subc in gj.DefaultIfEmpty()
                           join ct in db.COUNTRies on st.NTLTY_ID equals ct.ID into gk
                            from subct in gk.DefaultIfEmpty()
                           join cat in db.STUDENT_CATGEORY on st.STDNT_CAT_ID equals cat.ID into gl
                            from subcat in gl.DefaultIfEmpty()
                            join emp in db.EMPLOYEEs on subb.EMP_ID equals emp.ID into gm
                            from subemp in gm.DefaultIfEmpty()
                            join img in db.IMAGE_DOCUMENTS on st.PHTO_DATA equals img.DocumentId into gn
                            from subimg in gn.DefaultIfEmpty()
                            where st.ID == id
                           orderby st.LAST_NAME, subb.NAME
                           select new Models.Student { StudentData = st, BatcheData = (subb == null ? null : subb), CourseData = (subc == null ? null : subc), CountryData = (subct == null ? null : subct), CategoryData = (subcat == null ? null : subcat), EmployeeData = (subemp == null ? null : subemp), ImageData = (subimg == null ? null : subimg) }).Distinct();

            if (StudentS == null)
            {
                return HttpNotFound();
            }

            return View(StudentS.FirstOrDefault());
        }

        // GET: Student
        public ActionResult AdvancedSearch(string sortOrder, string currentFilter, string searchString, int? page, string currentFilter2, string AdmissionNumber, string currentFilter3, string Course, string currentFilter4, string CourseBatches, string currentFilter5, string Category, string currentFilter6, string StudentGender, string currentFilter7, string BloodGroup, string currentFilter8, string StudentGrade, string currentFilter9, string StudentBirthFromDate, string currentFilter10, string StudentBirthToDate)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            if (AdmissionNumber != null){page = 1;}
            else { AdmissionNumber = currentFilter2;}
            ViewBag.CurrentFilter2 = AdmissionNumber;
            if (Course != null) { page = 1; }
            else { Course = currentFilter3; }
            ViewBag.CurrentFilter3 = Course;
            if (CourseBatches != null) { page = 1; }
            else { CourseBatches = currentFilter4; }
            ViewBag.CurrentFilter4 = CourseBatches;
            if (Category != null) { page = 1; }
            else { Category = currentFilter5; }
            ViewBag.CurrentFilter5 = Category;
            if (StudentGender != null) { page = 1; }
            else { StudentGender = currentFilter6; }
            ViewBag.CurrentFilter6 = StudentGender;
            if (BloodGroup != null) { page = 1; }
            else { BloodGroup = currentFilter7; }
            ViewBag.CurrentFilter7 = BloodGroup;
            if (StudentGrade != null) { page = 1; }
            else { StudentGrade = currentFilter8; }
            ViewBag.CurrentFilter8 = StudentGrade;
            if (StudentBirthFromDate != null)
            {
                page = 1;
            }
            else { StudentBirthFromDate = currentFilter9; }
            DateTime? dFrom; DateTime dtFrom;
            dFrom = DateTime.TryParse(StudentBirthFromDate, out dtFrom) ? dtFrom : (DateTime?)null;
            ViewBag.CurrentFilter9 = StudentBirthFromDate;
            if (StudentBirthToDate != null)
            {
                page = 1;
            }
            else { StudentBirthToDate = currentFilter10; }
            DateTime? dTo; DateTime dtTo;
            dTo = DateTime.TryParse(StudentBirthFromDate, out dtTo) ? dtTo : (DateTime?)null;
            ViewBag.CurrentFilter10 = StudentBirthToDate;

            var StudentS = (from st in db.STUDENTs
                            join b in db.BATCHes on st.BTCH_ID equals b.ID into gi
                            from subb in gi.DefaultIfEmpty()
                            join c in db.COURSEs on subb.CRS_ID equals c.ID into gj
                            from subc in gj.DefaultIfEmpty()
                            join ct in db.COUNTRies on st.NTLTY_ID equals ct.ID into gk
                            from subct in gk.DefaultIfEmpty()
                            join cat in db.STUDENT_CATGEORY on st.STDNT_CAT_ID equals cat.ID into gl
                            from subcat in gl.DefaultIfEmpty()
                            join emp in db.EMPLOYEEs on subb.EMP_ID equals emp.ID into gm
                            from subemp in gm.DefaultIfEmpty()
                            join esc in db.EXAM_SCORE on st.ID equals esc.STDNT_ID into gn
                            from subesc in gn.DefaultIfEmpty()
                            join grl in db.GRADING_LEVEL on subesc.GRADING_LVL_ID equals grl.ID into go
                            from subgrl in go.DefaultIfEmpty()
                            orderby st.LAST_NAME, subb.NAME
                            select new Models.Student { StudentData = st, BatcheData = (subb == null ? null : subb), CourseData = (subc == null ? null : subc), CountryData = (subct == null ? null : subct), CategoryData = (subcat == null ? null : subcat), EmployeeData = (subemp == null ? null : subemp), GradeData = (subgrl == null ? null : subgrl) }).Distinct();

            if (!String.IsNullOrEmpty(searchString))
            {
                StudentS = StudentS.Where(s => s.StudentData.LAST_NAME.Contains(searchString)
                                       || s.StudentData.FIRST_NAME.Contains(searchString));
            }
            if (!String.IsNullOrEmpty(AdmissionNumber))
            {
                StudentS = StudentS.Where(s => s.StudentData.ADMSN_NO.Equals(AdmissionNumber));
            }
            if (!String.IsNullOrEmpty(Course))
            {
                StudentS = StudentS.Where(s => s.CourseData.CRS_NAME.Contains(Course));
            }
            if (!String.IsNullOrEmpty(CourseBatches))
            {
                StudentS = StudentS.Where(s => s.BatcheData.NAME.Contains(CourseBatches));
            }
            if (!String.IsNullOrEmpty(Category))
            {
                StudentS = StudentS.Where(s => s.CategoryData.NAME.Contains(Category));
            }
            if (!String.IsNullOrEmpty(StudentGender))
            {
                StudentS = StudentS.Where(s => s.StudentData.GNDR.Equals(StudentGender));
            }
            if (!String.IsNullOrEmpty(BloodGroup))
            {
                StudentS = StudentS.Where(s => s.StudentData.BLOOD_GRP.Equals(BloodGroup));
            }
            if (!String.IsNullOrEmpty(StudentGrade))
            {
                StudentS = StudentS.Where(s => s.GradeData.NAME.Equals(StudentGrade));
            }
            if (!String.IsNullOrEmpty(StudentBirthFromDate) && !String.IsNullOrEmpty(StudentBirthToDate))
            {
                StudentS = StudentS.Where(s => s.StudentData.ADMSN_DATE >= dFrom).Where(s => s.StudentData.ADMSN_DATE <= dTo);
            }
            switch (sortOrder)
            {
                case "name_desc":
                    StudentS = StudentS.OrderByDescending(s => s.StudentData.LAST_NAME);
                    break;
                case "Date":
                    StudentS = StudentS.OrderBy(s => s.StudentData.ADMSN_DATE);
                    break;
                case "date_desc":
                    StudentS = StudentS.OrderByDescending(s => s.StudentData.ADMSN_DATE);
                    break;
                default:  // Name ascending 
                    StudentS = StudentS.OrderBy(s => s.StudentData.LAST_NAME);
                    break;
            }

            int pageSize = 100;
            int pageNumber = (page ?? 1);
            return View(StudentS.ToPagedList(pageNumber, pageSize));
            //return View(db.USERS.ToList());
        }

        // GET: Student
        [HttpGet]
        public void AdvancedSearchPdf(string sortOrder, string currentFilter, string searchString, int? page, string currentFilter2, string AdmissionNumber, string currentFilter3, string Course, string currentFilter4, string CourseBatches, string currentFilter5, string Category, string currentFilter6, string StudentGender, string currentFilter7, string BloodGroup, string currentFilter8, string StudentGrade, string currentFilter9, string StudentBirthFromDate, string currentFilter10, string StudentBirthToDate)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            if (AdmissionNumber != null) { page = 1; }
            else { AdmissionNumber = currentFilter2; }
            ViewBag.CurrentFilter2 = AdmissionNumber;
            if (Course != null) { page = 1; }
            else { Course = currentFilter3; }
            ViewBag.CurrentFilter3 = Course;
            if (CourseBatches != null) { page = 1; }
            else { CourseBatches = currentFilter4; }
            ViewBag.CurrentFilter4 = CourseBatches;
            if (Category != null) { page = 1; }
            else { Category = currentFilter5; }
            ViewBag.CurrentFilter5 = Category;
            if (StudentGender != null) { page = 1; }
            else { StudentGender = currentFilter6; }
            ViewBag.CurrentFilter6 = StudentGender;
            if (BloodGroup != null) { page = 1; }
            else { BloodGroup = currentFilter7; }
            ViewBag.CurrentFilter7 = BloodGroup;
            if (StudentGrade != null) { page = 1; }
            else { StudentGrade = currentFilter8; }
            ViewBag.CurrentFilter8 = StudentGrade;
            if (StudentBirthFromDate != null)
            {
                page = 1;
            }
            else { StudentBirthFromDate = currentFilter9; }
            DateTime? dFrom; DateTime dtFrom;
            dFrom = DateTime.TryParse(StudentBirthFromDate, out dtFrom) ? dtFrom : (DateTime?)null;
            ViewBag.CurrentFilter9 = StudentBirthFromDate;
            if (StudentBirthToDate != null)
            {
                page = 1;
            }
            else { StudentBirthToDate = currentFilter10; }
            DateTime? dTo; DateTime dtTo;
            dTo = DateTime.TryParse(StudentBirthFromDate, out dtTo) ? dtTo : (DateTime?)null;
            ViewBag.CurrentFilter10 = StudentBirthToDate;

            var StudentS = (from st in db.STUDENTs
                            join b in db.BATCHes on st.BTCH_ID equals b.ID into gi
                            from subb in gi.DefaultIfEmpty()
                            join c in db.COURSEs on subb.CRS_ID equals c.ID into gj
                            from subc in gj.DefaultIfEmpty()
                            join ct in db.COUNTRies on st.NTLTY_ID equals ct.ID into gk
                            from subct in gk.DefaultIfEmpty()
                            join cat in db.STUDENT_CATGEORY on st.STDNT_CAT_ID equals cat.ID into gl
                            from subcat in gl.DefaultIfEmpty()
                            join emp in db.EMPLOYEEs on subb.EMP_ID equals emp.ID into gm
                            from subemp in gm.DefaultIfEmpty()
                            join esc in db.EXAM_SCORE on st.ID equals esc.STDNT_ID into gn
                            from subesc in gn.DefaultIfEmpty()
                            join grl in db.GRADING_LEVEL on subesc.GRADING_LVL_ID equals grl.ID into go
                            from subgrl in go.DefaultIfEmpty()
                            orderby st.LAST_NAME, subb.NAME
                            select new Models.Student { StudentData = st, BatcheData = (subb == null ? null : subb), CourseData = (subc == null ? null : subc), CountryData = (subct == null ? null : subct), CategoryData = (subcat == null ? null : subcat), EmployeeData = (subemp == null ? null : subemp), GradeData = (subgrl == null ? null : subgrl) }).Distinct();

            if (!String.IsNullOrEmpty(searchString))
            {
                StudentS = StudentS.Where(s => s.StudentData.LAST_NAME.Contains(searchString)
                                       || s.StudentData.FIRST_NAME.Contains(searchString));
            }
            if (!String.IsNullOrEmpty(AdmissionNumber))
            {
                StudentS = StudentS.Where(s => s.StudentData.ADMSN_NO.Equals(AdmissionNumber));
            }
            if (!String.IsNullOrEmpty(Course))
            {
                StudentS = StudentS.Where(s => s.CourseData.CRS_NAME.Contains(Course));
            }
            if (!String.IsNullOrEmpty(CourseBatches))
            {
                StudentS = StudentS.Where(s => s.BatcheData.NAME.Contains(CourseBatches));
            }
            if (!String.IsNullOrEmpty(Category))
            {
                StudentS = StudentS.Where(s => s.CategoryData.NAME.Contains(Category));
            }
            if (!String.IsNullOrEmpty(StudentGender))
            {
                StudentS = StudentS.Where(s => s.StudentData.GNDR.Equals(StudentGender));
            }
            if (!String.IsNullOrEmpty(BloodGroup))
            {
                StudentS = StudentS.Where(s => s.StudentData.BLOOD_GRP.Equals(BloodGroup));
            }
            if (!String.IsNullOrEmpty(StudentGrade))
            {
                StudentS = StudentS.Where(s => s.GradeData.NAME.Equals(StudentGrade));
            }
            if (!String.IsNullOrEmpty(StudentBirthFromDate) && !String.IsNullOrEmpty(StudentBirthToDate))
            {
                StudentS = StudentS.Where(s => s.StudentData.ADMSN_DATE >= dFrom).Where(s => s.StudentData.ADMSN_DATE <= dTo);
            }
            switch (sortOrder)
            {
                case "name_desc":
                    StudentS = StudentS.OrderByDescending(s => s.StudentData.LAST_NAME);
                    break;
                case "Date":
                    StudentS = StudentS.OrderBy(s => s.StudentData.ADMSN_DATE);
                    break;
                case "date_desc":
                    StudentS = StudentS.OrderByDescending(s => s.StudentData.ADMSN_DATE);
                    break;
                default:  // Name ascending 
                    StudentS = StudentS.OrderBy(s => s.StudentData.LAST_NAME);
                    break;
            }

            var PdfStudentS = (from res in StudentS
                            select new {LName = res.StudentData.LAST_NAME, FName = res.StudentData.FIRST_NAME, Batch = res.BatcheData.NAME, AdDate = res.StudentData.ADMSN_DATE, AdNumber = res.StudentData.ADMSN_NO }).ToList();
           

            var configuration = new ReportConfiguration();
            //configuration.PageOrientation = PageSize.LETTER_LANDSCAPE.Rotate();
            configuration.LogoPath
                = Server.MapPath(Url.Content("~/Content/images/login/SF_Square_Logo-Small.jpg"));
            configuration.LogImageScalePercent = 50;
            configuration.ReportTitle
                = "S. F. Square Academy Student Report";
            configuration.ReportSubTitle = "Result of Advanced Search";

            var report = new PdfTabularReport();
            report.ReportConfiguration = configuration;

            List<ReportColumn> columns = new List<ReportColumn>();
            columns.Add(new ReportColumn { ColumnName = "Sl. No.", Width = 100 });
            columns.Add(new ReportColumn { ColumnName = "Last Name", Width = 100 });
            columns.Add(new ReportColumn { ColumnName = "First Name", Width = 100 });
            columns.Add(new ReportColumn { ColumnName = "Batch", Width = 100 });
            columns.Add(new ReportColumn { ColumnName = "Admission Date", Width = 100 });
            columns.Add(new ReportColumn { ColumnName = "Admission Number", Width = 100 });

            var PdfStudentSI = new DataTable();

            PdfStudentSI.Columns.Add("Sl. No.", typeof(int));
            PdfStudentSI.Columns.Add("Last Name", typeof(string));
            PdfStudentSI.Columns.Add("First Name", typeof(string));
            PdfStudentSI.Columns.Add("Batch", typeof(string));
            PdfStudentSI.Columns.Add("Admission Date", typeof(string));
            PdfStudentSI.Columns.Add("Admission Number", typeof(string));

            int i = 1;
            foreach (var entity in PdfStudentS.ToList())
            {
                var row = PdfStudentSI.NewRow();
                row["Sl. No."] = i;
                row["Last Name"] = entity.LName;
                row["First Name"] = entity.FName;
                row["Batch"] = entity.Batch;
                row["Admission Date"] = entity.AdDate;
                row["Admission Number"] = entity.AdNumber;
                PdfStudentSI.Rows.Add(row);
                i = i + 1;
            }


            var stream = report.GetPdf(PdfStudentSI, columns);

            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition",
                "attachment;filename=ExampleReport.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(stream.ToArray());
            Response.End();

        }



        // GET: Student/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STUDENT sTUDENT = db.STUDENTs.Find(id);
            if (sTUDENT == null)
            {
                return HttpNotFound();
            }
            return View(sTUDENT);
        }

        //ViewBag.USRID = new SelectList(db.USERS, "ID", "USRNAME");
        // GET: Student/Admission1
        public ActionResult Admission1()
        {
            ViewBag.ReturnDate = System.DateTime.Now;
            ViewBag.CTRY_ID = new SelectList(db.COUNTRies, "ID", "CTRY_NAME", "India");
            ViewBag.NTLTY_ID = new SelectList(db.COUNTRies.Where(o => o.NTLTY != " ").ToList(), "ID", "NTLTY","Indian");
            ViewBag.STDNT_CAT_ID = new SelectList(db.STUDENT_CATGEORY, "ID", "NAME");
            ViewBag.BTCH_ID = new SelectList(db.BATCHes, "ID", "NAME");
            var configValue = (from C in db.CONFIGURATIONs
                                where C.CONFIG_KEY == "AdmissionNumberAutoIncrement"
                                select new { CONFIG_VALUE = C.CONFIG_VAL }).FirstOrDefault();
            int NewAdmissionNumberNum = Convert.ToInt32(configValue.CONFIG_VALUE.ToString()) + 1;
            ViewBag.NewAdmissionNumber = NewAdmissionNumberNum.ToString();
            return View();
        }

        // POST: Student/Admission1
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Admission1([Bind(Include = "ID,ADMSN_NO,CLS_ROLL_NO,ADMSN_DATE,FIRST_NAME,MID_NAME,LAST_NAME,BTCH_ID,DOB,GNDR,BLOOD_GRP,BIRTH_PLACE,LANG,RLGN,ADDR_LINE1,ADDR_LINE2,CITY,STATE,PIN_CODE,CTRY_ID,PH1,PH2,EML,IMMDT_CNTCT_ID,IS_SMS_ENABL,PHTO_FILENAME,PHTO_CNTNT_TYPE,PHTO_DATA,STAT_DESCR,IS_ACT,IS_DEL,CREATED_AT,UPDATED_AT,HAS_PD_FE,PHTO_FILE_SIZE,USRID,STDNT_CAT_ID,NTLTY_ID")] STUDENT sTUDENT, HttpPostedFileBase[] PHTO_FILENAME, string selectedTags)
        {
            int UserId = Convert.ToInt32(this.Session["UserId"]);
            sTUDENT.USRID = UserId;
            if (ModelState.IsValid)
            {
                /////Picture Upload Code
                string FileName = null;
                SuccessModel viewModel = new SuccessModel();
                if (Request.Files.Count == 1)
                {
                    var name = Request.Files[0].FileName;
                    var size = Request.Files[0].ContentLength;
                    var type = Request.Files[0].ContentType;
                    FileName = name;
                    viewModel.Success = HandleUpload(Request.Files[0].InputStream, name, size, type);
                    var PicDocumentId = (from C in db.IMAGE_DOCUMENTS
                                         where C.Name == FileName
                                         select C.DocumentId).FirstOrDefault().ToString();

                    sTUDENT.PHTO_DATA = Convert.ToInt32(PicDocumentId);
                    sTUDENT.PHTO_FILENAME = null;
                    sTUDENT.PHTO_CNTNT_TYPE = null;
                }
                else
                {
                    sTUDENT.PHTO_DATA = 1;
                    sTUDENT.PHTO_FILENAME = null;
                    sTUDENT.PHTO_CNTNT_TYPE = null;
                }
                ////End to Picture Upload Code


                try
                {
                    db.STUDENTs.Add(sTUDENT);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }


                var result = from u in db.CONFIGURATIONs where (u.CONFIG_KEY == "AdmissionNumberAutoIncrement") select u;
                if (result.Count() != 0)
                {
                    var dbConfig = result.First();

                    dbConfig.CONFIG_VAL = sTUDENT.ADMSN_NO;
                    db.SaveChanges();
                }

                string FullName = string.Concat(sTUDENT.FIRST_NAME, sTUDENT.LAST_NAME);
                var StdUser = new USER() { USRNAME= FullName, FIRST_NAME= sTUDENT.FIRST_NAME, LAST_NAME= sTUDENT.LAST_NAME, EML= sTUDENT.EML, ADMIN_IND="N", STDNT_IND="Y", EMP_IND="N", HASHED_PSWRD=string.Concat(sTUDENT.ADMSN_NO, 123), SALT="N", RST_PSWRD_CODE=null, RST_PSWRD_CODE_UNTL=null, CREATED_AT=System.DateTime.Now, UPDATED_AT=System.DateTime.Now, PARNT_IND="N" };
                db.USERS.Add(StdUser);
                db.SaveChanges();
                foreach (var entity in db.USERS_ACCESS.Select(s => new { s.USRS_ID, s.LIST_ITEM, s.LVL_1_MENU, s.LVL_2_MENU, s.CTL, s.ACTN, s.IS_ACCBLE }).Distinct().Where(a => a.USRS_ID.Equals(4)).ToList())
                {
                    var UserAccess = new USERS_ACCESS() { USRS_ID = StdUser.ID, LIST_ITEM = entity.LIST_ITEM, LVL_1_MENU = entity.LVL_1_MENU, LVL_2_MENU = entity.LVL_2_MENU, CTL = entity.CTL, ACTN = entity.ACTN, IS_ACCBLE = entity.IS_ACCBLE };
                    db.USERS_ACCESS.Add(UserAccess);
                    db.SaveChanges();
                }

                var StdResult = from u in db.STUDENTs where (u.ADMSN_NO == sTUDENT.ADMSN_NO) select u;
                if (StdResult.Count() != 0)
                {
                    //var StdRecord = StdResult.First();
                    var StdUserFinal = from u in db.USERS where (u.USRNAME == FullName) select u;

                    StdResult.First().USRID = StdUserFinal.First().ID;
                    db.SaveChanges();
                }
                // some code 
                TempData["alertMessage"] = string.Concat("Student Admission Done. Website User ID :", FullName, "     Password :", string.Concat(sTUDENT.ADMSN_NO, 123));
                return RedirectToAction("Admission2", "Student", new { Std_id = sTUDENT.ID }); 
            }
            ViewBag.ReturnDate = System.DateTime.Now;
            ViewBag.CTRY_ID = new SelectList(db.COUNTRies, "ID", "CTRY_NAME");
            ViewBag.NTLTY_ID = new SelectList(db.COUNTRies, "ID", "CTRY_NAME", sTUDENT.NTLTY_ID);
            ViewBag.STDNT_CAT_ID = new SelectList(db.STUDENT_CATGEORY, "ID", "NAME", sTUDENT.STDNT_CAT_ID);
            ViewBag.NewAdmissionNumber = sTUDENT.ADMSN_NO;
            return View(sTUDENT);
        }

        //ViewBag.USRID = new SelectList(db.USERS, "ID", "USRNAME");
        // GET: Student/Admission1
        public ActionResult Admission2(int? Std_id)
        {
            ViewBag.ReturnDate = System.DateTime.Now;
            STUDENT sTUDENT = db.STUDENTs.Find(Std_id);
            ViewBag.StudentFullName = String.Format("{0} {1}", sTUDENT.FIRST_NAME.ToString(), sTUDENT.LAST_NAME.ToString());
            ViewBag.NewAdmissionNumber = sTUDENT.ADMSN_NO.ToString();
            if(!String.IsNullOrEmpty(sTUDENT.ADDR_LINE1))
            {
                ViewBag.AddressLine1 = sTUDENT.ADDR_LINE1.ToString();
            }
            if (!String.IsNullOrEmpty(sTUDENT.ADDR_LINE2))
            {
                ViewBag.AddressLine2 = sTUDENT.ADDR_LINE2.ToString();
            }
            if (!String.IsNullOrEmpty(sTUDENT.CITY))
            {
                ViewBag.StudentCity = sTUDENT.CITY.ToString();
            }
            if (!String.IsNullOrEmpty(sTUDENT.STATE))
            {
                ViewBag.StudentState = sTUDENT.STATE.ToString();
            }
            if (!sTUDENT.COUNTRY.Equals(null))
            {
                ViewBag.CountryName = sTUDENT.COUNTRY.ToString();
            }
            ViewBag.StudentId = Std_id;
            COUNTRY cOUNTRY = db.COUNTRies.Find(sTUDENT.CTRY_ID);
            ViewBag.CountryName = cOUNTRY.CTRY_NAME;
            ViewBag.CTRY_ID = new SelectList(db.COUNTRies, "ID", "CTRY_NAME");
            DataTable dtGuardian = new DataTable();
            dtGuardian.Columns.Add("FIRST_NAME", typeof(string));
            dtGuardian.Columns.Add("LAST_NAME", typeof(string));
            dtGuardian.Columns.Add("REL", typeof(string));
            var GuardianVal = (from C in db.GUARDIANs
                               where C.WARD_ID == Std_id
                               select new { f_name = C.FIRST_NAME, l_name = C.LAST_NAME, rel=C.REL }).ToList();

            foreach (var entity in GuardianVal.ToList())
            {
                var row = dtGuardian.NewRow();
                row["FIRST_NAME"] = entity.f_name;
                row["LAST_NAME"] = entity.l_name;
                row["REL"] = entity.rel;
                dtGuardian.Rows.Add(row);
            }
            ViewBag.Data = dtGuardian.AsEnumerable();
            return View();
        }

        // POST: Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Admission2([Bind(Include = "ID,WARD_ID,FIRST_NAME,LAST_NAME,REL,EML,OFF_PH1,OFF_PH2,MOBL_PH,OFF_ADDR_LINE1,OFF_ADDR_LINE2,CITY,STATE,CTRY_ID,DOB,OCCP,INCM,ED,CREATED_AT,UPDATED_AT,USRID")] GUARDIAN gUARDIAN)
        {
            if (ModelState.IsValid)
            {
                db.GUARDIANs.Add(gUARDIAN);
                db.SaveChanges();
                return RedirectToAction("Admission2", "Student", new { Std_id = gUARDIAN.WARD_ID });
            }

            ViewBag.ReturnDate = System.DateTime.Now;
            STUDENT sTUDENT = db.STUDENTs.Find(gUARDIAN.WARD_ID);
            ViewBag.StudentFullName = String.Format("{0} {1} {2}", sTUDENT.FIRST_NAME.ToString(), sTUDENT.MID_NAME.ToString(), sTUDENT.LAST_NAME.ToString());
            ViewBag.NewAdmissionNumber = sTUDENT.ADMSN_NO.ToString();
            ViewBag.AddressLine1 = sTUDENT.ADDR_LINE1.ToString();
            ViewBag.AddressLine2 = sTUDENT.ADDR_LINE2.ToString();
            ViewBag.StudentCity = sTUDENT.CITY.ToString();
            ViewBag.StudentState = sTUDENT.STATE.ToString();
            ViewBag.StudentCountry = sTUDENT.COUNTRY.ToString();
            ViewBag.StudentId = gUARDIAN.WARD_ID;
            COUNTRY cOUNTRY = db.COUNTRies.Find(sTUDENT.CTRY_ID);
            ViewBag.CountryName = cOUNTRY.CTRY_NAME;
            ViewBag.CTRY_ID = new SelectList(db.COUNTRies, "ID", "CTRY_NAME");
            DataTable dtGuardian = new DataTable();
            dtGuardian.Columns.Add("FIRST_NAME", typeof(int));
            dtGuardian.Columns.Add("LAST_NAME", typeof(string));
            dtGuardian.Columns.Add("REL", typeof(string));
            var GuardianVal = (from C in db.GUARDIANs
                               where C.WARD_ID == gUARDIAN.WARD_ID
                               select new { f_name = C.FIRST_NAME, l_name = C.LAST_NAME, rel = C.REL }).ToList();

            foreach (var entity in GuardianVal.ToList())
            {
                var row = dtGuardian.NewRow();
                row["FIRST_NAME"] = entity.f_name;
                row["LAST_NAME"] = entity.l_name;
                row["REL"] = entity.rel;
                dtGuardian.Rows.Add(row);
            }
            ViewBag.Data = dtGuardian.AsEnumerable();
            return View();
        }


        // GET: Student/Admission3
        public ActionResult Admission3(int? Std_id)
        {
            //List<SelectGuardian> Guardian = new List<SelectGuardian>();

            var GuardianVal = (from C in db.GUARDIANs
                               where C.WARD_ID == Std_id
                               select new Models.SelectGuardian { GuardianList = C }).ToList();

            return View(GuardianVal);

        }

        // POST: Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // GET: Student/Admission3
        public ActionResult Admission3(IList<SelectGuardian> model)
        {
            foreach (SelectGuardian item in model)
            {
                if (item.Selected)
                {
                    STUDENT sTUDENT = db.STUDENTs.Find(item.GuardianList.WARD_ID);
                    sTUDENT.IMMDT_CNTCT_ID=item.GuardianList.ID;
                    db.SaveChanges();
                    break;
                }
            }
            return RedirectToAction("Index", "Student");

        }

        // GET: Student/Create
        public ActionResult Create()
        {
            ViewBag.NTLTY_ID = new SelectList(db.COUNTRies, "ID", "CTRY_NAME");
            ViewBag.STDNT_CAT_ID = new SelectList(db.STUDENT_CATGEORY, "ID", "NAME");
            ViewBag.USRID = new SelectList(db.USERS, "ID", "USRNAME");
            return View();
        }

        // POST: Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ADMSN_NO,CLS_ROLL_NO,ADMSN_DATE,FIRST_NAME,MID_NAME,LAST_NAME,BTCH_ID,DOB,GNDR,BLOOD_GRP,BIRTH_PLACE,LANG,RLGN,ADDR_LINE1,ADDR_LINE2,CITY,STATE,PIN_CODE,CTRY_ID,PH1,PH2,EML,IMMDT_CNTCT_ID,IS_SMS_ENABL,PHTO_FILENAME,PHTO_CNTNT_TYPE,PHTO_DATA,STAT_DESCR,IS_ACT,IS_DEL,CREATED_AT,UPDATED_AT,HAS_PD_FE,PHTO_FILE_SIZE,USRID,STDNT_CAT_ID,NTLTY_ID")] STUDENT sTUDENT)
        {
            if (ModelState.IsValid)
            {
                db.STUDENTs.Add(sTUDENT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.NTLTY_ID = new SelectList(db.COUNTRies, "ID", "CTRY_NAME", sTUDENT.NTLTY_ID);
            ViewBag.STDNT_CAT_ID = new SelectList(db.STUDENT_CATGEORY, "ID", "NAME", sTUDENT.STDNT_CAT_ID);
            ViewBag.USRID = new SelectList(db.USERS, "ID", "USRNAME", sTUDENT.USRID);
            return View(sTUDENT);
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STUDENT sTUDENT = db.STUDENTs.Find(id);
            if (sTUDENT == null)
            {
                return HttpNotFound();
            }
            ViewBag.NTLTY_ID = new SelectList(db.COUNTRies, "ID", "CTRY_NAME", sTUDENT.NTLTY_ID);
            ViewBag.STDNT_CAT_ID = new SelectList(db.STUDENT_CATGEORY, "ID", "NAME", sTUDENT.STDNT_CAT_ID);
            ViewBag.USRID = new SelectList(db.USERS, "ID", "USRNAME", sTUDENT.USRID);
            return View(sTUDENT);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ADMSN_NO,CLS_ROLL_NO,ADMSN_DATE,FIRST_NAME,MID_NAME,LAST_NAME,BTCH_ID,DOB,GNDR,BLOOD_GRP,BIRTH_PLACE,LANG,RLGN,ADDR_LINE1,ADDR_LINE2,CITY,STATE,PIN_CODE,CTRY_ID,PH1,PH2,EML,IMMDT_CNTCT_ID,IS_SMS_ENABL,PHTO_FILENAME,PHTO_CNTNT_TYPE,PHTO_DATA,STAT_DESCR,IS_ACT,IS_DEL,CREATED_AT,UPDATED_AT,HAS_PD_FE,PHTO_FILE_SIZE,USRID,STDNT_CAT_ID,NTLTY_ID")] STUDENT sTUDENT)
        {
            if (ModelState.IsValid)
            {

                /////Picture Upload Code
                string FileName = null;
                SuccessModel viewModel = new SuccessModel();
                if (Request.Files.Count == 1)
                {
                    var name = Request.Files[0].FileName;
                    var size = Request.Files[0].ContentLength;
                    var type = Request.Files[0].ContentType;
                    FileName = name;
                    viewModel.Success = HandleUpload(Request.Files[0].InputStream, name, size, type);
                    var PicDocumentId = (from C in db.IMAGE_DOCUMENTS
                                         where C.Name == FileName
                                         select C.DocumentId).FirstOrDefault().ToString();

                    sTUDENT.PHTO_DATA = Convert.ToInt32(PicDocumentId);
                    sTUDENT.PHTO_FILENAME = null;
                    sTUDENT.PHTO_CNTNT_TYPE = null;
                }
                else
                {
                    sTUDENT.PHTO_DATA = 1;
                    sTUDENT.PHTO_FILENAME = null;
                    sTUDENT.PHTO_CNTNT_TYPE = null;
                }
                ////End to Picture Upload Code


                db.Entry(sTUDENT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.NTLTY_ID = new SelectList(db.COUNTRies, "ID", "CTRY_NAME", sTUDENT.NTLTY_ID);
            ViewBag.STDNT_CAT_ID = new SelectList(db.STUDENT_CATGEORY, "ID", "NAME", sTUDENT.STDNT_CAT_ID);
            ViewBag.USRID = new SelectList(db.USERS, "ID", "USRNAME", sTUDENT.USRID);
            return View(sTUDENT);
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STUDENT sTUDENT = db.STUDENTs.Find(id);
            if (sTUDENT == null)
            {
                return HttpNotFound();
            }
            return View(sTUDENT);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var query = from m in db.GUARDIANs
                        where m.WARD_ID == id
                        select m;
            foreach (var entity in query.ToList())
            {
                db.GUARDIANs.Remove(entity);
                db.SaveChanges();

            } 
            STUDENT sTUDENT = db.STUDENTs.Find(id);
            db.STUDENTs.Remove(sTUDENT);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: Student/Details/5
        public ActionResult Categories()
        {
            return View();
        }

        // GET: Student/Details/5
        [ChildActionOnly]
        public ActionResult _CategoriesList()
        {
            var sTUDENTcATEGORY = db.STUDENT_CATGEORY.ToList();
            return View(sTUDENTcATEGORY);
        }

        // GET: Student/Delete/5
        public ActionResult _CategoriesDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STUDENT_CATGEORY sTUDENTcATEGORY = db.STUDENT_CATGEORY.Find(id);
            if (sTUDENTcATEGORY == null)
            {
                return HttpNotFound();
            }
            return View(sTUDENTcATEGORY);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("_CategoriesDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult _CategoriesDeleteConfirmed(int id)
        {
            STUDENT_CATGEORY sTUDENTcATEGORY = db.STUDENT_CATGEORY.Find(id);
            db.STUDENT_CATGEORY.Remove(sTUDENTcATEGORY);
            db.SaveChanges();
            return RedirectToAction("Categories");
        }

        // GET: Student/Edit/5
        public ActionResult _CategoriesEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STUDENT_CATGEORY sTUDENTcATEGORY = db.STUDENT_CATGEORY.Find(id);
            if (sTUDENTcATEGORY == null)
            {
                return HttpNotFound();
            }
            return View(sTUDENTcATEGORY);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _CategoriesEdit([Bind(Include = "ID,NAME,IS_DEL")] STUDENT_CATGEORY sTUDENTcATEGORY)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sTUDENTcATEGORY).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Categories");
            }
            return View(sTUDENTcATEGORY);
        }

        // POST: Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CategoriesCreate([Bind(Include = "ID,NAME,IS_DEL")] STUDENT_CATGEORY sTUDENTcATEGORY)
        {
            if (ModelState.IsValid)
            {
                db.STUDENT_CATGEORY.Add(sTUDENTcATEGORY);
                db.SaveChanges();
                return RedirectToAction("Categories");
            }

            return View(sTUDENTcATEGORY);
        }


        /////Document Upload related methods////////////////////////////////////////////////////////////////

        [HttpGet]
        public ActionResult Show(int? id)
        {
            string mime;
            byte[] bytes = LoadImage(id.Value, out mime);
            return File(bytes, mime);
        }

        [HttpPost]
        public ActionResult Upload()
        {
            SuccessModel viewModel = new SuccessModel();
            if (Request.Files.Count == 1)
            {
                var name = Request.Files[0].FileName;
                var size = Request.Files[0].ContentLength;
                var type = Request.Files[0].ContentType;
                viewModel.Success = HandleUpload(Request.Files[0].InputStream, name, size, type);
            }
            return Json(viewModel);
        }

        private bool HandleUpload(Stream fileStream, string name, int size, string type)
        {
            bool handled = false;

            try
            {
                // Convert image to buffered stream
                var imageBufferedStream = new BufferedStream(fileStream);
                byte[] documentBytes = new byte[imageBufferedStream.Length];
                imageBufferedStream.Read(documentBytes, 0, documentBytes.Length);


                IMAGE_DOCUMENTS databaseDocument = new IMAGE_DOCUMENTS
                {
                    CreatedOn = DateTime.Now,
                    FileContent = documentBytes,
                    IsDeleted = false,
                    Name = name,
                    Size = size,
                    Type = type
                };

                db.IMAGE_DOCUMENTS.Add(databaseDocument);
                handled = (db.SaveChanges() > 0);
            }
            catch (Exception ex)
            {
                throw ex; // Oops, something went wrong, handle the exception
            }

            return handled;
        }

        private byte[] LoadImage(int id, out string type)
        {
            byte[] fileBytes = null;
            string fileType = null;
            var databaseDocument = db.IMAGE_DOCUMENTS.FirstOrDefault(doc => doc.DocumentId == id);
            if (databaseDocument != null)
            {
                fileBytes = databaseDocument.FileContent;
                fileType = databaseDocument.Type;
            }
            type = fileType;
            return fileBytes;
        }


    }
}
