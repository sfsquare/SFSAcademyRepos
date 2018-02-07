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
                STORE_PRODUCTS sTORE_PRODUCTS_UPD = db.STORE_PRODUCTS.Find(sTORE_PRODUCTS.PRODUCT_ID);
                sTORE_PRODUCTS_UPD.NAME = sTORE_PRODUCTS.NAME;
                sTORE_PRODUCTS_UPD.CATEGORY_ID = sTORE_PRODUCTS.CATEGORY_ID;
                sTORE_PRODUCTS_UPD.BRAND = sTORE_PRODUCTS.BRAND;
                sTORE_PRODUCTS_UPD.TOTAL_UNIT = sTORE_PRODUCTS.TOTAL_UNIT;
                sTORE_PRODUCTS_UPD.TOTAL_COST = sTORE_PRODUCTS.TOTAL_COST;
                sTORE_PRODUCTS_UPD.COST_PER_UNIT = sTORE_PRODUCTS.COST_PER_UNIT;
                sTORE_PRODUCTS_UPD.SELL_PRICE_PER_UNIT = sTORE_PRODUCTS.SELL_PRICE_PER_UNIT;
                sTORE_PRODUCTS_UPD.PURCHASED_ON = sTORE_PRODUCTS.PURCHASED_ON;
                sTORE_PRODUCTS_UPD.PURCHASED_THROUGH = sTORE_PRODUCTS.PURCHASED_THROUGH;
                sTORE_PRODUCTS_UPD.PAID_BY = sTORE_PRODUCTS.PAID_BY;
                sTORE_PRODUCTS_UPD.UNIT_LEFT = sTORE_PRODUCTS.UNIT_LEFT;
                sTORE_PRODUCTS_UPD.IS_ACT = sTORE_PRODUCTS.IS_ACT;
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
                STORE_CATEGORY sTOREcATEGORY_UPD = db.STORE_CATEGORY.Find(sTOREcATEGORY.ID);
                sTOREcATEGORY_UPD.NAME = sTOREcATEGORY.NAME;
                sTOREcATEGORY_UPD.IS_DEL = sTOREcATEGORY.IS_DEL;
                db.Entry(sTOREcATEGORY_UPD).State = EntityState.Modified;
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
            //ViewBag.PRODUCT_ID = new SelectList(db.STORE_PRODUCTS.Where(o => o.PRODUCT_ID == id).ToList(), "PRODUCT_ID", "NAME");
            //ViewBag.CartItems = (from t in db.STORE_PURCHAGE_CART
            //                    select t).Count();

            STORE_PRODUCTS sTOREpRODUCT = db.STORE_PRODUCTS.Find(id);
            var sTORE_PURCHAGE_cART = new STORE_PURCHAGE_CART() { PRODUCT_ID = sTOREpRODUCT.PRODUCT_ID, UNIT_SOLD=1, SOLD_PRICE= sTOREpRODUCT.SELL_PRICE_PER_UNIT, SOLD_BY= this.Session["UserId"].ToString(), SOLD_ON= DateTime.Now, STUDENT_NAME=null, STUDENT_CONTACT_NO=null, MONEY_RECEIVED_BY=null, IS_DEPOSITED=null, IS_ACT=null, IS_DEL=null, CREATED_AT= DateTime.Now, UPDATED_AT= DateTime.Now };
            db.STORE_PURCHAGE_CART.Add(sTORE_PURCHAGE_cART);
            db.SaveChanges();

            sTOREpRODUCT.UPDATED_AT = DateTime.Now;
            sTOREpRODUCT.UNIT_LEFT = sTOREpRODUCT.UNIT_LEFT - 1;
            db.Entry(sTOREpRODUCT).State = EntityState.Modified;
            db.SaveChanges();

            ViewBag.CartItems = (from t in db.STORE_PURCHAGE_CART
                                 select t).Count();
            return RedirectToAction("ViewAll");
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
                PurchaseS = PurchaseS.Where(s => s.PurchaseData.STUDENT_CONTACT_NO.Equals(ContactNumber));
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
                PurchaseS = PurchaseS.Where(s => s.PurchaseData.STUDENT_CONTACT_NO.Equals(ContactNumber));
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
                STORE_PURCHAGE sTORE_PURCHASE_UPD = db.STORE_PURCHAGE.Find(sTORE_PURCHASE.ID);
                sTORE_PURCHASE_UPD.PRODUCT_ID = sTORE_PURCHASE.PRODUCT_ID;
                sTORE_PURCHASE_UPD.UNIT_SOLD = sTORE_PURCHASE.UNIT_SOLD;
                sTORE_PURCHASE_UPD.SOLD_PRICE = sTORE_PURCHASE.SOLD_PRICE;
                sTORE_PURCHASE_UPD.SOLD_BY = sTORE_PURCHASE.SOLD_BY;
                sTORE_PURCHASE_UPD.SOLD_ON = sTORE_PURCHASE.SOLD_ON;
                sTORE_PURCHASE_UPD.STUDENT_NAME = sTORE_PURCHASE.STUDENT_NAME;
                sTORE_PURCHASE_UPD.STUDENT_CONTACT_NO = sTORE_PURCHASE.STUDENT_CONTACT_NO;
                sTORE_PURCHASE_UPD.MONEY_RECEIVED_BY = sTORE_PURCHASE.MONEY_RECEIVED_BY;
                sTORE_PURCHASE_UPD.IS_DEPOSITED = sTORE_PURCHASE.IS_DEPOSITED;
                sTORE_PURCHASE_UPD.IS_ACT = sTORE_PURCHASE.IS_ACT;
                sTORE_PURCHASE_UPD.UPDATED_AT = DateTime.Now;
                db.Entry(sTORE_PURCHASE_UPD).State = EntityState.Modified;
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
            ViewBag.PURCHAGE_DATE = DateTime.Now.ToShortDateString();
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

            var payments = from p in db.STORE_PURCHAGE_CART
                               //where p.CREATED_AT.Value.ToShortDateString() == DateTime.Now.ToShortDateString()
                           group p by p.PRODUCT_ID into g
                           select new
                           {
                               ProductNo = g.Key,
                               UNIT_SOLD = g.Count(),
                               AMNT = g.Sum(x => x.SOLD_PRICE),
                               PUR_DATE = DateTime.Now
                           };

            var ProductS = (from pd in db.STORE_PRODUCTS
                            join ct in db.STORE_CATEGORY on pd.CATEGORY_ID equals ct.ID
                            join pct in payments on pd.PRODUCT_ID equals pct.ProductNo
                            orderby pd.NAME, ct.NAME
                            select new Models.PurchaseCart { ProductData = pd, CategoryData = ct, UNIT_SOLD = pct.UNIT_SOLD, SOLD_AMNT = pct.AMNT, PUR_DATE = pct.PUR_DATE }).Distinct();

            return View(ProductS.ToList());
        }


        // GET: Student
        //[HttpGet]
        public ActionResult Payment(string PAYMENT_MODE, string STUDENT_NAME, decimal? PAYMENT_AMOUNT, long? PHONE_NUMBER, DateTime? PURCHAGE_DATE)
        {
            ViewBag.STUDENT_NAME = STUDENT_NAME;
            ViewBag.PHONE_NUMBER = PHONE_NUMBER;
            ViewBag.PURCHAGE_DATE = PURCHAGE_DATE;
            ViewBag.PAYMENT_MODE = PAYMENT_MODE;
            ViewBag.CartItems = (from t in db.STORE_PURCHAGE_CART
                                 select t).Count();
            var TotalPriceSel = db.STORE_PURCHAGE_CART.GroupBy(o => o.ID)
                .Select(g => new { membername = g.Key, total = g.Sum(p => p.SOLD_PRICE) });
            int TCost = 0;
            foreach (var group in TotalPriceSel)
            {
                TCost = TCost + Convert.ToInt32(group.total);
            }

            ViewBag.PaidPrice = TCost.ToString();
            ViewBag.TotalPrice = 0;
            if(PHONE_NUMBER == null)
            { PHONE_NUMBER = 9967803589; }

            var IDList = new int[20];
            int i = 0;

            var pURcART = (from res in db.STORE_PURCHAGE_CART
                           select res).ToList();
            foreach (var PurCarList in pURcART)
            {

                STORE_PURCHAGE StorePur = db.STORE_PURCHAGE.Find(PurCarList.ID);
                StorePur.PRODUCT_ID = PurCarList.PRODUCT_ID;
                StorePur.UNIT_SOLD = PurCarList.UNIT_SOLD;
                StorePur.SOLD_PRICE = PurCarList.SOLD_PRICE;
                StorePur.SOLD_BY = PurCarList.MONEY_RECEIVED_BY;
                StorePur.SOLD_ON = PURCHAGE_DATE;
                StorePur.STUDENT_NAME = STUDENT_NAME;
                StorePur.STUDENT_CONTACT_NO = PHONE_NUMBER;
                StorePur.MONEY_RECEIVED_BY = PAYMENT_MODE;
                StorePur.IS_DEPOSITED = "N";
                StorePur.IS_ACT = "Y";
                StorePur.IS_DEL = "N";
                StorePur.CREATED_AT = PurCarList.CREATED_AT;
                StorePur.UPDATED_AT = PurCarList.UPDATED_AT;
                db.STORE_PURCHAGE.Add(StorePur);
                db.SaveChanges();

                IDList[i] = StorePur.ID;
                i++;

            }


            var payments = from p in db.STORE_PURCHAGE.Where(a => IDList.Any(s => a.ID.Equals(s)))
                           group p by p.PRODUCT_ID into g
                           select new
                           {                             
                               ProductNo = g.Key,
                               UNIT_SOLD = g.Count(),
                               AMNT = g.Sum(x => x.SOLD_PRICE),
                               PUR_DATE = DateTime.Now
                           };

            var ProductS = (from pd in db.STORE_PRODUCTS
                            join ct in db.STORE_CATEGORY on pd.CATEGORY_ID equals ct.ID
                            join pct in payments on pd.PRODUCT_ID equals pct.ProductNo
                            orderby pd.NAME, ct.NAME
                            select new Models.PurchaseCart { ProductData = pd, CategoryData = ct, UNIT_SOLD = pct.UNIT_SOLD, SOLD_AMNT = pct.AMNT, PUR_DATE = pct.PUR_DATE }).Distinct();



            db.Database.ExecuteSqlCommand("DELETE FROM STORE_PURCHAGE_CART");
            return View("_ListPurchagedProducts", ProductS.ToList());

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
