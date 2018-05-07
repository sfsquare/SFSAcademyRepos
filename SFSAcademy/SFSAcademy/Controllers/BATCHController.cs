using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SFSAcademy;
using PagedList;
using SFSAcademy.Models;
using System.Web.UI;

namespace SFSAcademy.Controllers
{
    public class BATCHController : Controller
    {
        private SFSAcademyEntities db = new SFSAcademyEntities();

        // GET: BATCH
        public ActionResult Index()
        {
            var bATCHes = db.BATCHes.Include(b => b.COURSE);
            return View(bATCHes.ToList());
        }

        // GET: Batches
        public ActionResult ManageBatches()
        {
            List<SelectListItem> options = new SelectList(db.COURSEs.OrderBy(x => x.ID), "ID", "CRS_NAME").ToList();
            //add the 'ALL' option
            options.Insert(0, new SelectListItem() { Value = "-1", Text = "ALL" });
            ViewBag.searchString = options;
            return View();
        }

        // GET: Fee Index
        public ActionResult _Update_Batch(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null && !searchString.Equals("-1"))
            {
                page = 1;
                ///As Drop down list sends Id, we will ahve to convert this to text which is different from text box
                int searchStringId = Convert.ToInt32(searchString);
                searchString = db.COURSEs.Find(searchStringId).CODE.ToString();
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var CourseBatchS = (from cs in db.COURSEs
                                join bt in db.BATCHes on cs.ID equals bt.CRS_ID 
                                where cs.IS_DEL.Equals("N") && bt.IS_DEL.Equals("N")
                                orderby cs.CODE
                                select new Models.CoursesBatch { CourseData = cs, BatchData = bt}).Distinct();

            if (!String.IsNullOrEmpty(searchString) && !searchString.Equals("ALL"))
            {
                CourseBatchS = CourseBatchS.Where(s => s.CourseData.CODE.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    CourseBatchS = CourseBatchS.OrderByDescending(s => s.BatchData.NAME);
                    break;
                case "Date":
                    CourseBatchS = CourseBatchS.OrderBy(s => s.BatchData.START_DATE);
                    break;
                case "date_desc":
                    CourseBatchS = CourseBatchS.OrderByDescending(s => s.BatchData.START_DATE);
                    break;
                default:  // Name ascending 
                    CourseBatchS = CourseBatchS.OrderBy(s => s.BatchData.NAME);
                    break;
            }

            int pageSize = 50;
            int pageNumber = (page ?? 1);
            return View(CourseBatchS.ToPagedList(pageNumber, pageSize));
        }

        // GET: BATCH/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BATCH bATCH = db.BATCHes.Find(id);
            if (bATCH == null)
            {
                return HttpNotFound();
            }
            return View(bATCH);
        }

        // GET: BATCH/Create
        public ActionResult Create()
        {
            ViewBag.CRS_ID = new SelectList(db.COURSEs, "ID", "CRS_NAME");
            ViewBag.EMP_ID = new SelectList(db.EMPLOYEEs, "ID", "EMP_NUM");
            return View();
        }

        // POST: BATCH/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NAME,CRS_ID,START_DATE,END_DATE,IS_DEL,EMP_ID")] BATCH bATCH)
        {
            if (ModelState.IsValid)
            {
                bATCH.IS_DEL = "N";
                bATCH.EMP_ID = Convert.ToInt32(this.Session["UserId"]);
                db.BATCHes.Add(bATCH);
                db.SaveChanges();
                return RedirectToAction("ManageBatches");
            }

            ViewBag.CRS_ID = new SelectList(db.COURSEs, "ID", "CRS_NAME", bATCH.CRS_ID);
            return View(bATCH);
        }

        // GET: BATCH/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BATCH bATCH = db.BATCHes.Find(id);
            if (bATCH == null)
            {
                return HttpNotFound();
            }
            ViewBag.CRS_ID = new SelectList(db.COURSEs, "ID", "CRS_NAME", bATCH.CRS_ID);
            return View(bATCH);
        }

        // POST: BATCH/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NAME,CRS_ID,START_DATE,END_DATE,IS_DEL,EMP_ID")] BATCH bATCH)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bATCH).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ManageBatches");
            }
            ViewBag.CRS_ID = new SelectList(db.COURSEs, "ID", "CRS_NAME", bATCH.CRS_ID);
            return View(bATCH);
        }

        // GET: BATCH/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BATCH bATCH = db.BATCHes.Find(id);
            if (bATCH == null)
            {
                return HttpNotFound();
            }
            return View(bATCH);
        }

        // POST: BATCH/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BATCH bATCH = db.BATCHes.Find(id);
            db.BATCHes.Remove(bATCH);
            db.SaveChanges();
            return RedirectToAction("ManageBatches");
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
