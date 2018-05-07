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
using System.Data;
using System.IO;
using System.Data.Entity.Validation;

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
                            join cs in db.COURSEs on b.CRS_ID equals cs.ID
                            where st.IS_DEL == "N" && st.IS_ACT == "Y"
                            orderby st.LAST_NAME, b.NAME
                            select new Models.Student { StudentData = st, BatcheData = b, CourseData = cs }).Distinct();

            if (!String.IsNullOrEmpty(searchString))
            {
                StudentS = StudentS.Where(s => s.StudentData.LAST_NAME.Contains(searchString)
                                       || s.StudentData.FIRST_NAME.Contains(searchString) || s.StudentData.ADMSN_NO.Contains(searchString));
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
        public ActionResult ViewAll()
        {
            var queryCourceBatch = (from cs in db.COURSEs
                                    join bt in db.BATCHes on cs.ID equals bt.CRS_ID
                                    select new Models.SelectCourseBatch { CourseData = cs, BatchData = bt, Selected = false })
                        .OrderBy(x => x.BatchData.ID).ToList();


            List<SelectListItem> options = new List<SelectListItem>();
            foreach (var item in queryCourceBatch)
            {
                string BatchFullName = string.Concat(item.CourseData.CODE, "-", item.BatchData.NAME);
                var result = new SelectListItem();
                result.Text = BatchFullName;
                result.Value = item.BatchData.ID.ToString();
                options.Add(result);
            }
            // add the 'ALL' option
            options.Insert(0, new SelectListItem() { Value = "-1", Text = "Select Batch" });
            ViewBag.searchString = options;
            return View(db.BATCHes.ToList());
        }

        // GET: Student
        public ActionResult _ListStudentsByCourse(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            int searchStringId = 0;
            if (searchString != null && !searchString.Equals("-1"))
            {
                page = 1;
                //searchString = db.BATCHes.Find(searchStringId).NAME.ToString();
            }
            else
            {
                searchString = currentFilter;
            }
            if (!string.IsNullOrEmpty(searchString) && !searchString.Equals("-1"))
            {
                ///As Drop down list sends Id, we will ahve to convert this to text which is different from text box
                searchStringId = Convert.ToInt32(searchString);
            }
            ViewBag.CurrentFilter = searchString;

            var StudentS = (from st in db.STUDENTs
                            join b in db.BATCHes on st.BTCH_ID equals b.ID
                            join cs in db.COURSEs on b.CRS_ID equals cs.ID
                            where st.IS_DEL == "N" && st.IS_ACT == "Y"
                            orderby st.LAST_NAME, b.NAME
                            select new Models.Student { StudentData = st, BatcheData = b, CourseData = cs }).Distinct();

            if (!String.IsNullOrEmpty(searchString) && !searchString.Equals("-1"))
            {
                StudentS = StudentS.Where(s => s.BatcheData.ID.Equals(searchStringId));
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

            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(StudentS.ToPagedList(pageNumber, pageSize));
            //return View(db.USERS.ToList());
        }

        // GET: Student
        public ActionResult BatchTransfer(string searchString, string currentFilter, string AdmissionNumber, string BatchId)
        {
            var queryCourceBatch = (from cs in db.COURSEs
                                    join bt in db.BATCHes on cs.ID equals bt.CRS_ID
                                    select new Models.RadioCourseBatch { CourseData = cs, BatchData = bt})
                        .OrderBy(x => x.BatchData.ID).ToList();


            List<SelectListItem> options = new List<SelectListItem>();
            foreach (var item in queryCourceBatch)
            {
                string BatchFullName = string.Concat(item.CourseData.CODE, "-", item.BatchData.NAME);
                var result = new SelectListItem();
                result.Text = BatchFullName;
                result.Value = item.BatchData.ID.ToString();
                options.Add(result);
            }
            // add the 'ALL' option
            options.Insert(0, new SelectListItem() { Value = "-1", Text = "Select Batch" });
            ViewBag.searchString = options;

            if ((!string.IsNullOrEmpty(searchString) && searchString != "-1") || (!string.IsNullOrEmpty(AdmissionNumber)) || (!string.IsNullOrEmpty(currentFilter)))
            {
                ViewBag.IsPostBack = 1;
            }
            if ((!string.IsNullOrEmpty(BatchId)))
            {
                if (!String.IsNullOrEmpty(searchString) && !searchString.Equals("-1"))
                {
                    int searchStringVal = Convert.ToInt32(searchString);

                    var sTUDENTfROM = db.STUDENTs.Where(x => x.BTCH_ID == searchStringVal && x.IS_DEL == "N" && x.IS_ACT == "Y").ToList();

                    foreach(var item in sTUDENTfROM)
                    {
                        STUDENT sTUDENTfROMuPD = db.STUDENTs.Find(item.ID);

                        int BatchIdVal = Convert.ToInt32(BatchId);

                        var sTUDENTtOiNS = new STUDENT()
                        {
                            ADMSN_NO = sTUDENTfROMuPD.ADMSN_NO,
                            CLS_ROLL_NO = sTUDENTfROMuPD.CLS_ROLL_NO,
                            ADMSN_DATE = sTUDENTfROMuPD.ADMSN_DATE,
                            FIRST_NAME = sTUDENTfROMuPD.FIRST_NAME,
                            MID_NAME = sTUDENTfROMuPD.MID_NAME,
                            LAST_NAME = sTUDENTfROMuPD.LAST_NAME,
                            BTCH_ID = BatchIdVal,
                            DOB = sTUDENTfROMuPD.DOB,
                            GNDR = sTUDENTfROMuPD.GNDR,
                            BLOOD_GRP = sTUDENTfROMuPD.BLOOD_GRP,
                            BIRTH_PLACE = sTUDENTfROMuPD.BIRTH_PLACE,
                            LANG = sTUDENTfROMuPD.LANG,
                            RLGN = sTUDENTfROMuPD.RLGN,
                            ADDR_LINE1 = sTUDENTfROMuPD.ADDR_LINE1,
                            ADDR_LINE2 = sTUDENTfROMuPD.ADDR_LINE2,
                            CITY = sTUDENTfROMuPD.CITY,
                            STATE = sTUDENTfROMuPD.STATE,
                            PIN_CODE = sTUDENTfROMuPD.PIN_CODE,
                            CTRY_ID = sTUDENTfROMuPD.CTRY_ID,
                            PH1 = sTUDENTfROMuPD.PH1,
                            PH2 = sTUDENTfROMuPD.PH2,
                            EML = sTUDENTfROMuPD.EML,
                            IMMDT_CNTCT_ID = sTUDENTfROMuPD.IMMDT_CNTCT_ID,
                            IS_SMS_ENABL = sTUDENTfROMuPD.IS_SMS_ENABL,
                            PHTO_FILENAME = sTUDENTfROMuPD.PHTO_FILENAME,
                            PHTO_CNTNT_TYPE = sTUDENTfROMuPD.PHTO_CNTNT_TYPE,
                            PHTO_DATA = sTUDENTfROMuPD.PHTO_DATA,
                            STAT_DESCR = sTUDENTfROMuPD.STAT_DESCR,
                            IS_ACT = "Y",
                            IS_DEL = "N",
                            CREATED_AT = DateTime.Now,
                            UPDATED_AT = DateTime.Now,
                            HAS_PD_FE = sTUDENTfROMuPD.HAS_PD_FE,
                            PHTO_FILE_SIZE = sTUDENTfROMuPD.PHTO_FILE_SIZE,
                            USRID = sTUDENTfROMuPD.USRID,
                            STDNT_CAT_ID = sTUDENTfROMuPD.STDNT_CAT_ID,
                            NTLTY_ID = sTUDENTfROMuPD.NTLTY_ID,
                            IMAGE_DOCUMENTS_ID = sTUDENTfROMuPD.IMAGE_DOCUMENTS_ID
                        };
                        db.STUDENTs.Add(sTUDENTtOiNS);
                   
                        sTUDENTfROMuPD.IS_ACT = "N";
                        sTUDENTfROMuPD.UPDATED_AT = System.DateTime.Now;
                        db.Entry(sTUDENTfROMuPD).State = EntityState.Modified;
                        //try { db.SaveChanges(); ViewBag.OldRecordMessage = "Old Recrod Saved."; }
                        //catch (Exception e) { Console.WriteLine(e); ViewBag.OldRecordMessage = e.InnerException.InnerException.Message; }
                    }
                    try { db.SaveChanges(); ViewBag.BatchTransferMessage = "Batch transfered for said batch."; }
                    catch (Exception e) { Console.WriteLine(e); ViewBag.BatchTransferMessage = e.InnerException.InnerException.Message; }
                }
                else if ((!string.IsNullOrEmpty(AdmissionNumber)))
                {
                    var sTUDENTfROM = db.STUDENTs.Where(x => x.ADMSN_NO == AdmissionNumber && x.IS_DEL == "N" && x.IS_ACT == "Y").ToList().FirstOrDefault();

                    STUDENT sTUDENTfROMuPD = db.STUDENTs.Find(sTUDENTfROM.ID);
                    sTUDENTfROMuPD.IS_ACT = "N";
                    sTUDENTfROMuPD.UPDATED_AT = System.DateTime.Now;
                    db.Entry(sTUDENTfROMuPD).State = EntityState.Modified;
                    try { db.SaveChanges(); ViewBag.OldRecordMessage = "Old Recrod Saved."; }
                    catch (Exception e) { Console.WriteLine(e); ViewBag.OldRecordMessage = e.InnerException.InnerException.Message; }

                    int BatchIdVal = Convert.ToInt32(BatchId);

                    var sTUDENTtOiNS = new STUDENT()
                    {
                        ADMSN_NO = sTUDENTfROMuPD.ADMSN_NO,
                        CLS_ROLL_NO = sTUDENTfROMuPD.CLS_ROLL_NO,
                        ADMSN_DATE = sTUDENTfROMuPD.ADMSN_DATE,
                        FIRST_NAME = sTUDENTfROMuPD.FIRST_NAME,
                        MID_NAME = sTUDENTfROMuPD.MID_NAME,
                        LAST_NAME = sTUDENTfROMuPD.LAST_NAME,
                        BTCH_ID = BatchIdVal,
                        DOB = sTUDENTfROMuPD.DOB,
                        GNDR = sTUDENTfROMuPD.GNDR,
                        BLOOD_GRP = sTUDENTfROMuPD.BLOOD_GRP,
                        BIRTH_PLACE = sTUDENTfROMuPD.BIRTH_PLACE,
                        LANG = sTUDENTfROMuPD.LANG,
                        RLGN = sTUDENTfROMuPD.RLGN,
                        ADDR_LINE1 = sTUDENTfROMuPD.ADDR_LINE1,
                        ADDR_LINE2 = sTUDENTfROMuPD.ADDR_LINE2,
                        CITY = sTUDENTfROMuPD.CITY,
                        STATE = sTUDENTfROMuPD.STATE,
                        PIN_CODE = sTUDENTfROMuPD.PIN_CODE,
                        CTRY_ID = sTUDENTfROMuPD.CTRY_ID,
                        PH1 = sTUDENTfROMuPD.PH1,
                        PH2 = sTUDENTfROMuPD.PH2,
                        EML = sTUDENTfROMuPD.EML,
                        IMMDT_CNTCT_ID = sTUDENTfROMuPD.IMMDT_CNTCT_ID,
                        IS_SMS_ENABL = sTUDENTfROMuPD.IS_SMS_ENABL,
                        PHTO_FILENAME = sTUDENTfROMuPD.PHTO_FILENAME,
                        PHTO_CNTNT_TYPE = sTUDENTfROMuPD.PHTO_CNTNT_TYPE,
                        PHTO_DATA = sTUDENTfROMuPD.PHTO_DATA,
                        STAT_DESCR = sTUDENTfROMuPD.STAT_DESCR,
                        IS_ACT = "Y",
                        IS_DEL = "N",
                        CREATED_AT = System.DateTime.Now,
                        UPDATED_AT = System.DateTime.Now,
                        HAS_PD_FE = sTUDENTfROMuPD.HAS_PD_FE,
                        PHTO_FILE_SIZE = sTUDENTfROMuPD.PHTO_FILE_SIZE,
                        USRID = sTUDENTfROMuPD.USRID,
                        STDNT_CAT_ID = sTUDENTfROMuPD.STDNT_CAT_ID,
                        NTLTY_ID = sTUDENTfROMuPD.NTLTY_ID,
                        IMAGE_DOCUMENTS_ID = sTUDENTfROMuPD.IMAGE_DOCUMENTS_ID
                    };
                    db.STUDENTs.Add(sTUDENTtOiNS);
                    try { db.SaveChanges(); ViewBag.BatchTransferMessage = "Batch transfered for said student."; }
                    catch (Exception e) { Console.WriteLine(e); ViewBag.BatchTransferMessage = e.InnerException.InnerException.Message; }
                }
            }

            return View(queryCourceBatch.ToList());
        }

        // GET: Student
        public ActionResult _ListStudentsForTransfer(string sortOrder, string currentFilter, string searchString, int? page, string currentFilter2, string AdmissionNumber)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            int searchStringId = 0;
            if (!string.IsNullOrEmpty(searchString) && !searchString.Equals("-1"))
            {
                page = 1;
                //searchString = db.BATCHes.Find(searchStringId).NAME.ToString();
                searchStringId = Convert.ToInt32(searchString);
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            if (!string.IsNullOrEmpty(AdmissionNumber)) { page = 1; }
            else { AdmissionNumber = currentFilter2; }
            ViewBag.CurrentFilter2 = AdmissionNumber;

            var StudentS = (from st in db.STUDENTs
                            join b in db.BATCHes on st.BTCH_ID equals b.ID
                            join cs in db.COURSEs on b.CRS_ID equals cs.ID
                            where st.IS_DEL == "N" && st.IS_ACT == "Y"
                            orderby st.LAST_NAME, b.NAME
                            select new Models.Student { StudentData = st, BatcheData = b, CourseData = cs }).Distinct();

            if (!String.IsNullOrEmpty(searchString) && !searchString.Equals("-1"))
            {
                StudentS = StudentS.Where(s => s.BatcheData.ID.Equals(searchStringId));
            }
            if (!String.IsNullOrEmpty(AdmissionNumber))
            {
                StudentS = StudentS.Where(s => s.StudentData.ADMSN_NO.Equals(AdmissionNumber));
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
        [HttpGet]
        public ActionResult ProfilePDF(int? id)
        {

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

            return View(StudentS.FirstOrDefault());

        }

        // GET: Student
        public ActionResult AdvancedSearch(string sortOrder, string currentFilter, string searchString, int? page, string currentFilter2, string AdmissionNumber, string currentFilter3, string HadPdFees, int? currentFilter4, int? CourseBatches, string currentFilter5, string Category, string currentFilter6, string StudentGender, string currentFilter7, string BloodGroup, string currentFilter8, string StudentGrade, string currentFilter9, string StudentBirthFromDate, string currentFilter10, string StudentBirthToDate, string currentFilter11, string ActiveStudent, string currentFilter12, string MissingDetl)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.NameSortParm2 = sortOrder == "FName" ? "name_desc_2" : "FName";
            ViewBag.NameSortParm3 = sortOrder == "RNum" ? "name_desc_3" : "RNum";
            ViewBag.NameSortParm4 = sortOrder == "AdNum" ? "name_desc_4" : "AdNum";
            ViewBag.NameSortParm5 = sortOrder == "DOB" ? "name_desc_5" : "DOB";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (!string.IsNullOrEmpty(searchString))
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            if (!string.IsNullOrEmpty(AdmissionNumber)) { page = 1; }
            else { AdmissionNumber = currentFilter2; }
            ViewBag.CurrentFilter2 = AdmissionNumber;
            if (!string.IsNullOrEmpty(HadPdFees)) { page = 1; }
            else { HadPdFees = currentFilter3; }
            ViewBag.CurrentFilter3 = HadPdFees;
            if (!CourseBatches.Equals(null)) { page = 1; }
            else { CourseBatches = currentFilter4; }
            ViewBag.CurrentFilter4 = CourseBatches;
            if (!string.IsNullOrEmpty(Category)) { page = 1; }
            else { Category = currentFilter5; }
            ViewBag.CurrentFilter5 = Category;
            if (!string.IsNullOrEmpty(StudentGender)) { page = 1; }
            else { StudentGender = currentFilter6; }
            ViewBag.CurrentFilter6 = StudentGender;
            if (!string.IsNullOrEmpty(BloodGroup)) { page = 1; }
            else { BloodGroup = currentFilter7; }
            ViewBag.CurrentFilter7 = BloodGroup;
            if (!string.IsNullOrEmpty(StudentGrade)) { page = 1; }
            else { StudentGrade = currentFilter8; }
            ViewBag.CurrentFilter8 = StudentGrade;
            if (!string.IsNullOrEmpty(StudentBirthFromDate))
            {
                page = 1;
            }
            else { StudentBirthFromDate = currentFilter9; }
            DateTime? dFrom; DateTime dtFrom;
            dFrom = DateTime.TryParse(StudentBirthFromDate, out dtFrom) ? dtFrom : (DateTime?)null;
            ViewBag.CurrentFilter9 = StudentBirthFromDate;
            if (!string.IsNullOrEmpty(StudentBirthToDate))
            {
                page = 1;
            }
            else { StudentBirthToDate = currentFilter10; }
            DateTime? dTo; DateTime dtTo;
            dTo = DateTime.TryParse(StudentBirthToDate, out dtTo) ? dtTo : (DateTime?)null;
            ViewBag.CurrentFilter10 = StudentBirthToDate;
            if (string.IsNullOrEmpty(ActiveStudent)) { ActiveStudent = "Y"; page = 1; };
            ViewBag.CurrentFilter11 = ActiveStudent;
            if (!string.IsNullOrEmpty(MissingDetl)) { page = 1; }
            else { MissingDetl = currentFilter12; }
            ViewBag.CurrentFilter12 = MissingDetl;

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
                            join grd in db.GUARDIANs on st.ID equals grd.WARD_ID into gd
                            from subgrd in gd.DefaultIfEmpty()
                            orderby st.LAST_NAME, subb.NAME
                            select new Models.Student { StudentData = st, BatcheData = (subb == null ? null : subb), CourseData = (subc == null ? null : subc), CountryData = (subct == null ? null : subct), CategoryData = (subcat == null ? null : subcat), EmployeeData = (subemp == null ? null : subemp), GradeData = (subgrl == null ? null : subgrl), GuardianData = (subgrd == null ? null : subgrd) }).Distinct();

            if (!String.IsNullOrEmpty(searchString))
            {
                StudentS = StudentS.Where(s => s.StudentData.LAST_NAME.Contains(searchString)
                                       || s.StudentData.FIRST_NAME.Contains(searchString));
            }
            if (!String.IsNullOrEmpty(AdmissionNumber))
            {
                StudentS = StudentS.Where(s => s.StudentData.ADMSN_NO.Equals(AdmissionNumber));
            }
            if (!String.IsNullOrEmpty(HadPdFees))
            {
                StudentS = StudentS.Where(s => s.StudentData.HAS_PD_FE.Contains(HadPdFees));
            }
            if (!CourseBatches.Equals(null))
            {
                StudentS = StudentS.Where(s => s.BatcheData.ID == CourseBatches);
            }
            if (!String.IsNullOrEmpty(Category))
            {
                StudentS = StudentS.Where(s => s.CategoryData.NAME.Contains(Category));
            }
            if (!String.IsNullOrEmpty(StudentGender) && !StudentGender.Contains("All"))
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
                StudentS = StudentS.Where(s => s.StudentData.DOB >= dFrom).Where(s => s.StudentData.DOB <= dTo);
            }
            if (!String.IsNullOrEmpty(ActiveStudent))
            {
                StudentS = StudentS.Where(s => s.StudentData.IS_ACT.Equals(ActiveStudent));
            }
            if (!String.IsNullOrEmpty(MissingDetl))
            {
                switch (MissingDetl)
                {
                    case "DateOfBirth":
                        StudentS = StudentS.Where(s => s.StudentData.DOB.Equals(null));
                        break;
                    case "PhoneNumber":
                        StudentS = StudentS.Where(s => s.StudentData.PH1.Equals(null) && s.StudentData.PH2.Equals(null));
                        break;
                    case "ParentDetails":
                        StudentS = StudentS.Where(s => s.GuardianData.ID.Equals(null));
                        break;
                    case "StundetsPicture":
                        StudentS = StudentS.Where(s => s.StudentData.IMAGE_DOCUMENTS_ID.Equals(null));
                        break;
                }
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
                case "FName":
                    StudentS = StudentS.OrderBy(s => s.StudentData.FIRST_NAME);
                    break;
                case "name_desc_2":
                    StudentS = StudentS.OrderByDescending(s => s.StudentData.FIRST_NAME);
                    break;
                case "RNum":
                    StudentS = StudentS.OrderBy(s => s.StudentData.CLS_ROLL_NO);
                    break;
                case "name_desc_3":
                    StudentS = StudentS.OrderByDescending(s => s.StudentData.CLS_ROLL_NO);
                    break;
                case "AdNum":
                    StudentS = StudentS.OrderBy(s => s.StudentData.ADMSN_NO);
                    break;
                case "name_desc_4":
                    StudentS = StudentS.OrderByDescending(s => s.StudentData.ADMSN_NO);
                    break;
                case "DOB":
                    StudentS = StudentS.OrderBy(s => s.StudentData.DOB);
                    break;
                case "name_desc_5":
                    StudentS = StudentS.OrderByDescending(s => s.StudentData.DOB);
                    break;
                default:  // Name ascending 
                    StudentS = StudentS.OrderBy(s => s.StudentData.LAST_NAME);
                    break;
            }
            var queryCourceBatch = (from cs in db.COURSEs
                                    join bt in db.BATCHes on cs.ID equals bt.CRS_ID
                                    select new Models.SelectCourseBatch { CourseData = cs, BatchData = bt, Selected = false })
                        .OrderBy(x => x.BatchData.ID).ToList();


            List<SelectListItem> options = new List<SelectListItem>();
            foreach (var item in queryCourceBatch)
            {
                string BatchFullName = string.Concat(item.CourseData.CODE, "-", item.BatchData.NAME);
                var result = new SelectListItem();
                result.Text = BatchFullName;
                result.Value = item.BatchData.ID.ToString();
                result.Selected = item.BatchData.ID == CourseBatches ? true : false;
                options.Add(result);
            }
            // add the 'ALL' option
            options.Insert(0, new SelectListItem() { Value = null, Text = "ALL" });
            ViewBag.CourseBatches = options;

            List<SelectListItem> options2 = new SelectList(db.STUDENTs, "HAS_PD_FE", "HAS_PD_FE").Distinct().ToList();
            // add the 'ALL' option
            options2.Insert(0, new SelectListItem() { Value = null, Text = "Select Paid Fees Satus" });
            ViewBag.HadPdFees = options2;



            int pageSize = 100;
            int pageNumber = (page ?? 1);
            return View(StudentS.ToPagedList(pageNumber, pageSize));
            //return View(db.USERS.ToList());
        }

        // GET: Student
        [HttpGet]
        public ActionResult AdvancedSearchPdf(string searchString, string AdmissionNumber, string HadPdFees, int? CourseBatches, string Category, string StudentGender, string BloodGroup, string StudentGrade, string StudentBirthFromDate, string StudentBirthToDate, string ActiveStudent, string MissingDetl)
        {

            DateTime? dFrom; DateTime dtFrom;
            dFrom = DateTime.TryParse(StudentBirthFromDate, out dtFrom) ? dtFrom : (DateTime?)null;
            DateTime? dTo; DateTime dtTo;
            dTo = DateTime.TryParse(StudentBirthToDate, out dtTo) ? dtTo : (DateTime?)null;

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
                            join grd in db.GUARDIANs on st.ID equals grd.WARD_ID into gd
                            from subgrd in gd.DefaultIfEmpty()
                            orderby st.LAST_NAME, subb.NAME
                            select new Models.Student { StudentData = st, BatcheData = (subb == null ? null : subb), CourseData = (subc == null ? null : subc), CountryData = (subct == null ? null : subct), CategoryData = (subcat == null ? null : subcat), EmployeeData = (subemp == null ? null : subemp), GradeData = (subgrl == null ? null : subgrl), GuardianData = (subgrd == null ? null : subgrd) }).Distinct();

            if (!String.IsNullOrEmpty(searchString))
            {
                StudentS = StudentS.Where(s => s.StudentData.LAST_NAME.Contains(searchString)
                                       || s.StudentData.FIRST_NAME.Contains(searchString));
            }
            if (!String.IsNullOrEmpty(AdmissionNumber))
            {
                StudentS = StudentS.Where(s => s.StudentData.ADMSN_NO.Equals(AdmissionNumber));
            }
            if (!String.IsNullOrEmpty(HadPdFees))
            {
                StudentS = StudentS.Where(s => s.StudentData.HAS_PD_FE.Contains(HadPdFees));
            }
            if (!CourseBatches.Equals(null))
            {
                StudentS = StudentS.Where(s => s.BatcheData.ID == CourseBatches);
            }
            if (!String.IsNullOrEmpty(Category))
            {
                StudentS = StudentS.Where(s => s.CategoryData.NAME.Contains(Category));
            }
            if (!String.IsNullOrEmpty(StudentGender) && !StudentGender.Contains("All"))
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
                StudentS = StudentS.Where(s => s.StudentData.DOB >= dFrom).Where(s => s.StudentData.DOB <= dTo);
            }
            if (!String.IsNullOrEmpty(ActiveStudent))
            {
                StudentS = StudentS.Where(s => s.StudentData.IS_ACT.Equals(ActiveStudent));
            }
            if (!String.IsNullOrEmpty(MissingDetl))
            {
                switch (MissingDetl)
                {
                    case "DateOfBirth":
                        StudentS = StudentS.Where(s => s.StudentData.DOB.Equals(null));
                        break;
                    case "PhoneNumber":
                        StudentS = StudentS.Where(s => s.StudentData.PH1.Equals(null) && s.StudentData.PH2.Equals(null));
                        break;
                    case "ParentDetails":
                        StudentS = StudentS.Where(s => s.GuardianData.ID.Equals(null));
                        break;
                    case "StundetsPicture":
                        StudentS = StudentS.Where(s => s.StudentData.IMAGE_DOCUMENTS_ID.Equals(null));
                        break;
                }
            }

            return View(StudentS);

        }

        // GET: Student/Edit/5
        public ActionResult ActivateStudent(int? id)
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
            sTUDENT.IS_ACT = "Y";
            sTUDENT.IS_DEL = "N";
            sTUDENT.UPDATED_AT = System.DateTime.Now;
            db.Entry(sTUDENT).State = EntityState.Modified;
            try { db.SaveChanges(); }
            catch (Exception ex)
            {
                throw ex; // Oops, something went wrong, handle the exception
            }

            return RedirectToAction("Index");
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
            DateTime PDate = Convert.ToDateTime(System.DateTime.Now);
            ViewBag.ReturnDate = PDate.ToShortDateString();
            ViewBag.CTRY_ID = new SelectList(db.COUNTRies, "ID", "CTRY_NAME", "99");
            ViewBag.NTLTY_ID = new SelectList(db.COUNTRies.Where(o => o.NTLTY != " ").ToList(), "ID", "NTLTY", "99");
            ViewBag.STDNT_CAT_ID = new SelectList(db.STUDENT_CATGEORY, "ID", "NAME");
            ///Code to get the Batch along weith Course
            var queryCourceBatch = (from cs in db.COURSEs
                                    join bt in db.BATCHes on cs.ID equals bt.CRS_ID
                                    where cs.IS_DEL == "N" && bt.IS_DEL == "N"
                                    select new { CourseData = cs, BatchData = bt })
                                    .OrderBy(x => x.BatchData.ID).ToList();


            List<SelectListItem> options = new List<SelectListItem>();
            foreach (var item in queryCourceBatch)
            {
                string BatchFullName = string.Concat(item.CourseData.CODE, "-", item.BatchData.NAME);
                var result = new SelectListItem();
                result.Text = BatchFullName;
                result.Value = item.BatchData.ID.ToString();
                options.Add(result);
            }

            options.Insert(0, new SelectListItem() { Value = "-1", Text = "Select Course and Batch" });
            ViewBag.BTCH_ID = options;
            //End of Code to get batch and Course

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
        public ActionResult Admission1([Bind(Include = "ID,ADMSN_NO,CLS_ROLL_NO,ADMSN_DATE,FIRST_NAME,MID_NAME,LAST_NAME,BTCH_ID,DOB,GNDR,BLOOD_GRP,BIRTH_PLACE,LANG,RLGN,ADDR_LINE1,ADDR_LINE2,CITY,STATE,PIN_CODE,CTRY_ID,PH1,PH2,EML,IMMDT_CNTCT_ID,IS_SMS_ENABL,PHTO_FILENAME,PHTO_CNTNT_TYPE,PHTO_DATA,STAT_DESCR,IS_ACT,IS_DEL,CREATED_AT,UPDATED_AT,HAS_PD_FE,PHTO_FILE_SIZE,USRID,STDNT_CAT_ID,NTLTY_ID,IMAGE_DOCUMENTS_ID")] STUDENT sTUDENT)
        {
            int UserId = Convert.ToInt32(this.Session["UserId"]);
            sTUDENT.USRID = UserId;
            if (ModelState.IsValid)
            {
                /////Picture Upload Code
                string FileName = null;
                SuccessModel viewModel = new SuccessModel();
                if (Request.Files.Count == 1 && Request.Files[0].FileName != "")
                {
                    var name = Request.Files[0].FileName;
                    var size = Request.Files[0].ContentLength;
                    var type = Request.Files[0].ContentType;
                    FileName = name;
                    int PhotoId = 0;
                    PhotoId = HandleUpload(Request.Files[0].InputStream, name, size, type, PhotoId);

                    sTUDENT.IMAGE_DOCUMENTS_ID = PhotoId;
                    sTUDENT.PHTO_FILENAME = null;
                }
                ////End to Picture Upload Code

                sTUDENT.IS_ACT = "Y";
                sTUDENT.IS_DEL = "N";
                sTUDENT.CREATED_AT = System.DateTime.Now;
                sTUDENT.UPDATED_AT = System.DateTime.Now;
                db.STUDENTs.Add(sTUDENT);
                try { db.SaveChanges(); }
                catch (DbEntityValidationException e)
                {
                    ViewBag.NewAdmissionNumber = sTUDENT.ADMSN_NO;
                    DateTime PDate = Convert.ToDateTime(sTUDENT.ADMSN_DATE);
                    ViewBag.ReturnDate = PDate.ToShortDateString();
                    ViewBag.CTRY_ID = new SelectList(db.COUNTRies, "ID", "CTRY_NAME", sTUDENT.CTRY_ID);
                    ViewBag.NTLTY_ID = new SelectList(db.COUNTRies.Where(o => o.NTLTY != " ").ToList(), "ID", "NTLTY", sTUDENT.NTLTY_ID);
                    ViewBag.STDNT_CAT_ID = new SelectList(db.STUDENT_CATGEORY, "ID", "NAME", sTUDENT.STDNT_CAT_ID);
                    ///Code to get the Batch along weith Course
                    var queryCourceBatch = (from cs in db.COURSEs
                                            join bt in db.BATCHes on cs.ID equals bt.CRS_ID
                                            where cs.IS_DEL == "N" && bt.IS_DEL == "N"
                                            select new { CourseData = cs, BatchData = bt })
                                            .OrderBy(x => x.BatchData.ID).ToList();


                    List<SelectListItem> options = new List<SelectListItem>();
                    foreach (var item in queryCourceBatch)
                    {
                        string BatchFullName = string.Concat(item.CourseData.CODE, "-", item.BatchData.NAME);
                        var result1 = new SelectListItem();
                        result1.Text = BatchFullName;
                        result1.Value = item.BatchData.ID.ToString();
                        result1.Selected = item.BatchData.ID == sTUDENT.BTCH_ID ? true : false;
                        options.Add(result1);
                    }

                    options.Insert(0, new SelectListItem() { Value = "-1", Text = "Select Course and Batch" });
                    ViewBag.BTCH_ID = options;
                    //End of Code to get batch and Course

                    var configValue = (from C in db.CONFIGURATIONs
                                       where C.CONFIG_KEY == "AdmissionNumberAutoIncrement"
                                       select new { CONFIG_VALUE = C.CONFIG_VAL }).FirstOrDefault();
                    int NewAdmissionNumberNum = Convert.ToInt32(configValue.CONFIG_VALUE.ToString()) + 1;
                    ViewBag.NewAdmissionNumber = NewAdmissionNumberNum.ToString();
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        //Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                         //   eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            //Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            //    ve.PropertyName, ve.ErrorMessage);
                            ViewBag.ErrorMessage = string.Concat(ViewBag.ErrorMessage, "|", ve.ErrorMessage);
                        }
                    }
                    return View(sTUDENT);
                }
                catch (Exception e) {
                    ViewBag.NewAdmissionNumber = sTUDENT.ADMSN_NO;
                    DateTime PDate = Convert.ToDateTime(sTUDENT.ADMSN_DATE);
                    ViewBag.ReturnDate = PDate.ToShortDateString();
                    ViewBag.CTRY_ID = new SelectList(db.COUNTRies, "ID", "CTRY_NAME", sTUDENT.CTRY_ID);
                    ViewBag.NTLTY_ID = new SelectList(db.COUNTRies.Where(o => o.NTLTY != " ").ToList(), "ID", "NTLTY", sTUDENT.NTLTY_ID);
                    ViewBag.STDNT_CAT_ID = new SelectList(db.STUDENT_CATGEORY, "ID", "NAME", sTUDENT.STDNT_CAT_ID);
                    ///Code to get the Batch along weith Course
                    var queryCourceBatch = (from cs in db.COURSEs
                                            join bt in db.BATCHes on cs.ID equals bt.CRS_ID
                                            where cs.IS_DEL == "N" && bt.IS_DEL == "N"
                                            select new { CourseData = cs, BatchData = bt })
                                            .OrderBy(x => x.BatchData.ID).ToList();


                    List<SelectListItem> options = new List<SelectListItem>();
                    foreach (var item in queryCourceBatch)
                    {
                        string BatchFullName = string.Concat(item.CourseData.CODE, "-", item.BatchData.NAME);
                        var result1 = new SelectListItem();
                        result1.Text = BatchFullName;
                        result1.Value = item.BatchData.ID.ToString();
                        result1.Selected = item.BatchData.ID == sTUDENT.BTCH_ID ? true : false;
                        options.Add(result1);
                    }

                    options.Insert(0, new SelectListItem() { Value = "-1", Text = "Select Course and Batch" });
                    ViewBag.BTCH_ID = options;
                    //End of Code to get batch and Course

                    var configValue = (from C in db.CONFIGURATIONs
                                       where C.CONFIG_KEY == "AdmissionNumberAutoIncrement"
                                       select new { CONFIG_VALUE = C.CONFIG_VAL }).FirstOrDefault();
                    int NewAdmissionNumberNum = Convert.ToInt32(configValue.CONFIG_VALUE.ToString()) + 1;
                    ViewBag.NewAdmissionNumber = NewAdmissionNumberNum.ToString();
                    ViewBag.ErrorMessage = string.Concat(ViewBag.ErrorMessage, "|", e.InnerException.InnerException.Message);
                    return View(sTUDENT);
                }

                var result = from u in db.CONFIGURATIONs where (u.CONFIG_KEY == "AdmissionNumberAutoIncrement") select u;
                if (result.Count() != 0)
                {
                    var dbConfig = result.First();

                    dbConfig.CONFIG_VAL = sTUDENT.ADMSN_NO;
                    db.SaveChanges();
                }

                string FullName = string.Concat(sTUDENT.FIRST_NAME, sTUDENT.LAST_NAME);
                var StdUser = new USER() { USRNAME = FullName, FIRST_NAME = sTUDENT.FIRST_NAME, LAST_NAME = sTUDENT.LAST_NAME, EML = sTUDENT.EML, ADMIN_IND = "N", STDNT_IND = "Y", EMP_IND = "N", HASHED_PSWRD = string.Concat(sTUDENT.ADMSN_NO, 123), SALT = "N", RST_PSWRD_CODE = null, RST_PSWRD_CODE_UNTL = null, CREATED_AT = System.DateTime.Now, UPDATED_AT = System.DateTime.Now, PARNT_IND = "N" };
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
            if (!String.IsNullOrEmpty(sTUDENT.ADDR_LINE1))
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
            //COUNTRY cOUNTRY = db.COUNTRies.Find(sTUDENT.CTRY_ID);
            ViewBag.CountryName = sTUDENT.CTRY_ID;
            ViewBag.CTRY_ID = new SelectList(db.COUNTRies, "ID", "CTRY_NAME", "99");
            DataTable dtGuardian = new DataTable();
            dtGuardian.Columns.Add("FIRST_NAME", typeof(string));
            dtGuardian.Columns.Add("LAST_NAME", typeof(string));
            dtGuardian.Columns.Add("REL", typeof(string));
            dtGuardian.Columns.Add("MOB", typeof(string));
            var GuardianVal = (from C in db.GUARDIANs
                               where C.WARD_ID == Std_id
                               select new { f_name = C.FIRST_NAME, l_name = C.LAST_NAME, rel = C.REL, mob = C.MOBL_PH }).ToList();

            foreach (var entity in GuardianVal.ToList())
            {
                var row = dtGuardian.NewRow();
                row["FIRST_NAME"] = entity.f_name;
                row["LAST_NAME"] = entity.l_name;
                row["REL"] = entity.rel;
                row["MOB"] = entity.mob;
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
                gUARDIAN.CREATED_AT = System.DateTime.Now;
                gUARDIAN.UPDATED_AT = System.DateTime.Now;
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
            if (GuardianVal.Count().Equals(0))
            {
                ViewBag.ErrorMessage = "Any guardian must be added to proceed forward.";
            }
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
                    sTUDENT.IMMDT_CNTCT_ID = item.GuardianList.ID;
                    //db.SaveChanges();
                    try { db.SaveChanges(); ViewBag.ErrorMessage = string.Concat("Guardian Details added successfully for Studnet Id:", item.GuardianList.WARD_ID, ". Please click on link above to go to Stundet Details Page."); }
                    catch (Exception e) { Console.WriteLine(e); ViewBag.ErrorMessage = e.InnerException.InnerException.Message; }
                    break;
                }
            }
            //return RedirectToAction("Index", "Student");
            return View(model);

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
                sTUDENT.IS_ACT = "Y";
                sTUDENT.IS_DEL = "N";
                sTUDENT.CREATED_AT = System.DateTime.Now;
                sTUDENT.UPDATED_AT = System.DateTime.Now;
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
            ViewBag.CTRY_ID = new SelectList(db.COUNTRies, "ID", "CTRY_NAME", sTUDENT.CTRY_ID);

            ///Code to get the Batch along weith Course
            var queryCourceBatch = (from cs in db.COURSEs
                                    join bt in db.BATCHes on cs.ID equals bt.CRS_ID
                                    where cs.IS_DEL == "N" && bt.IS_DEL == "N"
                                    select new { CourseData = cs, BatchData = bt })
                                    .OrderBy(x => x.BatchData.ID).ToList();


            List<SelectListItem> options = new List<SelectListItem>();
            foreach (var item in queryCourceBatch)
            {
                string BatchFullName = string.Concat(item.CourseData.CODE, "-", item.BatchData.NAME);
                var result = new SelectListItem();
                result.Text = BatchFullName;
                result.Value = item.BatchData.ID.ToString();
                result.Selected = item.BatchData.ID == sTUDENT.BTCH_ID ? true : false;
                options.Add(result);
            }

            options.Insert(0, new SelectListItem() { Value = "-1", Text = "Select Course and Batch" });
            ViewBag.BTCH_ID = options;
            //End of Code to get batch and Course
            return View(sTUDENT);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ADMSN_NO,CLS_ROLL_NO,ADMSN_DATE,FIRST_NAME,MID_NAME,LAST_NAME,BTCH_ID,DOB,GNDR,BLOOD_GRP,BIRTH_PLACE,LANG,RLGN,ADDR_LINE1,ADDR_LINE2,CITY,STATE,PIN_CODE,CTRY_ID,PH1,PH2,EML,IMMDT_CNTCT_ID,IS_SMS_ENABL,PHTO_FILENAME,PHTO_CNTNT_TYPE,PHTO_DATA,STAT_DESCR,IS_ACT,IS_DEL,CREATED_AT,UPDATED_AT,HAS_PD_FE,PHTO_FILE_SIZE,USRID,STDNT_CAT_ID,NTLTY_ID,IMAGE_DOCUMENTS_ID")] STUDENT sTUDENT)
        {
            if (ModelState.IsValid)
            {

                STUDENT sTUDENTtOuPDATE = db.STUDENTs.Find(sTUDENT.ID);

                /////Picture Upload Code
                string FileName = null;
                SuccessModel viewModel = new SuccessModel();
                if (Request.Files.Count == 1 && Request.Files[0].FileName != "")
                {
                    var name = Request.Files[0].FileName;
                    var size = Request.Files[0].ContentLength;
                    var type = Request.Files[0].ContentType;
                    FileName = name;
                    sTUDENTtOuPDATE.IMAGE_DOCUMENTS_ID = HandleUpload(Request.Files[0].InputStream, name, size, type, Convert.ToInt32(sTUDENT.IMAGE_DOCUMENTS_ID));
                }
                ////End to Picture Upload Code
                sTUDENTtOuPDATE.ADMSN_NO = sTUDENT.ADMSN_NO;
                sTUDENTtOuPDATE.ADMSN_DATE = sTUDENT.ADMSN_DATE;
                sTUDENTtOuPDATE.FIRST_NAME = sTUDENT.FIRST_NAME;
                sTUDENTtOuPDATE.MID_NAME = sTUDENT.MID_NAME;
                sTUDENTtOuPDATE.LAST_NAME = sTUDENT.LAST_NAME;
                sTUDENTtOuPDATE.BTCH_ID = sTUDENT.BTCH_ID;
                sTUDENTtOuPDATE.DOB = sTUDENT.DOB;
                sTUDENTtOuPDATE.GNDR = sTUDENT.GNDR;
                sTUDENTtOuPDATE.BLOOD_GRP = sTUDENT.BLOOD_GRP;
                sTUDENTtOuPDATE.BIRTH_PLACE = sTUDENT.BIRTH_PLACE;
                sTUDENTtOuPDATE.NTLTY_ID = sTUDENT.NTLTY_ID;
                sTUDENTtOuPDATE.LANG = sTUDENT.LANG;
                sTUDENTtOuPDATE.STDNT_CAT_ID = sTUDENT.STDNT_CAT_ID;
                sTUDENTtOuPDATE.RLGN = sTUDENT.RLGN;
                sTUDENTtOuPDATE.ADDR_LINE1 = sTUDENT.ADDR_LINE1;
                sTUDENTtOuPDATE.ADDR_LINE2 = sTUDENT.ADDR_LINE2;
                sTUDENTtOuPDATE.CITY = sTUDENT.CITY;
                sTUDENTtOuPDATE.STATE = sTUDENT.STATE;
                sTUDENTtOuPDATE.PIN_CODE = sTUDENT.PIN_CODE;
                sTUDENTtOuPDATE.CTRY_ID = sTUDENT.CTRY_ID;
                sTUDENTtOuPDATE.PH1 = sTUDENT.PH1;
                sTUDENTtOuPDATE.PH2 = sTUDENT.PH2;
                sTUDENTtOuPDATE.EML = sTUDENT.EML;
                sTUDENTtOuPDATE.CLS_ROLL_NO = sTUDENT.CLS_ROLL_NO;
                sTUDENTtOuPDATE.USRID = sTUDENT.USRID;
                sTUDENTtOuPDATE.IS_SMS_ENABL = sTUDENT.IS_SMS_ENABL;

                sTUDENTtOuPDATE.UPDATED_AT = System.DateTime.Now;
                db.Entry(sTUDENTtOuPDATE).State = EntityState.Modified;
                try { db.SaveChanges(); ViewBag.ErrorMessage = "Student's Details updated successfully."; }
                catch (Exception e) { Console.WriteLine(e); ViewBag.ErrorMessage = e.InnerException.InnerException.Message; }

                ViewBag.NTLTY_ID = new SelectList(db.COUNTRies, "ID", "CTRY_NAME", sTUDENTtOuPDATE.NTLTY_ID);
                ViewBag.STDNT_CAT_ID = new SelectList(db.STUDENT_CATGEORY, "ID", "NAME", sTUDENTtOuPDATE.STDNT_CAT_ID);
                ViewBag.USRID = new SelectList(db.USERS, "ID", "USRNAME", sTUDENTtOuPDATE.USRID);
                ViewBag.CTRY_ID = new SelectList(db.COUNTRies, "ID", "CTRY_NAME", sTUDENTtOuPDATE.CTRY_ID);

                ///Code to get the Batch along weith Course
                var queryCourceBatch = (from cs in db.COURSEs
                                        join bt in db.BATCHes on cs.ID equals bt.CRS_ID
                                        where cs.IS_DEL == "N" && bt.IS_DEL == "N"
                                        select new { CourseData = cs, BatchData = bt })
                                        .OrderBy(x => x.BatchData.ID).ToList();


                List<SelectListItem> options = new List<SelectListItem>();
                foreach (var item in queryCourceBatch)
                {
                    string BatchFullName = string.Concat(item.CourseData.CODE, "-", item.BatchData.NAME);
                    var result = new SelectListItem();
                    result.Text = BatchFullName;
                    result.Value = item.BatchData.ID.ToString();
                    result.Selected = item.BatchData.ID == sTUDENTtOuPDATE.BTCH_ID ? true : false;
                    options.Add(result);
                }

                options.Insert(0, new SelectListItem() { Value = "-1", Text = "Select Course and Batch" });
                ViewBag.BTCH_ID = options;
                return View(sTUDENTtOuPDATE);
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
            var queryCourceBatch = (from cs in db.COURSEs
                                    join bt in db.BATCHes on cs.ID equals bt.CRS_ID
                                    select new Models.SelectCourseBatch { CourseData = cs, BatchData = bt, Selected = false })
                                    .OrderBy(x => x.BatchData.ID).ToList();
            List<SelectListItem> options = new List<SelectListItem>();
            foreach (var item in queryCourceBatch)
            {
                string BatchFullName = string.Concat(item.CourseData.CODE, "-", item.BatchData.NAME);
                var result = new SelectListItem();
                result.Text = BatchFullName;
                result.Value = item.BatchData.ID.ToString();
                result.Selected = item.BatchData.ID == sTUDENT.BTCH_ID ? true : false;
                options.Add(result);
            }
            // add the 'ALL' option
            options.Insert(0, new SelectListItem() { Value = "-1", Text = "ALL" });
            ViewBag.BTCH_ID = options;
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
            sTUDENT.IS_ACT = "N";
            sTUDENT.IS_DEL = "Y";
            sTUDENT.UPDATED_AT = System.DateTime.Now;
            db.Entry(sTUDENT).State = EntityState.Modified;
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

        // GET: Student/Guardian Details
        public ActionResult Guardians(int? Std_id)
        {
            //var parents = db.GUARDIANs.Where(x=>x.WARD_ID == Std_id).ToList();

            var parents = (from st in db.STUDENTs
                            join b in db.BATCHes on st.BTCH_ID equals b.ID into gi
                            from subb in gi.DefaultIfEmpty()
                            join c in db.COURSEs on subb.CRS_ID equals c.ID into gj
                            from subc in gj.DefaultIfEmpty()
                            join emp in db.EMPLOYEEs on subb.EMP_ID equals emp.ID into gm
                            from subemp in gm.DefaultIfEmpty()
                            join grd in db.GUARDIANs on st.ID equals grd.WARD_ID into gd
                            from subgrd in gd.DefaultIfEmpty()
                           where st.ID == Std_id && st.IS_ACT =="Y" && st.IS_DEL == "N"
                           orderby st.LAST_NAME, subb.NAME
                            select new Models.StudentsGuardians { StudentData = st, BatchData = (subb == null ? null : subb), CourseData = (subc == null ? null : subc),  EmployeeData = (subemp == null ? null : subemp), GuardianData = (subgrd == null ? null : subgrd) }).Distinct();

            if (parents.FirstOrDefault().GuardianData == null)
            {
                ViewBag.GuardianMessage = "No Parents added to this Student yet. Please click on 'Add Parents' button to add parents.";
            }
            ViewBag.WARD_ID = Std_id;

            return View(parents.ToList());
        }

        // GET: Student/Guardian Details
        public ActionResult Edit_Guardian(int? id)
        {
            GUARDIAN parents = db.GUARDIANs.Find(id);

            ViewBag.CTRY_ID = new SelectList(db.COUNTRies, "ID", "CTRY_NAME", parents.CTRY_ID);
            DateTime SDate = Convert.ToDateTime(parents.DOB);
            ViewBag.DOB = SDate.ToShortDateString();
            //var aDMISSIONnUM = db.STUDENTs.Where(x => x.ID == parents.WARD_ID).ToList().FirstOrDefault().ADMSN_NO;

            ViewBag.ReturnDate = System.DateTime.Now;
            STUDENT sTUDENT = db.STUDENTs.Find(parents.WARD_ID);
            ViewBag.StudentFullName = String.Format("{0} {1}", sTUDENT.FIRST_NAME.ToString(), sTUDENT.LAST_NAME.ToString());
            ViewBag.NewAdmissionNumber = sTUDENT.ADMSN_NO.ToString();
            if (!String.IsNullOrEmpty(sTUDENT.ADDR_LINE1))
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
            ViewBag.StudentId = parents.WARD_ID;
            ViewBag.ADMSN_NO = sTUDENT.ADMSN_NO;

            return View(parents);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_Guardian([Bind(Include = "ID,WARD_ID,FIRST_NAME,LAST_NAME, REL, EML, OFF_PH1, OFF_PH2, MOBL_PH, OFF_ADDR_LINE1, OFF_ADDR_LINE2, CITY, STATE, CTRY_ID, DOB, OCCP, INCM, ED, USRID")] GUARDIAN gUARDIAN, string ADMSN_NO)
        {
            if (ModelState.IsValid)
            {
                GUARDIAN gUARDIAN_UPD = db.GUARDIANs.Find(gUARDIAN.ID);
                gUARDIAN_UPD.ID = gUARDIAN.ID;
                gUARDIAN_UPD.FIRST_NAME = gUARDIAN.FIRST_NAME;
                gUARDIAN_UPD.LAST_NAME = gUARDIAN.LAST_NAME;
                gUARDIAN_UPD.REL = gUARDIAN.REL;
                gUARDIAN_UPD.EML = gUARDIAN.EML;
                gUARDIAN_UPD.OFF_PH1 = gUARDIAN.OFF_PH1;
                gUARDIAN_UPD.OFF_PH2 = gUARDIAN.OFF_PH2;
                gUARDIAN_UPD.MOBL_PH = gUARDIAN.MOBL_PH;
                gUARDIAN_UPD.OFF_ADDR_LINE1 = gUARDIAN.OFF_ADDR_LINE1;
                gUARDIAN_UPD.OFF_ADDR_LINE2 = gUARDIAN.OFF_ADDR_LINE2;
                gUARDIAN_UPD.CITY = gUARDIAN.CITY;
                gUARDIAN_UPD.STATE = gUARDIAN.STATE;
                gUARDIAN_UPD.CTRY_ID = gUARDIAN.CTRY_ID;
                gUARDIAN_UPD.DOB = gUARDIAN.DOB;
                gUARDIAN_UPD.OCCP = gUARDIAN.OCCP;
                gUARDIAN_UPD.INCM = gUARDIAN.INCM;
                gUARDIAN_UPD.ED = gUARDIAN.ED;
                gUARDIAN_UPD.UPDATED_AT = DateTime.Now;
                db.Entry(gUARDIAN_UPD).State = EntityState.Modified;
                try { db.SaveChanges(); ViewBag.GuardianEditMessage = "Guardian Details updated successfully."; }
                catch (Exception e) { Console.WriteLine(e); ViewBag.GuardianEditMessage = e.InnerException.InnerException.Message; }
                ViewBag.CTRY_ID = new SelectList(db.COUNTRies, "ID", "CTRY_NAME", gUARDIAN.CTRY_ID);
                DateTime SDate1 = Convert.ToDateTime(gUARDIAN.DOB);
                ViewBag.DOB = SDate1.ToShortDateString();
                //var aDMISSIONnUM1 = db.STUDENTs.Where(x => x.ID == gUARDIAN.WARD_ID).ToList().FirstOrDefault().ADMSN_NO;
                ViewBag.ADMSN_NO = ADMSN_NO;
                return View(gUARDIAN);
            }
            ViewBag.CTRY_ID = new SelectList(db.COUNTRies, "ID", "CTRY_NAME", gUARDIAN.CTRY_ID);
            DateTime SDate = Convert.ToDateTime(gUARDIAN.DOB);
            ViewBag.DOB = SDate.ToShortDateString();
            //var aDMISSIONnUM = db.STUDENTs.Where(x => x.ID == gUARDIAN.WARD_ID).ToList().FirstOrDefault().ADMSN_NO;
            ViewBag.ADMSN_NO = ADMSN_NO;
            return View(gUARDIAN);

        }

        // GET: Student/Delete/5
        public ActionResult Delete_Guardian(int? id, int? std_id)
        {
            GUARDIAN gUARDIAN = db.GUARDIANs.Find(id);
            db.GUARDIANs.Remove(gUARDIAN);
            db.SaveChanges();
            return RedirectToAction("Guardians", new { Std_id = std_id });
        }

        // GET: Student/Delete/5
        public ActionResult Add_Guardian(int? std_id)
        {
            ViewBag.CTRY_ID = new SelectList(db.COUNTRies, "ID", "CTRY_NAME", "99");
            
            ViewBag.ReturnDate = System.DateTime.Now;
            STUDENT sTUDENT = db.STUDENTs.Find(std_id);
            ViewBag.StudentFullName = String.Format("{0} {1}", sTUDENT.FIRST_NAME.ToString(), sTUDENT.LAST_NAME.ToString());
            ViewBag.NewAdmissionNumber = sTUDENT.ADMSN_NO.ToString();
            if (!String.IsNullOrEmpty(sTUDENT.ADDR_LINE1))
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
            ViewBag.StudentId = std_id;
            ViewBag.ADMSN_NO = sTUDENT.ADMSN_NO;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add_Guardian([Bind(Include = "ID,WARD_ID,FIRST_NAME,LAST_NAME,REL,EML,OFF_PH1,OFF_PH2,MOBL_PH,OFF_ADDR_LINE1,OFF_ADDR_LINE2,CITY,STATE,CTRY_ID,DOB,OCCP,INCM,ED,CREATED_AT,UPDATED_AT,USRID")] GUARDIAN gUARDIAN, string ADMSN_NO)
        {
            if (ModelState.IsValid)
            {
                gUARDIAN.CREATED_AT = DateTime.Now;
                gUARDIAN.UPDATED_AT = DateTime.Now;
                db.GUARDIANs.Add(gUARDIAN);
                try { db.SaveChanges(); ViewBag.GuardianAddMessage = "Guardian Added successfully."; }
                catch (Exception e) { Console.WriteLine(e); ViewBag.GuardianAddMessage = e.InnerException.InnerException.Message; }
                ViewBag.CTRY_ID = new SelectList(db.COUNTRies, "ID", "CTRY_NAME", "99");
                //DateTime SDate = Convert.ToDateTime(parents.DOB);
                //ViewBag.DOB = SDate.ToShortDateString();
                var aDMISSIONnUM = db.STUDENTs.Where(x => x.ID == gUARDIAN.WARD_ID).ToList().FirstOrDefault().ADMSN_NO;
                ViewBag.ADMSN_NO = aDMISSIONnUM;
                return RedirectToAction("admission3_1", new { Std_id = gUARDIAN .WARD_ID});
            }

            return View(gUARDIAN);
        }

        // GET: Student/Admission3
        public ActionResult admission3_1(int? Std_id)
        {
            //List<SelectGuardian> Guardian = new List<SelectGuardian>();

            var GuardianVal = (from C in db.GUARDIANs
                               where C.WARD_ID == Std_id
                               select new Models.SelectGuardian { GuardianList = C }).ToList();
            if (GuardianVal.Count().Equals(0))
            {
                ViewBag.ErrorMessage = "Any guardian must be added to proceed forward.";
            }
            return View(GuardianVal);

        }

        // POST: Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // GET: Student/Admission3
        public ActionResult admission3_1(IList<SelectGuardian> model)
        {
            foreach (SelectGuardian item in model)
            {
                if (item.Selected)
                {
                    STUDENT sTUDENT = db.STUDENTs.Find(item.GuardianList.WARD_ID);
                    sTUDENT.IMMDT_CNTCT_ID = item.GuardianList.ID;
                    //db.SaveChanges();
                    try { db.SaveChanges(); ViewBag.ErrorMessage = string.Concat("Guardian Details added successfully for Studnet Id:", item.GuardianList.WARD_ID,". Please click on link above to go to Stundet Details Page."); }
                    catch (Exception e) { Console.WriteLine(e); ViewBag.ErrorMessage = e.InnerException.InnerException.Message; }
                    break;
                }
            }
            //return RedirectToAction("Index", "Student");
            return View(model);

        }

        // GET: Student/Guardian Details
        public ActionResult Add_Existing_Guardian(int? Std_id)
        {
            //var parents = db.GUARDIANs.Where(x=>x.WARD_ID == Std_id).ToList();

            var parents = (from st in db.STUDENTs
                           join b in db.BATCHes on st.BTCH_ID equals b.ID into gi
                           from subb in gi.DefaultIfEmpty()
                           join c in db.COURSEs on subb.CRS_ID equals c.ID into gj
                           from subc in gj.DefaultIfEmpty()
                           join emp in db.EMPLOYEEs on subb.EMP_ID equals emp.ID into gm
                           from subemp in gm.DefaultIfEmpty()
                           join grd in db.GUARDIANs on st.ID equals grd.WARD_ID
                           where st.IS_DEL == "N"
                           orderby grd.LAST_NAME, grd.FIRST_NAME
                           select new Models.StudentsGuardians { StudentData = st, BatchData = (subb == null ? null : subb), CourseData = (subc == null ? null : subc), EmployeeData = (subemp == null ? null : subemp), GuardianData = grd }).Distinct();

            ViewBag.WARD_ID = Std_id;

            return View(parents.ToList());
        }

        // GET: Student/Guardian Details
        public ActionResult Add_Existing_Guardian2(int? Std_id, int? Parent_id)
        {
            var gUARDIAN = db.GUARDIANs.Find(Parent_id);

            var gUARDIANnEW = new GUARDIAN()
            {
                WARD_ID = Std_id,
                FIRST_NAME = gUARDIAN.FIRST_NAME,
                LAST_NAME = gUARDIAN.LAST_NAME,
                REL = gUARDIAN.REL,
                EML = gUARDIAN.EML,
                OFF_PH1 = gUARDIAN.OFF_PH1,
                OFF_PH2 = gUARDIAN.OFF_PH2,
                MOBL_PH = gUARDIAN.MOBL_PH,
                OFF_ADDR_LINE1 = gUARDIAN.OFF_ADDR_LINE1,
                OFF_ADDR_LINE2 = gUARDIAN.OFF_ADDR_LINE2,
                CITY = gUARDIAN.CITY,
                STATE = gUARDIAN.STATE,
                CTRY_ID = gUARDIAN.CTRY_ID,
                DOB = gUARDIAN.DOB,
                OCCP = gUARDIAN.OCCP,
                INCM = gUARDIAN.INCM,
                ED = gUARDIAN.ED,
                CREATED_AT = System.DateTime.Now,
                UPDATED_AT = System.DateTime.Now,
                USRID = gUARDIAN.USRID
            };
            db.GUARDIANs.Add(gUARDIANnEW);
            try { db.SaveChanges(); ViewBag.GuardianAddMessage = "Guardian Added successfully."; }
            catch (Exception e) { Console.WriteLine(e); ViewBag.GuardianAddMessage = e.InnerException.InnerException.Message; }
            return RedirectToAction("Admission2", "Student", new { Std_id = Std_id });
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
                int PhotoId = 0;
                //viewModel.Success = HandleUpload(Request.Files[0].InputStream, name, size, type, PhotoId);
                PhotoId = HandleUpload(Request.Files[0].InputStream, name, size, type, PhotoId);
            }
            return Json(viewModel);
        }

        private int HandleUpload(Stream fileStream, string name, int size, string type, int PhotoId)
        {
            bool handled = false;

            try
            {
                // Convert image to buffered stream
                var imageBufferedStream = new BufferedStream(fileStream);
                byte[] documentBytes = new byte[imageBufferedStream.Length];
                imageBufferedStream.Read(documentBytes, 0, documentBytes.Length);


                if (PhotoId == 0 || PhotoId.Equals(null))
                {
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
                    PhotoId = databaseDocument.DocumentId;
                }
                else
                {
                    IMAGE_DOCUMENTS ImagetoUpdate = db.IMAGE_DOCUMENTS.Find(PhotoId);
                    ImagetoUpdate.CreatedOn = DateTime.Now;
                    ImagetoUpdate.FileContent = documentBytes;
                    ImagetoUpdate.Name = name;
                    ImagetoUpdate.Size = size;
                    ImagetoUpdate.Type = type;

                    db.Entry(ImagetoUpdate).State = EntityState.Modified;
                    handled = (db.SaveChanges() > 0);
                }

            }
            catch (Exception ex)
            {
                throw ex; // Oops, something went wrong, handle the exception
            }

            return PhotoId;
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
