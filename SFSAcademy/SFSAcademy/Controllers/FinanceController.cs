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
    public class FinanceController : Controller
    {
        private SFSAcademyEntities db = new SFSAcademyEntities();

        // GET: Finance
        public ActionResult Index()
        {
            //var fINANCE_FEE = db.FINANCE_FEE.Include(f => f.FINANCE_FEE_COLLECTION).Include(f => f.FINANCE_TRANSACTION1).Include(f => f.STUDENT);
            return View();
        }

        // GET: Fee Index
        public ActionResult Fees_Index()
        {
            //var fINANCE_FEE = db.FINANCE_FEE.Include(f => f.FINANCE_FEE_COLLECTION).Include(f => f.FINANCE_TRANSACTION1).Include(f => f.STUDENT);
            return View();
        }

        // GET: Fee Index
        public ActionResult Master_Fees()
        {
            List<SelectListItem> options = new SelectList(db.BATCHes.OrderBy(x => x.ID), "ID", "NAME").ToList();
            // add the 'ALL' option
            options.Insert(0, new SelectListItem() { Value = "-1", Text = "ALL" });
            ViewBag.searchString = options;
            return View();
        }

        // GET: Fee Index
        public ActionResult _Master_Category_List(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null && !searchString.Equals("-1"))
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

            var FeeCategoryS = (from ffc in db.FINANCE_FEE_CATGEORY
                                join b in db.BATCHes on ffc.BTCH_ID equals b.ID into gi
                                from subb in gi.DefaultIfEmpty()
                                join ffp in db.FINANCE_FEE_PARTICULAR on ffc.ID equals ffp.FIN_FEE_CAT_ID into gj
                                from subffc in gj.DefaultIfEmpty()
                                join sc in db.STUDENT_CATGEORY on subffc.STDNT_CAT_ID equals sc.ID into gk
                                from subsc in gk.DefaultIfEmpty()
                                join st in db.STUDENTs on subffc.STDNT_ID equals st.ID into gl
                                from subst in gl.DefaultIfEmpty()
                                    //where st.ID == id
                                orderby ffc.NAME
                                select new Models.FeeCategory { FinanceFeeCategoryData = ffc, BatchData = (subb == null ? null : subb), FinanceFeeParticularData = (subffc == null ? null : subffc), StudentCategoryData = (subsc == null ? null : subsc), StudentData = (subst == null ? null : subst) }).Distinct();

            if (!String.IsNullOrEmpty(searchString) && !searchString.Equals("ALL"))
            {
                FeeCategoryS = FeeCategoryS.Where(s => s.BatchData.NAME.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    FeeCategoryS = FeeCategoryS.OrderByDescending(s => s.FinanceFeeCategoryData.NAME);
                    break;
                case "Date":
                    FeeCategoryS = FeeCategoryS.OrderBy(s => s.FinanceFeeCategoryData.CREATED_AT);
                    break;
                case "date_desc":
                    FeeCategoryS = FeeCategoryS.OrderByDescending(s => s.FinanceFeeCategoryData.CREATED_AT);
                    break;
                default:  // Name ascending 
                    FeeCategoryS = FeeCategoryS.OrderBy(s => s.FinanceFeeCategoryData.NAME);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(FeeCategoryS.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult _Master_Category_Create_Form()
        {
            List<SelectListItem> options = new SelectList(db.BATCHes.OrderBy(x => x.ID), "ID", "NAME").ToList();
            // add the 'ALL' option
            options.Insert(0, new SelectListItem() { Value = "-1", Text = "ALL" });
            ViewBag.BTCH_ID = options;
            return View();
        }

        // POST: Finance/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _Master_Category_Create_Form([Bind(Include = "ID,NAME,DESCR,BTCH_ID,IS_DEL,IS_MSTR,CREATED_AT,UPDATED_AT")] FINANCE_FEE_CATGEORY fINANCE_FEE_cATAGORY)
        {

            if (ModelState.IsValid)
            {
                if(fINANCE_FEE_cATAGORY.BTCH_ID.Equals(-1))
                {
                    foreach (var entity in db.BATCHes.Select(s => new { s.ID, s.NAME, s.CRS_ID, s.START_DATE, s.END_DATE, s.IS_DEL, s.EMP_ID }).Distinct().Where(a => a.IS_DEL.Equals("N")).ToList())
                    {
                        var FF_cATAGORY = new FINANCE_FEE_CATGEORY() { NAME = fINANCE_FEE_cATAGORY.NAME, DESCR = fINANCE_FEE_cATAGORY.DESCR, BTCH_ID = entity.ID, IS_DEL = "N", IS_MSTR = "Y", CREATED_AT = System.DateTime.Now, UPDATED_AT = System.DateTime.Now };
                        db.FINANCE_FEE_CATGEORY.Add(FF_cATAGORY);
                        db.SaveChanges();
                    }
                }
                else
                {
                    var FF_cATAGORY = new FINANCE_FEE_CATGEORY() { NAME = fINANCE_FEE_cATAGORY.NAME, DESCR = fINANCE_FEE_cATAGORY.DESCR, BTCH_ID = fINANCE_FEE_cATAGORY.BTCH_ID, IS_DEL = "N", IS_MSTR = "Y", CREATED_AT = System.DateTime.Now, UPDATED_AT = System.DateTime.Now };
                    db.FINANCE_FEE_CATGEORY.Add(FF_cATAGORY);
                    db.SaveChanges();
                }
                
                return RedirectToAction("Master_Fees");
            }
            List<SelectListItem> options = new SelectList(db.BATCHes, "ID", "NAME", fINANCE_FEE_cATAGORY.BTCH_ID).ToList();
            // add the 'ALL' option
            options.Add(new SelectListItem() { Value = "-1", Text = "ALL" });
            ViewBag.BTCH_ID = options;
            return View(fINANCE_FEE_cATAGORY);
        }

        [HttpGet]
        public ActionResult Fees_Particulars_New()
        {
            var fEEcATEGORYaACCESS = (from ffc in db.FINANCE_FEE_CATGEORY
                                      join b in db.BATCHes on ffc.BTCH_ID equals b.ID into gi
                                      from subb in gi.DefaultIfEmpty()
                                      select new Models.SelectFeeCategory { FinanceFeeCategoryData = ffc, BatchData = (subb == null ? null : subb), Selected = false }).OrderBy(g => g.FinanceFeeCategoryData.ID).ToList();

            return View(fEEcATEGORYaACCESS);
        }

        [HttpGet]
        public ActionResult _Fees_Particulars_Create()
        {
            List<SelectListItem> sCategory = new SelectList(db.STUDENT_CATGEORY.OrderBy(x => x.ID), "ID", "NAME").ToList();
            // add the 'ALL' option
            sCategory.Insert(0, new SelectListItem() { Value = "-1", Text = "ALL" });
            ViewBag.STDNT_CAT_ID = sCategory;
            ViewBag.FeeCatError = TempData["FeeCatError"];
            return View();
        }



        // POST: Finance/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _Fees_Particulars_Create([Bind(Include = "ID,NAME,DESCR,AMT,FIN_FEE_CAT_ID,STDNT_CAT_ID,ADMSN_NO,STDNT_ID,IS_DEL,CREATED_AT,UPDATED_AT")] FINANCE_FEE_PARTICULAR fINANCE_FEE_pARTUCULAR, IList<SelectFeeCategory> model, string FeePartOption)
        {

            if (ModelState.IsValid)
            {
                int FeeCatCount = 0;
                foreach (SelectFeeCategory item in model)
                {
                    if (item.Selected)
                    {
                        FeeCatCount++;
                        fINANCE_FEE_pARTUCULAR.FIN_FEE_CAT_ID = item.FinanceFeeCategoryData.ID;
                        if(HtmlHelpers.ApplicationHelper.StringToIntList(fINANCE_FEE_pARTUCULAR.ADMSN_NO).Count() != 0)
                        {
                            foreach (var AdmissionNoList in HtmlHelpers.ApplicationHelper.StringToIntList(fINANCE_FEE_pARTUCULAR.ADMSN_NO).ToList())
                            {
                                var StdResult = from u in db.STUDENTs where (u.ADMSN_NO == AdmissionNoList.ToString()) select u;
                                if (StdResult.Count() != 0)
                                {
                                    var CurrFinFeeParticular = from ffp in db.FINANCE_FEE_PARTICULAR
                                                               where ffp.FIN_FEE_CAT_ID == fINANCE_FEE_pARTUCULAR.FIN_FEE_CAT_ID
                                                               && ffp.ADMSN_NO == AdmissionNoList.ToString()
                                                               select ffp;

                                    foreach (var detail in CurrFinFeeParticular)
                                    {
                                        db.FINANCE_FEE_PARTICULAR.Remove(detail);
                                    }
                                    try { db.SaveChanges(); }
                                    catch (Exception e) { Console.WriteLine(e); }

                                    fINANCE_FEE_pARTUCULAR.ADMSN_NO = AdmissionNoList.ToString();
                                    fINANCE_FEE_pARTUCULAR.STDNT_ID = StdResult.FirstOrDefault().ID;
                                    db.FINANCE_FEE_PARTICULAR.Add(fINANCE_FEE_pARTUCULAR);
                                    db.SaveChanges();
                                }                                                            


                            }
                        }
                        else if(fINANCE_FEE_pARTUCULAR.STDNT_CAT_ID != -1)
                        {
                            var StdResult = from st in db.STUDENTs                                          
                                            where (st.STDNT_CAT_ID == fINANCE_FEE_pARTUCULAR.STDNT_CAT_ID) select st;

                            foreach (var StdRecordToDelete in StdResult)
                            {
                                var CurrFinFeeParticular = from ffp in db.FINANCE_FEE_PARTICULAR
                                                           where ffp.FIN_FEE_CAT_ID == fINANCE_FEE_pARTUCULAR.FIN_FEE_CAT_ID
                                                           && ffp.STDNT_ID == StdRecordToDelete.ID
                                                           select ffp;

                                foreach (var detail in CurrFinFeeParticular)
                                {
                                    db.FINANCE_FEE_PARTICULAR.Remove(detail);
                                }
                            }
                            try { db.SaveChanges(); }
                            catch (Exception e) { Console.WriteLine(e); }
                            foreach (var StdRecordToDelete in StdResult)
                            {

                                fINANCE_FEE_pARTUCULAR.ADMSN_NO = StdRecordToDelete.ADMSN_NO;
                                fINANCE_FEE_pARTUCULAR.STDNT_ID = StdRecordToDelete.ID;
                                db.FINANCE_FEE_PARTICULAR.Add(fINANCE_FEE_pARTUCULAR);
                            }
                            db.SaveChanges();

                        }
                        else
                        {
                            var StdResult = from st in db.STUDENTs
                                            join bt in db.BATCHes on st.BTCH_ID equals bt.ID
                                            join ffc in db.FINANCE_FEE_CATGEORY on bt.ID equals ffc.BTCH_ID 
                                            where (ffc.ID == item.FinanceFeeCategoryData.ID)
                                            select new { Students = st, Batches = bt, FinanceFeeCategory = ffc};

                            foreach (var StdRecordToDelete in StdResult)
                            {
                                var CurrFinFeeParticular = from ffp in db.FINANCE_FEE_PARTICULAR
                                                           where ffp.FIN_FEE_CAT_ID == fINANCE_FEE_pARTUCULAR.FIN_FEE_CAT_ID
                                                           && ffp.STDNT_ID == StdRecordToDelete.Students.ID
                                                           select ffp;
                                if(CurrFinFeeParticular.Count() !=0)
                                {
                                    db.FINANCE_FEE_PARTICULAR.Remove(CurrFinFeeParticular.FirstOrDefault());
                                }                              
                            }
                            db.SaveChanges();

                            foreach (var StdRecordToDelete in StdResult)
                            {
                                fINANCE_FEE_pARTUCULAR.ADMSN_NO = StdRecordToDelete.Students.ADMSN_NO;
                                fINANCE_FEE_pARTUCULAR.STDNT_ID = StdRecordToDelete.Students.ID;
                                fINANCE_FEE_pARTUCULAR.STDNT_CAT_ID = StdRecordToDelete.Students.STDNT_CAT_ID;
                                db.FINANCE_FEE_PARTICULAR.Add(fINANCE_FEE_pARTUCULAR);
                            }
                            db.SaveChanges();
                        }
                    }
                }
                if (FeeCatCount.Equals(0))
                {
                    TempData["FeeCatError"] = "Please select valid Fee Category";                   
                    return RedirectToAction("Fees_Particulars_New");
                }
                return RedirectToAction("Master_Fees");
            }
            List<SelectListItem> sCategory = new SelectList(db.STUDENT_CATGEORY.OrderBy(x => x.ID), "ID", "NAME", fINANCE_FEE_pARTUCULAR.STDNT_CAT_ID).ToList();
            // add the 'ALL' option
            sCategory.Insert(0, new SelectListItem() { Value = "-1", Text = "ALL" });
            ViewBag.STDNT_CAT_ID = sCategory;
            return View(fINANCE_FEE_pARTUCULAR);
        }

   


        // GET: Finance/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FINANCE_FEE fINANCE_FEE = db.FINANCE_FEE.Find(id);
            if (fINANCE_FEE == null)
            {
                return HttpNotFound();
            }
            return View(fINANCE_FEE);
        }



        // GET: Finance/Create
        public ActionResult Create()
        {
            ViewBag.FEE_CLCT_ID = new SelectList(db.FINANCE_FEE_COLLECTION, "ID", "NAME");
            ViewBag.TRAN_ID = new SelectList(db.FINANCE_TRANSACTION, "ID", "TIL");
            ViewBag.STDNT_ID = new SelectList(db.STUDENTs, "ID", "ADMSN_NO");
            return View();
        }

        // POST: Finance/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FEE_CLCT_ID,TRAN_ID,STDNT_ID,IS_PD")] FINANCE_FEE fINANCE_FEE)
        {
            if (ModelState.IsValid)
            {
                db.FINANCE_FEE.Add(fINANCE_FEE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FEE_CLCT_ID = new SelectList(db.FINANCE_FEE_COLLECTION, "ID", "NAME", fINANCE_FEE.FEE_CLCT_ID);
            ViewBag.TRAN_ID = new SelectList(db.FINANCE_TRANSACTION, "ID", "TIL", fINANCE_FEE.TRAN_ID);
            ViewBag.STDNT_ID = new SelectList(db.STUDENTs, "ID", "ADMSN_NO", fINANCE_FEE.STDNT_ID);
            return View(fINANCE_FEE);
        }

        // GET: Finance/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FINANCE_FEE fINANCE_FEE = db.FINANCE_FEE.Find(id);
            if (fINANCE_FEE == null)
            {
                return HttpNotFound();
            }
            ViewBag.FEE_CLCT_ID = new SelectList(db.FINANCE_FEE_COLLECTION, "ID", "NAME", fINANCE_FEE.FEE_CLCT_ID);
            ViewBag.TRAN_ID = new SelectList(db.FINANCE_TRANSACTION, "ID", "TIL", fINANCE_FEE.TRAN_ID);
            ViewBag.STDNT_ID = new SelectList(db.STUDENTs, "ID", "ADMSN_NO", fINANCE_FEE.STDNT_ID);
            return View(fINANCE_FEE);
        }

        // POST: Finance/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FEE_CLCT_ID,TRAN_ID,STDNT_ID,IS_PD")] FINANCE_FEE fINANCE_FEE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fINANCE_FEE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FEE_CLCT_ID = new SelectList(db.FINANCE_FEE_COLLECTION, "ID", "NAME", fINANCE_FEE.FEE_CLCT_ID);
            ViewBag.TRAN_ID = new SelectList(db.FINANCE_TRANSACTION, "ID", "TIL", fINANCE_FEE.TRAN_ID);
            ViewBag.STDNT_ID = new SelectList(db.STUDENTs, "ID", "ADMSN_NO", fINANCE_FEE.STDNT_ID);
            return View(fINANCE_FEE);
        }

        // GET: Finance/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FINANCE_FEE fINANCE_FEE = db.FINANCE_FEE.Find(id);
            if (fINANCE_FEE == null)
            {
                return HttpNotFound();
            }
            return View(fINANCE_FEE);
        }

        // POST: Finance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FINANCE_FEE fINANCE_FEE = db.FINANCE_FEE.Find(id);
            db.FINANCE_FEE.Remove(fINANCE_FEE);
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
