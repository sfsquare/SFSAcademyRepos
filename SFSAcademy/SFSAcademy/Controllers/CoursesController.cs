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
    public class CoursesController : Controller
    {
        private SFSAcademyEntities db = new SFSAcademyEntities();

        // GET: Courses
        public ActionResult Index()
        {
            return View();
        }

        // GET: Courses
        public ActionResult ManageCourse()
        {
            return View(db.COURSEs.Where(s=> s.IS_DEL == "N").ToList());
        }
        // GET: Courses/Details/5

        // GET: Courses
        public ActionResult ManageBatches()
        {
            return View(db.BATCHes.ToList());
        }
        // GET: Courses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COURSE cOURSE = db.COURSEs.Find(id);
            if (cOURSE == null)
            {
                return HttpNotFound();
            }
            return View(cOURSE);
        }

        // GET: Courses/Create
        public ActionResult Create()
        {

            DateTime PDate = Convert.ToDateTime(System.DateTime.Now);
            ViewBag.ReturnDate = PDate.ToShortDateString();
            DateTime PDate2 = Convert.ToDateTime(System.DateTime.Now.AddYears(1));
            ViewBag.ReturnDate2 = PDate2.ToShortDateString();
            ViewBag.BATCH_NAME = "";
            var GradingLevelList = new SelectList(new[]
        {
                        new { ID = "Normal", Name = "Normal" },
                        new { ID = "GPA", Name = "GPA" },
                        new { ID = "CWA", Name = "CWA" },
                        new { ID = "CCE", Name = "CCE" },
                        new { ID = "ICSE", Name = "ICSE" },
                    },
                "ID", "Name", "Normal");

            ViewData["GRADING_TYPE"] = GradingLevelList;


            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CRS_NAME,CODE,SECTN_NAME,IS_DEL,CREATED_AT,UPDATED_AT,GRADING_TYPE")] COURSE cOURSE, string BATCH_NAME, DateTime ReturnDate, DateTime ReturnDate2)
        {
            if (ModelState.IsValid)
            {
                cOURSE.IS_DEL = "N";
                cOURSE.CREATED_AT = System.DateTime.Now;
                cOURSE.UPDATED_AT = System.DateTime.Now;
                db.COURSEs.Add(cOURSE);
                db.SaveChanges();

                BATCH bATCH = new BATCH();
                bATCH.NAME = BATCH_NAME;
                bATCH.START_DATE = ReturnDate;
                bATCH.END_DATE = ReturnDate2;
                bATCH.CRS_ID = cOURSE.ID;
                bATCH.IS_DEL = "N";
                db.BATCHes.Add(bATCH);
                db.SaveChanges();
                ViewBag.Message = "Course created successfully!";

                DateTime PDate = Convert.ToDateTime(ReturnDate);
                ViewBag.ReturnDate = PDate.ToShortDateString();
                DateTime PDate2 = Convert.ToDateTime(ReturnDate2);
                ViewBag.ReturnDate2 = PDate2.ToShortDateString();
                ViewBag.BATCH_NAME = BATCH_NAME;
                var GradingLevelList = new SelectList(new[]
            {
                        new { ID = "Normal", Name = "Normal" },
                        new { ID = "GPA", Name = "GPA" },
                        new { ID = "CWA", Name = "CWA" },
                        new { ID = "CCE", Name = "CCE" },
                        new { ID = "ICSE", Name = "ICSE" },
                    },
                    "ID", "Name", "Normal");

                ViewData["GRADING_TYPE"] = GradingLevelList;
                return View();
            }

            ViewBag.Message = "There is some issue adding course.";
            return View(cOURSE);
        }

        // GET: Courses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COURSE cOURSE = db.COURSEs.Find(id);
            if (cOURSE == null)
            {
                return HttpNotFound();
            }
            return View(cOURSE);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CRS_NAME,CODE,SECTN_NAME,IS_DEL,CREATED_AT,UPDATED_AT,GRADING_TYPE")] COURSE cOURSE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cOURSE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cOURSE);
        }

        // GET: Courses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COURSE cOURSE = db.COURSEs.Find(id);
            if (cOURSE == null)
            {
                return HttpNotFound();
            }
            return View(cOURSE);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var BatchVal = (from C in db.BATCHes
                               where C.CRS_ID == id
                               select new { C}).ToList();

            /*var GuardianVal = (from C in db.GUARDIANs
                               where C.WARD_ID == Std_id
                               select new Models.SelectGuardian { GuardianList = C }).ToList();

            return View(GuardianVal);*/

            if(BatchVal.Count.Equals(0))
            {
                COURSE cOURSE = db.COURSEs.Find(id);
                cOURSE.IS_DEL = "Y";
                cOURSE.UPDATED_AT = System.DateTime.Now;
                db.Entry(cOURSE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ManageCourse");
            }
            else
            {
                ViewBag.Message = "Batch related to this course needs to be deleted first!";
                return View();
            }

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
