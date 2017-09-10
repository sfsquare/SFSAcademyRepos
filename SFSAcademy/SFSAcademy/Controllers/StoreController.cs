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
using SFSAcademy.HtmlHelpers;
using System.IO;
using iTextSharp.text;

namespace SFSAcademy.Controllers
{
    public class StoreController : Controller
    {
        private SFSAcademyEntities db = new SFSAcademyEntities();

        // GET: Store
        public ActionResult Index()
        {
            var sTORE_PRODUCTS = db.STORE_PRODUCTS.Include(s => s.STORE_CATEGORY);
            return View(sTORE_PRODUCTS.ToList());
        }

        // GET: Store/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STORE_PRODUCTS sTORE_PRODUCTS = db.STORE_PRODUCTS.Find(id);
            if (sTORE_PRODUCTS == null)
            {
                return HttpNotFound();
            }
            return View(sTORE_PRODUCTS);
        }

        // GET: Store/Create
        public ActionResult Create()
        {
            ViewBag.CATEGORY_ID = new SelectList(db.STORE_CATEGORY, "ID", "NAME");
            return View();
        }

        // POST: Store/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PRODUCT_ID,NAME,CATEGORY_ID,BRAND,TOTAL_UNIT,TOTAL_COST,COST_PER_UNIT,SELL_PRICE_PER_UNIT,PURCHASED_ON,PURCHASED_THROUGH,PAID_BY,UNIT_LEFT,IS_ACT,IS_DEL,CREATED_AT,UPDATED_AT")] STORE_PRODUCTS sTORE_PRODUCTS)
        {
            if (ModelState.IsValid)
            {
                sTORE_PRODUCTS.IS_DEL = "N";
                sTORE_PRODUCTS.CREATED_AT = DateTime.Now;
                sTORE_PRODUCTS.UPDATED_AT = DateTime.Now;
                db.STORE_PRODUCTS.Add(sTORE_PRODUCTS);
                db.SaveChanges();
                return RedirectToAction("ViewAll");
            }

            ViewBag.CATEGORY_ID = new SelectList(db.STORE_CATEGORY, "ID", "NAME", sTORE_PRODUCTS.CATEGORY_ID);
            return View(sTORE_PRODUCTS);
        }

        // GET: Store/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STORE_PRODUCTS sTORE_PRODUCTS = db.STORE_PRODUCTS.Find(id);
            if (sTORE_PRODUCTS == null)
            {
                return HttpNotFound();
            }
            ViewBag.CATEGORY_ID = new SelectList(db.STORE_CATEGORY, "ID", "NAME", sTORE_PRODUCTS.CATEGORY_ID);
            ViewBag.PURCHASED_THROUGH = sTORE_PRODUCTS.PURCHASED_THROUGH;
            ViewBag.PAID_BY = sTORE_PRODUCTS.PAID_BY;
            DateTime PDate = Convert.ToDateTime(sTORE_PRODUCTS.PURCHASED_ON);
            ViewBag.PURCHASED_ON = PDate.ToShortDateString();
            return View(sTORE_PRODUCTS);
        }

        // POST: Store/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PRODUCT_ID,NAME,CATEGORY_ID,BRAND,TOTAL_UNIT,TOTAL_COST,COST_PER_UNIT,SELL_PRICE_PER_UNIT,PURCHASED_ON,PURCHASED_THROUGH,PAID_BY,UNIT_LEFT,IS_ACT,IS_DEL,CREATED_AT,UPDATED_AT")] STORE_PRODUCTS sTORE_PRODUCTS)
        {
            if (ModelState.IsValid)
            {
                sTORE_PRODUCTS.IS_DEL = "N";
                sTORE_PRODUCTS.UPDATED_AT = DateTime.Now;
                db.Entry(sTORE_PRODUCTS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ViewAll");
            }
            ViewBag.CATEGORY_ID = new SelectList(db.STORE_CATEGORY, "ID", "NAME", sTORE_PRODUCTS.CATEGORY_ID);
            ViewBag.PURCHASED_THROUGH = sTORE_PRODUCTS.PURCHASED_THROUGH;
            ViewBag.PAID_BY = sTORE_PRODUCTS.PAID_BY;
            DateTime PDate = Convert.ToDateTime(sTORE_PRODUCTS.PURCHASED_ON);
            ViewBag.PURCHASED_ON = PDate.ToString("dd/mm/yyyy");
            return View(sTORE_PRODUCTS);
        }

        // GET: Store/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STORE_PRODUCTS sTORE_PRODUCTS = db.STORE_PRODUCTS.Find(id);
            if (sTORE_PRODUCTS == null)
            {
                return HttpNotFound();
            }
            return View(sTORE_PRODUCTS);
        }

        // POST: Store/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            STORE_PRODUCTS sTORE_PRODUCTS = db.STORE_PRODUCTS.Find(id);
            sTORE_PRODUCTS.IS_DEL = "Y";
            sTORE_PRODUCTS.IS_ACT = "N";
            sTORE_PRODUCTS.UPDATED_AT = DateTime.Now;
            db.Entry(sTORE_PRODUCTS).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ViewAll");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        // GET: Student
        public ActionResult ViewAll()
        {
            ViewBag.CartItems = (from t in db.STORE_PURCHAGE_CART
                                 select t).Count();
            return View(db.STORE_CATEGORY.ToList());
        }

        // GET: Store Products
        public ActionResult ListAllProducts(string sortOrder, string currentFilter, string searchString, int? page)
        {

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null && searchString != "")
            {
                page = 1;
                ///As Drop down list sends Id, we will ahve to convert this to text which is different from text box
                int searchStringId = Convert.ToInt32(searchString);
                searchString = db.STORE_CATEGORY.Find(searchStringId).NAME.ToString();
            } 
            else 
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var ProductS = (from pd in db.STORE_PRODUCTS
                            join ct in db.STORE_CATEGORY on pd.CATEGORY_ID equals ct.ID
                            orderby pd.NAME, ct.NAME
                            where pd.IS_DEL == "N"
                            select new Models.Products { ProductData = pd, CategoryData = ct }).Distinct();

            if (!String.IsNullOrEmpty(searchString))
            {
                ProductS = ProductS.Where(s => s.CategoryData.NAME.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    ProductS = ProductS.OrderByDescending(s => s.ProductData.NAME);
                    break;
                case "Date":
                    ProductS = ProductS.OrderBy(s => s.ProductData.PURCHASED_ON);
                    break;
                case "date_desc":
                    ProductS = ProductS.OrderByDescending(s => s.ProductData.PURCHASED_ON);
                    break;
                default:  // Name ascending 
                    ProductS = ProductS.OrderBy(s => s.ProductData.NAME);
                    break;
            }

            int pageSize = 100;
            int pageNumber = (page ?? 1);
            return View(ProductS.ToPagedList(pageNumber, pageSize));
            //return View(db.USERS.ToList());
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
            var sTOREcATEGORY = db.STORE_CATEGORY.ToList();
            return View(sTOREcATEGORY);
        }

        // GET: Student/Delete/5
        public ActionResult _CategoriesDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STORE_CATEGORY sTOREcATEGORY = db.STORE_CATEGORY.Find(id);
            if (sTOREcATEGORY == null)
            {
                return HttpNotFound();
            }
            return View(sTOREcATEGORY);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("_CategoriesDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult _CategoriesDeleteConfirmed(int id)
        {
            STORE_CATEGORY sTOREcATEGORY = db.STORE_CATEGORY.Find(id);
            db.STORE_CATEGORY.Remove(sTOREcATEGORY);
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
            STORE_CATEGORY sTOREcATEGORY = db.STORE_CATEGORY.Find(id);
            if (sTOREcATEGORY == null)
            {
                return HttpNotFound();
            }
            return View(sTOREcATEGORY);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _CategoriesEdit([Bind(Include = "ID,NAME,IS_DEL")] STORE_CATEGORY sTOREcATEGORY)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sTOREcATEGORY).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Categories");
            }
            return View(sTOREcATEGORY);
        }

        // POST: Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CategoriesCreate([Bind(Include = "ID,NAME,IS_DEL")] STORE_CATEGORY sTOREcATEGORY)
        {
            if (ModelState.IsValid)
            {
                db.STORE_CATEGORY.Add(sTOREcATEGORY);
                db.SaveChanges();
                return RedirectToAction("Categories");
            }

            return View(sTOREcATEGORY);
        }

        // GET: Purchase/Create
        public ActionResult Purchase(int? id)
        {
            ViewBag.PRODUCT_ID = new SelectList(db.STORE_PRODUCTS.Where(o => o.PRODUCT_ID == id).ToList(), "PRODUCT_ID", "NAME");
            ViewBag.CartItems = (from t in db.STORE_PURCHAGE_CART
                                 select t).Count();
            return View();
        }

        // POST: Purchase/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Purchase([Bind(Include = "ID,PRODUCT_ID,UNIT_SOLD,SOLD_PRICE,SOLD_BY,SOLD_ON,STUDENT_NAME,STUDENT_CONTACT_NO,MONEY_RECEIVED_BY,IS_DEPOSITED,IS_ACT,IS_DEL,CREATED_AT,UPDATED_AT")] STORE_PURCHAGE_CART sTORE_PURCHAGE_cART)
        {
            if (ModelState.IsValid)
            {
                sTORE_PURCHAGE_cART.IS_DEL = "N";
                sTORE_PURCHAGE_cART.CREATED_AT = DateTime.Now;
                sTORE_PURCHAGE_cART.UPDATED_AT = DateTime.Now;
                db.STORE_PURCHAGE_CART.Add(sTORE_PURCHAGE_cART);
                db.SaveChanges();
                STORE_PRODUCTS sTOREpRODUCT = db.STORE_PRODUCTS.Find(sTORE_PURCHAGE_cART.PRODUCT_ID);
                sTOREpRODUCT.UPDATED_AT = DateTime.Now;
                sTOREpRODUCT.UNIT_LEFT = sTOREpRODUCT.UNIT_LEFT - sTORE_PURCHAGE_cART.UNIT_SOLD;
                db.Entry(sTOREpRODUCT).State = EntityState.Modified;
                db.SaveChanges();

                ViewBag.CartItems = (from t in db.STORE_PURCHAGE_CART
                                                 select t).Count();
                return RedirectToAction("ViewAll");
            }

            ViewBag.PRODUCT_ID = new SelectList(db.STORE_PRODUCTS, "PRODUCT_ID", "NAME", sTORE_PURCHAGE_cART.PRODUCT_ID);
            return View(sTORE_PURCHAGE_cART);
        }

        // GET: Student
        public ActionResult ViewAllSelling()
        {

            return View(db.STORE_PRODUCTS.ToList());
        }

 

        // GET: Student
        public ActionResult ListAllSellings(string sortOrder, string currentFilter, string searchString, int? page, string currentFilter2, string StudentName, string currentFilter3, string ContactNumber, string currentFilter4, string ReceivedBy, string currentFilter5, string MoneyDeposited, string currentFilter9, string SoldFromDate, string currentFilter10, string SoldToDate)
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
            if (StudentName != null) { page = 1; }
            else { StudentName = currentFilter2; }
            ViewBag.CurrentFilter2 = StudentName;
            if (ContactNumber != null) { page = 1; }
            else { ContactNumber = currentFilter3; }
            ViewBag.CurrentFilter3 = ContactNumber;
            if (ReceivedBy != null) { page = 1; }
            else { ReceivedBy = currentFilter4; }
            ViewBag.CurrentFilter4 = ReceivedBy;
            if (MoneyDeposited != null) { page = 1; }
            else { MoneyDeposited = currentFilter5; }
            ViewBag.CurrentFilter5 = MoneyDeposited;
            if (SoldFromDate != null)
            {
                page = 1;
            }
            else { SoldFromDate = currentFilter9; }
            DateTime? dFrom; DateTime dtFrom;
            dFrom = DateTime.TryParse(SoldFromDate, out dtFrom) ? dtFrom : (DateTime?)null;
            ViewBag.CurrentFilter9 = SoldFromDate;
            if (SoldToDate != null)
            {
                page = 1;
            }
            else { SoldToDate = currentFilter10; }
            DateTime? dTo; DateTime dtTo;
            dTo = DateTime.TryParse(SoldToDate, out dtTo) ? dtTo : (DateTime?)null;
            ViewBag.CurrentFilter10 = SoldToDate;

            var PurchaseS = (from pd in db.STORE_PRODUCTS
                             join ct in db.STORE_CATEGORY on pd.CATEGORY_ID equals ct.ID
                             join pur in db.STORE_PURCHAGE on pd.PRODUCT_ID equals pur.PRODUCT_ID
                             orderby pur.SOLD_ON, pd.NAME, ct.NAME
                             where pur.IS_DEL == "N"
                             select new Models.Purchase { PurchaseData = pur, ProductData = pd, CategoryData = ct }).Distinct();

            if (!String.IsNullOrEmpty(searchString))
            {
                PurchaseS = PurchaseS.Where(s => s.ProductData.NAME.Contains(searchString));
            }
            if (!String.IsNullOrEmpty(StudentName))
            {
                PurchaseS = PurchaseS.Where(s => s.PurchaseData.STUDENT_NAME.Equals(StudentName));
            }
            if (!String.IsNullOrEmpty(ContactNumber))
            {
                PurchaseS = PurchaseS.Where(s => s.PurchaseData.STUDENT_CONTACT_NO.Contains(ContactNumber));
            }
            if (!String.IsNullOrEmpty(ReceivedBy))
            {
                PurchaseS = PurchaseS.Where(s => s.PurchaseData.MONEY_RECEIVED_BY.Contains(ReceivedBy));
            }
            if (!String.IsNullOrEmpty(MoneyDeposited))
            {
                PurchaseS = PurchaseS.Where(s => s.PurchaseData.IS_DEPOSITED.Contains(MoneyDeposited));
            }
            if (!String.IsNullOrEmpty(SoldFromDate) && !String.IsNullOrEmpty(SoldToDate))
            {
                PurchaseS = PurchaseS.Where(s => s.PurchaseData.SOLD_ON >= dFrom).Where(s => s.PurchaseData.SOLD_ON <= dTo);
            }
            switch (sortOrder)
            {
                case "name_desc":
                    PurchaseS = PurchaseS.OrderByDescending(s => s.ProductData.NAME);
                    break;
                case "Date":
                    PurchaseS = PurchaseS.OrderBy(s => s.PurchaseData.SOLD_ON);
                    break;
                case "date_desc":
                    PurchaseS = PurchaseS.OrderByDescending(s => s.PurchaseData.SOLD_ON);
                    break;
                default:  // Name ascending 
                    PurchaseS = PurchaseS.OrderBy(s => s.PurchaseData.SOLD_ON);
                    break;
            }

            int pageSize = 100;
            int pageNumber = (page ?? 1);
            return View(PurchaseS.ToPagedList(pageNumber, pageSize));
            //return View(db.USERS.ToList());
        }

        // GET: Student
        [HttpGet]
        public void PurchasePdf(string sortOrder, string currentFilter, string searchString, int? page, string currentFilter2, string StudentName, string currentFilter3, string ContactNumber, string currentFilter4, string ReceivedBy, string currentFilter5, string MoneyDeposited, string currentFilter9, string SoldFromDate, string currentFilter10, string SoldToDate)
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
            if (StudentName != null) { page = 1; }
            else { StudentName = currentFilter2; }
            ViewBag.CurrentFilter2 = StudentName;
            if (ContactNumber != null) { page = 1; }
            else { ContactNumber = currentFilter3; }
            ViewBag.CurrentFilter3 = ContactNumber;
            if (ReceivedBy != null) { page = 1; }
            else { ReceivedBy = currentFilter4; }
            ViewBag.CurrentFilter4 = ReceivedBy;
            if (MoneyDeposited != null) { page = 1; }
            else { MoneyDeposited = currentFilter5; }
            ViewBag.CurrentFilter5 = MoneyDeposited;
            if (SoldFromDate != null)
            {
                page = 1;
            }
            else { SoldFromDate = currentFilter9; }
            DateTime? dFrom; DateTime dtFrom;
            dFrom = DateTime.TryParse(SoldFromDate, out dtFrom) ? dtFrom : (DateTime?)null;
            ViewBag.CurrentFilter9 = SoldFromDate;
            if (SoldToDate != null)
            {
                page = 1;
            }
            else { SoldToDate = currentFilter10; }
            DateTime? dTo; DateTime dtTo;
            dTo = DateTime.TryParse(SoldToDate, out dtTo) ? dtTo : (DateTime?)null;
            ViewBag.CurrentFilter10 = SoldToDate;

            var PurchaseS = (from pd in db.STORE_PRODUCTS
                             join ct in db.STORE_CATEGORY on pd.CATEGORY_ID equals ct.ID
                             join pur in db.STORE_PURCHAGE on pd.PRODUCT_ID equals pur.PRODUCT_ID
                             orderby pur.SOLD_ON, pd.NAME, ct.NAME
                             where pur.IS_DEL == "N"
                             select new Models.Purchase { PurchaseData = pur, ProductData = pd, CategoryData = ct }).Distinct();

            if (!String.IsNullOrEmpty(searchString))
            {
                PurchaseS = PurchaseS.Where(s => s.ProductData.NAME.Contains(searchString));
            }
            if (!String.IsNullOrEmpty(StudentName))
            {
                PurchaseS = PurchaseS.Where(s => s.PurchaseData.STUDENT_NAME.Equals(StudentName));
            }
            if (!String.IsNullOrEmpty(ContactNumber))
            {
                PurchaseS = PurchaseS.Where(s => s.PurchaseData.STUDENT_CONTACT_NO.Contains(ContactNumber));
            }
            if (!String.IsNullOrEmpty(ReceivedBy))
            {
                PurchaseS = PurchaseS.Where(s => s.PurchaseData.MONEY_RECEIVED_BY.Contains(ReceivedBy));
            }
            if (!String.IsNullOrEmpty(MoneyDeposited))
            {
                PurchaseS = PurchaseS.Where(s => s.PurchaseData.IS_DEPOSITED.Contains(MoneyDeposited));
            }
            if (!String.IsNullOrEmpty(SoldFromDate) && !String.IsNullOrEmpty(SoldToDate))
            {
                PurchaseS = PurchaseS.Where(s => s.PurchaseData.SOLD_ON >= dFrom).Where(s => s.PurchaseData.SOLD_ON <= dTo);
            }
            switch (sortOrder)
            {
                case "name_desc":
                    PurchaseS = PurchaseS.OrderByDescending(s => s.ProductData.NAME);
                    break;
                case "Date":
                    PurchaseS = PurchaseS.OrderBy(s => s.PurchaseData.SOLD_ON);
                    break;
                case "date_desc":
                    PurchaseS = PurchaseS.OrderByDescending(s => s.PurchaseData.SOLD_ON);
                    break;
                default:  // Name ascending 
                    PurchaseS = PurchaseS.OrderBy(s => s.PurchaseData.SOLD_ON);
                    break;
            }

            var PdfStoreS = (from res in PurchaseS
                               select new { PName = res.ProductData.NAME, ContactNum = res.PurchaseData.STUDENT_CONTACT_NO, RecBy = res.PurchaseData.MONEY_RECEIVED_BY, MDepo = res.PurchaseData.IS_DEPOSITED, SoldOn = res.PurchaseData.SOLD_ON }).ToList();


            var configuration = new ReportConfiguration();
            //configuration.PageOrientation = PageSize.LETTER_LANDSCAPE.Rotate();
            configuration.LogoPath
                = Server.MapPath(Url.Content("~/Content/images/login/SF_Square_Logo-Small.jpg"));
            configuration.LogImageScalePercent = 50;
            configuration.ReportTitle
                = "S. F. Square Store Report";
            configuration.ReportSubTitle = "Result of Purchase Search";

            var report = new PdfTabularReport();
            report.ReportConfiguration = configuration;

            List<ReportColumn> columns = new List<ReportColumn>();
            columns.Add(new ReportColumn { ColumnName = "Sl. No.", Width = 100 });
            columns.Add(new ReportColumn { ColumnName = "Product Name", Width = 100 });
            columns.Add(new ReportColumn { ColumnName = "Student Contact", Width = 100 });
            columns.Add(new ReportColumn { ColumnName = "Money Received By", Width = 100 });
            columns.Add(new ReportColumn { ColumnName = "Is Money Deposited?", Width = 100 });
            columns.Add(new ReportColumn { ColumnName = "Sold On", Width = 100 });

            var PdfStoreSI = new DataTable();

            PdfStoreSI.Columns.Add("Sl. No.", typeof(int));
            PdfStoreSI.Columns.Add("Product Name", typeof(string));
            PdfStoreSI.Columns.Add("Student Contact", typeof(string));
            PdfStoreSI.Columns.Add("Money Received By", typeof(string));
            PdfStoreSI.Columns.Add("Is Money Deposited?", typeof(string));
            PdfStoreSI.Columns.Add("Sold On", typeof(string));

            int i = 1;
            foreach (var entity in PdfStoreS.ToList())
            {
                var row = PdfStoreSI.NewRow();
                row["Sl. No."] = i;
                row["Product Name"] = entity.PName;
                row["Student Contact"] = entity.ContactNum;
                row["Money Received By"] = entity.RecBy;
                row["Is Money Deposited?"] = entity.MDepo;
                row["Sold On"] = entity.SoldOn;
                PdfStoreSI.Rows.Add(row);
                i = i + 1;
            }


            var stream = report.GetPdf(PdfStoreSI, columns);

            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition",
                "attachment;filename=ExampleReport.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(stream.ToArray());
            Response.End();

        }


        // GET: Store/Edit/5
        public ActionResult EditSelling(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STORE_PURCHAGE sTORE_PURCHASE = db.STORE_PURCHAGE.Find(id);
            if (sTORE_PURCHASE == null)
            {
                return HttpNotFound();
            }
            ViewBag.PRODUCT_ID = new SelectList(db.STORE_PRODUCTS, "PRODUCT_ID", "NAME", sTORE_PURCHASE.PRODUCT_ID);
            DateTime SDate = Convert.ToDateTime(sTORE_PURCHASE.SOLD_ON);
            ViewBag.SOLD_ON = SDate.ToShortDateString();
            return View(sTORE_PURCHASE);
        }

        // POST: Store/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSelling([Bind(Include = "ID,PRODUCT_ID,UNIT_SOLD,SOLD_PRICE,SOLD_BY,SOLD_ON,STUDENT_NAME,STUDENT_CONTACT_NO,MONEY_RECEIVED_BY,IS_DEPOSITED,IS_ACT,IS_DEL,CREATED_AT,UPDATED_AT")] STORE_PURCHAGE sTORE_PURCHASE)
        {
            if (ModelState.IsValid)
            {
                sTORE_PURCHASE.IS_DEL = "N";
                sTORE_PURCHASE.UPDATED_AT = DateTime.Now;
                db.Entry(sTORE_PURCHASE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ViewAllSelling");
            }
            ViewBag.PRODUCT_ID = new SelectList(db.STORE_PRODUCTS, "PRODUCT_ID", "NAME", sTORE_PURCHASE.PRODUCT_ID);
            DateTime SDate = Convert.ToDateTime(sTORE_PURCHASE.SOLD_ON);
            ViewBag.SOLD_ON = SDate.ToShortDateString();
            return View(sTORE_PURCHASE);
        }

        // GET: Student
        public ActionResult ViewCart()
        {
            ViewBag.CartItems = (from t in db.STORE_PURCHAGE_CART
                                 select t).Count();
            var TotalPriceSel = db.STORE_PURCHAGE_CART.GroupBy(o => o.ID)
                .Select(g => new { membername = g.Key, total = g.Sum(p => p.SOLD_PRICE) });
            int TCost = 0;
            foreach (var group in TotalPriceSel)
            {
                TCost = TCost+ Convert.ToInt32(group.total);
            }

            ViewBag.TotalPrice = TCost.ToString();
            return View(db.STORE_CATEGORY.ToList());
        }


        // GET: Store Products
        public ActionResult ListPurchagedProducts(string sortOrder, string currentFilter, string searchString, int? page)
        {

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null && searchString != "")
            {
                page = 1;
                ///As Drop down list sends Id, we will ahve to convert this to text which is different from text box
                int searchStringId = Convert.ToInt32(searchString);
                searchString = db.STORE_CATEGORY.Find(searchStringId).NAME.ToString();
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var ProductS = (from pd in db.STORE_PRODUCTS
                            join ct in db.STORE_CATEGORY on pd.CATEGORY_ID equals ct.ID
                            join pct in db.STORE_PURCHAGE_CART on pd.PRODUCT_ID equals pct.PRODUCT_ID
                            orderby pd.NAME, ct.NAME
                            select new Models.PurchaseCart { ProductData = pd, CategoryData = ct, PurchaseCartData=pct }).Distinct();

            if (!String.IsNullOrEmpty(searchString))
            {
                ProductS = ProductS.Where(s => s.CategoryData.NAME.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    ProductS = ProductS.OrderByDescending(s => s.ProductData.NAME);
                    break;
                case "Date":
                    ProductS = ProductS.OrderBy(s => s.PurchaseCartData.SOLD_ON);
                    break;
                case "date_desc":
                    ProductS = ProductS.OrderByDescending(s => s.PurchaseCartData.SOLD_ON);
                    break;
                default:  // Name ascending 
                    ProductS = ProductS.OrderBy(s => s.ProductData.NAME);
                    break;
            }

            int pageSize = 100;
            int pageNumber = (page ?? 1);
            return View(ProductS.ToPagedList(pageNumber, pageSize));
            //return View(db.USERS.ToList());
        }

        // GET: Student
        [HttpGet]
        public void PrintReceipt(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null && searchString != "")
            {
                page = 1;
                ///As Drop down list sends Id, we will ahve to convert this to text which is different from text box
                int searchStringId = Convert.ToInt32(searchString);
                searchString = db.STORE_CATEGORY.Find(searchStringId).NAME.ToString();
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var ProductS = (from pd in db.STORE_PRODUCTS
                            join ct in db.STORE_CATEGORY on pd.CATEGORY_ID equals ct.ID
                            join pct in db.STORE_PURCHAGE_CART on pd.PRODUCT_ID equals pct.PRODUCT_ID
                            orderby pd.NAME, ct.NAME
                            select new Models.PurchaseCart { ProductData = pd, CategoryData = ct, PurchaseCartData = pct }).Distinct();


            if (!String.IsNullOrEmpty(searchString))
            {
                ProductS = ProductS.Where(s => s.CategoryData.NAME.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    ProductS = ProductS.OrderByDescending(s => s.ProductData.NAME);
                    break;
                case "Date":
                    ProductS = ProductS.OrderBy(s => s.PurchaseCartData.SOLD_ON);
                    break;
                case "date_desc":
                    ProductS = ProductS.OrderByDescending(s => s.PurchaseCartData.SOLD_ON);
                    break;
                default:  // Name ascending 
                    ProductS = ProductS.OrderBy(s => s.ProductData.NAME);
                    break;
            }

            var ContactNum = db.STORE_PURCHAGE_CART
                            .Select(g => new { ConNum = g.STUDENT_CONTACT_NO }).FirstOrDefault();
            string ConNum = ContactNum.ConNum.ToString();
            var PurDate = db.STORE_PURCHAGE_CART
                            .Select(g => new { SoldOn = g.SOLD_ON }).FirstOrDefault();

            DateTime PDate = Convert.ToDateTime(PurDate.SoldOn);
            string SoldDate = PDate.ToString("mm/dd/yyyy");

            var TotalPriceSel = db.STORE_PURCHAGE_CART.GroupBy(o => o.ID)
                .Select(g => new { membername = g.Key, total = g.Sum(p => p.SOLD_PRICE) });
            int TCost = 0;
            foreach (var group in TotalPriceSel)
            {
                TCost = TCost + Convert.ToInt32(group.total);
            }

            var TotalPrice = TCost.ToString();

            var PdfStoreS = (from res in ProductS
                             select new { PName = res.ProductData.NAME, NumUnit = res.PurchaseCartData.UNIT_SOLD, UnitPrice = res.ProductData.SELL_PRICE_PER_UNIT, TotalPrice = res.PurchaseCartData.SOLD_PRICE}).ToList();


            var configuration = new ReportConfiguration();
            //configuration.PageOrientation = PageSize.LETTER_LANDSCAPE.Rotate();
            configuration.LogoPath
                = Server.MapPath(Url.Content("~/Content/images/login/SF_Square_Logo-Small.jpg"));
            configuration.LogImageScalePercent = 25;
            configuration.ReportTitle
                = "S. F. Square Store Purchage Receipt";
            configuration.ReportSubTitle = string.Concat("Total Cost =", TotalPrice,";Date =", SoldDate, ";Concat Number =", ConNum);
            configuration.MarginLeft = 5;
            configuration.MarginRight = 5;

        var report = new PdfTabularReport();
            report.ReportConfiguration = configuration;

            List<ReportColumn> columns = new List<ReportColumn>();
            columns.Add(new ReportColumn { ColumnName = "Sl. No.", Width = 100 });
            columns.Add(new ReportColumn { ColumnName = "Product Name", Width = 100 });
            columns.Add(new ReportColumn { ColumnName = "Number of Units", Width = 100 });
            columns.Add(new ReportColumn { ColumnName = "Price Per Unit", Width = 100 });
            columns.Add(new ReportColumn { ColumnName = "Total Cost", Width = 100 });

            var PdfStoreSI = new DataTable();

            PdfStoreSI.Columns.Add("Sl. No.", typeof(int));
            PdfStoreSI.Columns.Add("Product Name", typeof(string));
            PdfStoreSI.Columns.Add("Number of Units", typeof(string));
            PdfStoreSI.Columns.Add("Price Per Unit", typeof(string));
            PdfStoreSI.Columns.Add("Total Cost", typeof(string));

            int i = 1;
            foreach (var entity in PdfStoreS.ToList())
            {
                var row = PdfStoreSI.NewRow();
                row["Sl. No."] = i;
                row["Product Name"] = entity.PName;
                row["Number of Units"] = entity.NumUnit;
                row["Price Per Unit"] = entity.UnitPrice;
                row["Total Cost"] = entity.TotalPrice;
                PdfStoreSI.Rows.Add(row);
                i = i + 1;
            }

            var pURcART = (from res in db.STORE_PURCHAGE_CART
                           select res).ToList();
            foreach (var PurCarList in pURcART)
            {

                STORE_PURCHAGE StorePur = db.STORE_PURCHAGE.Find(PurCarList.ID);
                StorePur.PRODUCT_ID = PurCarList.PRODUCT_ID;
                StorePur.UNIT_SOLD = PurCarList.UNIT_SOLD;
                StorePur.SOLD_PRICE = PurCarList.SOLD_PRICE;
                StorePur.SOLD_BY = PurCarList.SOLD_BY;
                StorePur.SOLD_ON = PurCarList.SOLD_ON;
                StorePur.STUDENT_NAME = PurCarList.STUDENT_NAME;
                StorePur.STUDENT_CONTACT_NO = PurCarList.STUDENT_CONTACT_NO;
                StorePur.MONEY_RECEIVED_BY = PurCarList.MONEY_RECEIVED_BY;
                StorePur.IS_DEPOSITED = PurCarList.IS_DEPOSITED;
                StorePur.IS_ACT = PurCarList.IS_ACT;
                StorePur.IS_DEL = PurCarList.IS_DEL;
                StorePur.CREATED_AT = PurCarList.CREATED_AT;
                StorePur.UPDATED_AT = PurCarList.UPDATED_AT;
                db.STORE_PURCHAGE.Add(StorePur);
                db.SaveChanges();

            }

            db.Database.ExecuteSqlCommand("TRUNCATE TABLE STORE_PURCHAGE_CART");

            var stream = report.GetPdf(PdfStoreSI, columns);

            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition",
                "attachment;filename=ExampleReport.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(stream.ToArray());
            Response.End();

        }

        // GET: Student
        public ActionResult CancelTransaction()
        {

            var pURcART = (from res in db.STORE_PURCHAGE_CART
                             select res).ToList();
            foreach(var PurCarList in pURcART) 
            {
 
                STORE_PRODUCTS sTOREpRODUCT = db.STORE_PRODUCTS.Find(PurCarList.PRODUCT_ID);
                sTOREpRODUCT.UPDATED_AT = DateTime.Now;
                sTOREpRODUCT.UNIT_LEFT = sTOREpRODUCT.UNIT_LEFT + PurCarList.UNIT_SOLD;
                db.Entry(sTOREpRODUCT).State = EntityState.Modified;
                db.SaveChanges();
            }

            db.Database.ExecuteSqlCommand("TRUNCATE TABLE STORE_PURCHAGE_CART");
            return RedirectToAction("ViewAll");
        }

        // GET: Student
        public ActionResult CleanCart()
        {
            var pURcART = (from res in db.STORE_PURCHAGE_CART
                           select res).ToList();
            foreach (var PurCarList in pURcART)
            {

                STORE_PRODUCTS sTOREpRODUCT = db.STORE_PRODUCTS.Find(PurCarList.PRODUCT_ID);
                sTOREpRODUCT.UPDATED_AT = DateTime.Now;
                sTOREpRODUCT.UNIT_LEFT = sTOREpRODUCT.UNIT_LEFT + PurCarList.UNIT_SOLD;
                db.Entry(sTOREpRODUCT).State = EntityState.Modified;
                db.SaveChanges();
            }

            db.Database.ExecuteSqlCommand("TRUNCATE TABLE STORE_PURCHAGE_CART");
            return RedirectToAction("ViewAll");
        }
    }
}
