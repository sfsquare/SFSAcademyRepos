using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SFSAcademy;

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
            return View(db.BATCHes.ToList());
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
                db.BATCHes.Add(bATCH);
                db.SaveChanges();
                return RedirectToAction("Index");
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
                return RedirectToAction("Index");
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
    }
}
