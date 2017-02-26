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
            return View();
        }

        // GET: Fee Index
        public ActionResult Fees_Index()
        {
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
                                where ffc.IS_DEL.Equals("N") && ffc.IS_MSTR.Equals("Y")
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
                if (fINANCE_FEE_cATAGORY.BTCH_ID.Equals(-1))
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


        // GET: Finance/Delete/5
        public ActionResult Master_Category_Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FINANCE_FEE_CATGEORY fINANCE_FEE_CATEGORY = db.FINANCE_FEE_CATGEORY.Find(id);
            if (fINANCE_FEE_CATEGORY == null)
            {
                return HttpNotFound();
            }
            return View(fINANCE_FEE_CATEGORY);
        }

        // POST: Finance/Delete/5
        [HttpPost, ActionName("Master_Category_Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Master_Category_DeleteConfirmed(int id)
        {
            FINANCE_FEE_CATGEORY fINANCE_FEE_CATEGORY = db.FINANCE_FEE_CATGEORY.Find(id);
            fINANCE_FEE_CATEGORY.IS_DEL = "Y";
            db.SaveChanges();
            return RedirectToAction("Master_Fees");
        }

        // GET: Finance/Edit/5
        public ActionResult Master_Category_Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FINANCE_FEE_CATGEORY fINANCE_FEE_CATEGORY = db.FINANCE_FEE_CATGEORY.Find(id);
            if (fINANCE_FEE_CATEGORY == null)
            {
                return HttpNotFound();
            }
            List<SelectListItem> options = new SelectList(db.BATCHes.OrderBy(x => x.ID), "ID", "NAME", fINANCE_FEE_CATEGORY.BTCH_ID).ToList();
            // add the 'ALL' option
            options.Insert(0, new SelectListItem() { Value = "-1", Text = "ALL" });
            ViewBag.BTCH_ID = options;
            return View(fINANCE_FEE_CATEGORY);
        }

        // POST: Finance/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Master_Category_Edit([Bind(Include = "ID,NAME,DESCR,BTCH_ID,IS_DEL,IS_MSTR,CREATED_AT,UPDATED_AT")] FINANCE_FEE_CATGEORY fINANCE_FEE_CATEGORY)
        {
            if (ModelState.IsValid)
            {
                if (fINANCE_FEE_CATEGORY.BTCH_ID.Equals(-1))
                {
                    foreach (var entity in db.BATCHes.Select(s => new { s.ID, s.NAME, s.CRS_ID, s.START_DATE, s.END_DATE, s.IS_DEL, s.EMP_ID }).Distinct().Where(a => a.IS_DEL.Equals("N")).ToList())
                    {
                        var FF_cATAGORY = new FINANCE_FEE_CATGEORY() { NAME = fINANCE_FEE_CATEGORY.NAME, DESCR = fINANCE_FEE_CATEGORY.DESCR, BTCH_ID = entity.ID, IS_DEL = "N", IS_MSTR = "Y", CREATED_AT = System.DateTime.Now, UPDATED_AT = System.DateTime.Now };
                        db.FINANCE_FEE_CATGEORY.Add(FF_cATAGORY);
                        db.SaveChanges();
                    }
                }
                else
                {
                    var FF_cATAGORY = new FINANCE_FEE_CATGEORY() { NAME = fINANCE_FEE_CATEGORY.NAME, DESCR = fINANCE_FEE_CATEGORY.DESCR, BTCH_ID = fINANCE_FEE_CATEGORY.BTCH_ID, IS_DEL = "N", IS_MSTR = "Y", CREATED_AT = System.DateTime.Now, UPDATED_AT = System.DateTime.Now };
                    db.FINANCE_FEE_CATGEORY.Add(FF_cATAGORY);
                    db.SaveChanges();
                }
            }
            List<SelectListItem> options = new SelectList(db.BATCHes.OrderBy(x => x.ID), "ID", "NAME", fINANCE_FEE_CATEGORY.BTCH_ID).ToList();
            // add the 'ALL' option
            options.Insert(0, new SelectListItem() { Value = "-1", Text = "ALL" });
            ViewBag.BTCH_ID = options;
            return View(fINANCE_FEE_CATEGORY);
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
                        if (HtmlHelpers.ApplicationHelper.StringToIntList(fINANCE_FEE_pARTUCULAR.ADMSN_NO).Count() != 0)
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
                        else if (fINANCE_FEE_pARTUCULAR.STDNT_CAT_ID != -1)
                        {
                            var StdResult = from st in db.STUDENTs
                                            where (st.STDNT_CAT_ID == fINANCE_FEE_pARTUCULAR.STDNT_CAT_ID)
                                            select st;

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
                                            select new { Students = st, Batches = bt, FinanceFeeCategory = ffc };

                            foreach (var StdRecordToDelete in StdResult)
                            {
                                var CurrFinFeeParticular = from ffp in db.FINANCE_FEE_PARTICULAR
                                                           where ffp.FIN_FEE_CAT_ID == fINANCE_FEE_pARTUCULAR.FIN_FEE_CAT_ID
                                                           && ffp.STDNT_ID == StdRecordToDelete.Students.ID
                                                           select ffp;
                                if (CurrFinFeeParticular.Count() != 0)
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

        [HttpGet]
        public ActionResult Fee_Discounts(string sortOrder, string currentFilter, string BTCH_ID, int? page, string currentFilter2, string FINANCE_FEE_CATGEORY_ID)
        {
            List<SelectListItem> options = new SelectList(db.BATCHes.OrderBy(x => x.ID).Distinct(), "NAME", "NAME", BTCH_ID).ToList();
            // add the 'ALL' option
            options.Insert(0, new SelectListItem() { Value = "ALL", Text = "ALL" });
            ViewBag.BTCH_ID = options;

            List<SelectListItem> options2 = new SelectList(db.FINANCE_FEE_CATGEORY.OrderBy(x => x.NAME).Distinct(), "NAME", "NAME", FINANCE_FEE_CATGEORY_ID).ToList();
            // add the 'ALL' option
            options2.Insert(0, new SelectListItem() { Value = "ALL", Text = "ALL" });
            ViewBag.FINANCE_FEE_CATGEORY_ID = options2;

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (BTCH_ID != null)
            {
                if (!BTCH_ID.Equals("ALL"))
                {
                    page = 1;
                }
                else { BTCH_ID = currentFilter; }
            }
            else { BTCH_ID = currentFilter; }
            ViewBag.CurrentFilter = BTCH_ID;

            if (FINANCE_FEE_CATGEORY_ID != null)
            {
                if (!FINANCE_FEE_CATGEORY_ID.Equals("ALL"))
                {
                    page = 1;
                }
                else { FINANCE_FEE_CATGEORY_ID = currentFilter2; }
            }
            else { FINANCE_FEE_CATGEORY_ID = currentFilter2; }
            ViewBag.CurrentFilter2 = FINANCE_FEE_CATGEORY_ID;

            var Fee_discountSData = (from fd in db.FEE_DISCOUNT
                                     join ffc in db.FINANCE_FEE_CATGEORY on fd.FIN_FEE_CAT_ID equals ffc.ID
                                     join bt in db.BATCHes on ffc.BTCH_ID equals bt.ID
                                     join std in db.STUDENTs on fd.RCVR_ID equals std.ID into gm
                                     from substd in gm.DefaultIfEmpty()
                                     join cat in db.STUDENT_CATGEORY on substd.STDNT_CAT_ID equals cat.ID into gl
                                     from subcat in gl.DefaultIfEmpty()
                                     orderby fd.NAME
                                     select new Models.FeeDiscount { FeeDiscountData = fd, FinanceFeeCategoryData = ffc, BatchData = bt, StudentData = (substd == null ? null : substd), StudentCategoryData = (subcat == null ? null : subcat) }).Distinct();

            if (BTCH_ID != null)
            {
                if (!BTCH_ID.Equals("ALL"))
                {
                    Fee_discountSData = Fee_discountSData.Where(s => s.BatchData.NAME.Contains(BTCH_ID));
                }
            }
            if (FINANCE_FEE_CATGEORY_ID != null)
            {
                if (!FINANCE_FEE_CATGEORY_ID.Equals("ALL"))
                {
                    Fee_discountSData = Fee_discountSData.Where(s => s.FinanceFeeCategoryData.NAME.Contains(FINANCE_FEE_CATGEORY_ID));
                }
            }
            switch (sortOrder)
            {
                case "name_desc":
                    Fee_discountSData = Fee_discountSData.OrderByDescending(s => s.FeeDiscountData.NAME);
                    break;
                case "Date":
                    Fee_discountSData = Fee_discountSData.OrderBy(s => s.StudentData.ADMSN_DATE);
                    break;
                case "date_desc":
                    Fee_discountSData = Fee_discountSData.OrderByDescending(s => s.StudentData.ADMSN_DATE);
                    break;
                default:  // Name ascending 
                    Fee_discountSData = Fee_discountSData.OrderBy(s => s.FeeDiscountData.NAME);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(Fee_discountSData.ToPagedList(pageNumber, pageSize));
            //return View(db.USERS.ToList());
        }


        // GET: Finance/Create
        public ActionResult Fee_Discount_New()
        {
            var DiscountTypesList = new SelectList(new[]
                    {
                        new { ID = "Batch", Name = "Batch" },
                        new { ID = "Student Category", Name = "Student Category" },
                        new { ID = "Student", Name = "Student" },
                    },
                    "ID", "Name", "Batch");

            ViewData["TYPE"] = DiscountTypesList;

            List<SelectListItem> options2 = new SelectList(db.FINANCE_FEE_CATGEORY.OrderBy(x => x.NAME).Distinct(), "ID", "NAME").ToList();
            // add the 'ALL' option
            options2.Insert(0, new SelectListItem() { Value = "-1", Text = "ALL" });
            ViewBag.FIN_FEE_CAT_ID = options2;

            var query = from c in db.BATCHes
                        select c;
            List<SelectListItem> obj = new List<SelectListItem>();
            foreach (var item in query)
            {
                string BatchFullName = string.Concat(item.NAME, "-", Convert.ToDateTime(item.START_DATE).ToString("yyyy"), "-", Convert.ToDateTime(item.END_DATE).ToString("yyyy"));
                var result = new SelectListItem();
                result.Text = BatchFullName;
                result.Value = item.ID.ToString();
                obj.Add(result);
            }
            obj.Insert(0, new SelectListItem() { Value = "-1", Text = "ALL" });
            ViewBag.BATCH_ID = obj;

            List<SelectListItem> options4 = new SelectList(db.STUDENT_CATGEORY.OrderBy(x => x.NAME).Distinct(), "ID", "NAME").ToList();
            // add the 'ALL' option
            options4.Insert(0, new SelectListItem() { Value = "-1", Text = "ALL" });
            ViewBag.STUDENT_CATGEORY_ID = options4;

            List<SelectListItem> options5 = new SelectList(db.COURSEs.OrderBy(x => x.CRS_NAME).Distinct(), "ID", "CRS_NAME").ToList();
            // add the 'ALL' option
            options5.Insert(0, new SelectListItem() { Value = "-1", Text = "ALL" });
            ViewBag.CRS_NAME = options5;


            return View();
        }


        // POST: Finance/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Fee_Discount_New([Bind(Include = "ID,TYPE,NAME,RCVR_ID,FIN_FEE_CAT_ID,DISC,IS_AMT")] FEE_DISCOUNT fEEdISCOUNT, int? BATCH_ID, int? STUDENT_CATGEORY_ID, int? CRS_NAME, string ADMSN_NO)
        {
            if (ModelState.IsValid)
            {
                if (STUDENT_CATGEORY_ID.Equals(-1) && ADMSN_NO.Equals("") && !BATCH_ID.Equals(-1))
                {
                    var BatchQuery = from std in db.STUDENTs
                                     join c in db.BATCHes on std.BTCH_ID equals c.ID
                                     where c.ID == BATCH_ID
                                     select std;

                    foreach (var item in BatchQuery)
                    {
                        fEEdISCOUNT.RCVR_ID = item.ID;
                        db.FEE_DISCOUNT.Add(fEEdISCOUNT);
                    }
                    db.SaveChanges();
                }
                else if (!STUDENT_CATGEORY_ID.Equals(-1) && ADMSN_NO.Equals("") && !BATCH_ID.Equals(-1))
                {
                    var BatchQuery = from std in db.STUDENTs
                                     join c in db.BATCHes on std.BTCH_ID equals c.ID
                                     where c.ID == BATCH_ID && std.STDNT_CAT_ID == STUDENT_CATGEORY_ID
                                     select std;

                    foreach (var item in BatchQuery)
                    {
                        fEEdISCOUNT.RCVR_ID = item.ID;
                        db.FEE_DISCOUNT.Add(fEEdISCOUNT);
                    }
                    db.SaveChanges();
                }
                else if (STUDENT_CATGEORY_ID.Equals(-1) && !ADMSN_NO.Equals("") && !BATCH_ID.Equals(-1))
                {
                    if (HtmlHelpers.ApplicationHelper.StringToIntList(ADMSN_NO).Count() != 0)
                    {
                        foreach (var AdmissionNoList in HtmlHelpers.ApplicationHelper.StringToIntList(ADMSN_NO).ToList())
                        {
                            var StdResult = from u in db.STUDENTs where (u.ADMSN_NO == AdmissionNoList.ToString()) select u;
                            if (StdResult.Count() != 0)
                            {
                                var BatchQuery = from std in db.STUDENTs
                                                 join c in db.BATCHes on std.BTCH_ID equals c.ID
                                                 where c.ID == BATCH_ID && std.ADMSN_NO == AdmissionNoList.ToString()
                                                 select std;


                                foreach (var item in BatchQuery)
                                {
                                    fEEdISCOUNT.RCVR_ID = item.ID;
                                    db.FEE_DISCOUNT.Add(fEEdISCOUNT);
                                }
                                try { db.SaveChanges(); }
                                catch (Exception e) { Console.WriteLine(e); }
                            }


                        }
                    }
                }
                else
                {
                    db.FEE_DISCOUNT.Add(fEEdISCOUNT);
                    db.SaveChanges();
                }

                return RedirectToAction("Fee_Discounts");
            }

            List<SelectListItem> options2 = new SelectList(db.FINANCE_FEE_CATGEORY.OrderBy(x => x.NAME).Distinct(), "ID", "NAME", fEEdISCOUNT.FIN_FEE_CAT_ID).ToList();
            // add the 'ALL' option
            options2.Insert(0, new SelectListItem() { Value = "-1", Text = "ALL" });
            ViewBag.FIN_FEE_CAT_ID = options2;


            var query = from c in db.BATCHes
                        select new BATCH
                        {
                            ID = c.ID,
                            NAME = string.Concat(c.NAME, "-", Convert.ToDateTime(c.START_DATE).ToString("yyyy"), "-", Convert.ToDateTime(c.END_DATE).ToString("yyyy"))
                        };
            List<SelectListItem> obj = new List<SelectListItem>();
            foreach (var item in query)
            {
                var result = new SelectListItem();
                result.Text = item.NAME;
                result.Value = item.ID.ToString();
                obj.Add(result);
            }
            obj.Insert(0, new SelectListItem() { Value = "-1", Text = "ALL" });
            ViewBag.BATCH_ID = obj;

            List<SelectListItem> options4 = new SelectList(db.STUDENT_CATGEORY.OrderBy(x => x.NAME).Distinct(), "ID", "NAME", STUDENT_CATGEORY_ID).ToList();
            // add the 'ALL' option
            options4.Insert(0, new SelectListItem() { Value = "-1", Text = "ALL" });
            ViewBag.STUDENT_CATGEORY_ID = options4;

            List<SelectListItem> options5 = new SelectList(db.COURSEs.OrderBy(x => x.CRS_NAME).Distinct(), "ID", "CRS_NAME", CRS_NAME).ToList();
            // add the 'ALL' option
            options5.Insert(0, new SelectListItem() { Value = "-1", Text = "ALL" });
            ViewBag.CRS_NAME = options5;

            return View();
        }


        // GET: Finance/Edit/5
        public ActionResult Edit_Fee_Discount(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FEE_DISCOUNT fEE_DISCOUNT = db.FEE_DISCOUNT.Find(id);
            if (fEE_DISCOUNT == null)
            {
                return HttpNotFound();
            }
            ViewBag.RCVR_ID = new SelectList(db.STUDENTs, "ID", "ADMSN_NO", fEE_DISCOUNT.RCVR_ID);
            ViewBag.FIN_FEE_CAT_ID = new SelectList(db.FINANCE_FEE_CATGEORY, "ID", "NAME", fEE_DISCOUNT.FIN_FEE_CAT_ID);
            return View(fEE_DISCOUNT);
        }

        // POST: Finance/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_Fee_Discount([Bind(Include = "ID,TYPE,NAME,RCVR_ID,FIN_FEE_CAT_ID,DISC,IS_AMT")] FEE_DISCOUNT fEE_DISCOUNT)
        {
            if (ModelState.IsValid)
            {
                var BatchQuery = from fd in db.FEE_DISCOUNT
                                 where fd.NAME == fEE_DISCOUNT.NAME
                                 select fd;


                foreach (var item in BatchQuery)
                {
                    item.DISC = fEE_DISCOUNT.DISC;
                    db.Entry(item).State = EntityState.Modified;
                }
                db.SaveChanges();
                return RedirectToAction("Fee_Discounts");
            }
            ViewBag.RCVR_ID = new SelectList(db.STUDENTs, "ID", "ADMSN_NO", fEE_DISCOUNT.RCVR_ID);
            ViewBag.FIN_FEE_CAT_ID = new SelectList(db.FINANCE_FEE_CATGEORY, "ID", "NAME", fEE_DISCOUNT.FIN_FEE_CAT_ID);
            return View(fEE_DISCOUNT);
        }


        // GET: Finance/Delete/5
        public ActionResult Delete_Fee_Discount(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FEE_DISCOUNT fEE_DISCOUNT = db.FEE_DISCOUNT.Find(id);
            if (fEE_DISCOUNT == null)
            {
                return HttpNotFound();
            }
            return View(fEE_DISCOUNT);
        }

        // POST: Finance/Delete/5
        [HttpPost, ActionName("Delete_Fee_Discount")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete_Fee_DiscountConfirmed(int id)
        {
            FEE_DISCOUNT fEE_DISCOUNT = db.FEE_DISCOUNT.Find(id);
            db.FEE_DISCOUNT.Remove(fEE_DISCOUNT);
            db.SaveChanges();
            return RedirectToAction("Fee_Discounts");
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
