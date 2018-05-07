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
            //var sTORE_PRODUCTS = db.STORE_PRODUCTS.Include(s => s.STORE_CATEGORY);
            return View();
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
            /////Query for selecting Subcategory**********************
            var queryCatSubcat = (from sc in db.STORE_CATEGORY
                                  join ssc in db.STORE_SUB_CATEGORY on sc.ID equals ssc.STORE_CATEGORY_ID
                                  select new Models.SubCategory { CategoryData = sc, SubCategoryData = ssc })
                        .OrderBy(x => x.CategoryData.ID).ToList();


            List<SelectListItem> options = new List<SelectListItem>();
            foreach (var item in queryCatSubcat)
            {
                string CatFullName = string.Concat(item.CategoryData.NAME, "-", item.SubCategoryData.NAME);
                var result = new SelectListItem();
                result.Text = CatFullName;
                result.Value = item.SubCategoryData.ID.ToString();
                options.Add(result);
            }
            // add the 'ALL' option
            options.Insert(0, new SelectListItem() { Value = "-1", Text = "Select Subcategory" });
            ViewBag.SUB_CATEGORY_ID = options;
            ///// End 
            //ViewBag.SUB_CATEGORY_ID = new SelectList(db.STORE_SUB_CATEGORY.Where(x=>x.STORE_CATEGORY_ID == sTORE_PRODUCTS.CATEGORY_ID), "ID", "NAME", sTORE_PRODUCTS.SUB_CATEGORY_ID);

            return View();
        }

        // POST: Store/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PRODUCT_ID,NAME,CATEGORY_ID,SUB_CATEGORY_ID,BRAND,TOTAL_UNIT,TOTAL_COST,COST_PER_UNIT,SELL_PRICE_PER_UNIT,PURCHASED_ON,PURCHASED_THROUGH,PAID_BY,UNIT_LEFT,IS_ACT,IS_DEL,CREATED_AT,UPDATED_AT")] STORE_PRODUCTS sTORE_PRODUCTS)
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
            /////Query for selecting Subcategory**********************
            var queryCatSubcat = (from sc in db.STORE_CATEGORY
                                  join ssc in db.STORE_SUB_CATEGORY on sc.ID equals ssc.STORE_CATEGORY_ID
                                  select new Models.SubCategory { CategoryData = sc, SubCategoryData = ssc })
                        .OrderBy(x => x.CategoryData.ID).ToList();


            List<SelectListItem> options = new List<SelectListItem>();
            foreach (var item in queryCatSubcat)
            {
                string CatFullName = string.Concat(item.CategoryData.NAME, "-", item.SubCategoryData.NAME);
                var result = new SelectListItem();
                result.Text = CatFullName;
                result.Value = item.SubCategoryData.ID.ToString();
                result.Selected = item.SubCategoryData.ID == sTORE_PRODUCTS.SUB_CATEGORY_ID ? true : false;
                options.Add(result);
            }
            // add the 'ALL' option
            options.Insert(0, new SelectListItem() { Value = "-1", Text = "Select Subcategory" });
            ViewBag.SUB_CATEGORY_ID = options;
            ///// End 
            //ViewBag.SUB_CATEGORY_ID = new SelectList(db.STORE_SUB_CATEGORY.Where(x=>x.STORE_CATEGORY_ID == sTORE_PRODUCTS.CATEGORY_ID), "ID", "NAME", sTORE_PRODUCTS.SUB_CATEGORY_ID);
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

            /////Query for selecting Subcategory**********************
            var queryCatSubcat = (from sc in db.STORE_CATEGORY
                                  join ssc in db.STORE_SUB_CATEGORY on sc.ID equals ssc.STORE_CATEGORY_ID
                                    select new Models.SubCategory { CategoryData = sc, SubCategoryData = ssc})
                        .OrderBy(x => x.CategoryData.ID).ToList();


            List<SelectListItem> options = new List<SelectListItem>();
            foreach (var item in queryCatSubcat)
            {
                string CatFullName = string.Concat(item.CategoryData.NAME, "-", item.SubCategoryData.NAME);
                var result = new SelectListItem();
                result.Text = CatFullName;
                result.Value = item.SubCategoryData.ID.ToString();
                result.Selected = item.SubCategoryData.ID == sTORE_PRODUCTS.SUB_CATEGORY_ID ? true : false;
                options.Add(result);
            }
            // add the 'ALL' option
            options.Insert(0, new SelectListItem() { Value = "-1", Text = "Select Subcategory" });
            ViewBag.SUB_CATEGORY_ID = options;
            ///// End 
            //ViewBag.SUB_CATEGORY_ID = new SelectList(db.STORE_SUB_CATEGORY.Where(x=>x.STORE_CATEGORY_ID == sTORE_PRODUCTS.CATEGORY_ID), "ID", "NAME", sTORE_PRODUCTS.SUB_CATEGORY_ID);
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
        public ActionResult Edit([Bind(Include = "PRODUCT_ID,NAME,CATEGORY_ID,SUB_CATEGORY_ID,BRAND,TOTAL_UNIT,TOTAL_COST,COST_PER_UNIT,SELL_PRICE_PER_UNIT,PURCHASED_ON,PURCHASED_THROUGH,PAID_BY,UNIT_LEFT,IS_ACT,IS_DEL,CREATED_AT,UPDATED_AT")] STORE_PRODUCTS sTORE_PRODUCTS)
        {
            if (ModelState.IsValid)
            {
                STORE_PRODUCTS sTORE_PRODUCTS_UPD = db.STORE_PRODUCTS.Find(sTORE_PRODUCTS.PRODUCT_ID);
                sTORE_PRODUCTS_UPD.NAME = sTORE_PRODUCTS.NAME;
                sTORE_PRODUCTS_UPD.CATEGORY_ID = sTORE_PRODUCTS.CATEGORY_ID;
                sTORE_PRODUCTS_UPD.SUB_CATEGORY_ID = sTORE_PRODUCTS.SUB_CATEGORY_ID;
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
                sTORE_PRODUCTS_UPD.CREATED_AT = DateTime.Now;
                sTORE_PRODUCTS_UPD.UPDATED_AT = DateTime.Now;
                db.STORE_PRODUCTS.Add(sTORE_PRODUCTS_UPD);
                try { db.SaveChanges(); ViewBag.StoreEditMessage="Product Details updated successfully."; }
                catch (Exception e) { Console.WriteLine(e); ViewBag.StoreEditMessage = e.InnerException.InnerException.Message; }

                STORE_PRODUCTS sTORE_PRODUCTS_ORG = db.STORE_PRODUCTS.Find(sTORE_PRODUCTS.PRODUCT_ID);
                sTORE_PRODUCTS_ORG.IS_ACT = "N";
                sTORE_PRODUCTS_ORG.UPDATED_AT = DateTime.Now;
                db.Entry(sTORE_PRODUCTS_ORG).State = EntityState.Modified;
                try { db.SaveChanges(); ViewBag.StoreEditMessage = string.Concat(ViewBag.StoreEditMessage,"Privious information saved."); }
                catch (Exception e) { Console.WriteLine(e); ViewBag.StoreEditMessage = string.Concat(ViewBag.StoreEditMessage, e.InnerException.InnerException.Message); }

                ViewBag.CATEGORY_ID = new SelectList(db.STORE_CATEGORY, "ID", "NAME", sTORE_PRODUCTS.CATEGORY_ID);
                ViewBag.SUB_CATEGORY_ID = new SelectList(db.STORE_SUB_CATEGORY, "ID", "NAME", sTORE_PRODUCTS.SUB_CATEGORY_ID);
                ViewBag.PURCHASED_THROUGH = sTORE_PRODUCTS.PURCHASED_THROUGH;
                ViewBag.PAID_BY = sTORE_PRODUCTS.PAID_BY;
                DateTime PDateIner = Convert.ToDateTime(sTORE_PRODUCTS.PURCHASED_ON);
                ViewBag.PURCHASED_ON = PDateIner.ToString("dd/mm/yyyy");
                return View(sTORE_PRODUCTS);
            }
            ViewBag.CATEGORY_ID = new SelectList(db.STORE_CATEGORY, "ID", "NAME", sTORE_PRODUCTS.CATEGORY_ID);
            /////Query for selecting Subcategory**********************
            var queryCatSubcat = (from sc in db.STORE_CATEGORY
                                  join ssc in db.STORE_SUB_CATEGORY on sc.ID equals ssc.STORE_CATEGORY_ID
                                  select new Models.SubCategory { CategoryData = sc, SubCategoryData = ssc })
                        .OrderBy(x => x.CategoryData.ID).ToList();


            List<SelectListItem> options = new List<SelectListItem>();
            foreach (var item in queryCatSubcat)
            {
                string CatFullName = string.Concat(item.CategoryData.NAME, "-", item.SubCategoryData.NAME);
                var result = new SelectListItem();
                result.Text = CatFullName;
                result.Value = item.SubCategoryData.ID.ToString();
                result.Selected = item.SubCategoryData.ID == sTORE_PRODUCTS.SUB_CATEGORY_ID ? true : false;
                options.Add(result);
            }
            // add the 'ALL' option
            options.Insert(0, new SelectListItem() { Value = "-1", Text = "Select Subcategory" });
            ViewBag.SUB_CATEGORY_ID = options;
            ///// End 
            //ViewBag.SUB_CATEGORY_ID = new SelectList(db.STORE_SUB_CATEGORY.Where(x=>x.STORE_CATEGORY_ID == sTORE_PRODUCTS.CATEGORY_ID), "ID", "NAME", sTORE_PRODUCTS.SUB_CATEGORY_ID);
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
            ViewBag.CATEGORY_ID = new SelectList(db.STORE_CATEGORY, "ID", "NAME", sTORE_PRODUCTS.CATEGORY_ID);
            /////Query for selecting Subcategory**********************
            var queryCatSubcat = (from sc in db.STORE_CATEGORY
                                  join ssc in db.STORE_SUB_CATEGORY on sc.ID equals ssc.STORE_CATEGORY_ID
                                  select new Models.SubCategory { CategoryData = sc, SubCategoryData = ssc })
                        .OrderBy(x => x.CategoryData.ID).ToList();


            List<SelectListItem> options = new List<SelectListItem>();
            foreach (var item in queryCatSubcat)
            {
                string CatFullName = string.Concat(item.CategoryData.NAME, "-", item.SubCategoryData.NAME);
                var result = new SelectListItem();
                result.Text = CatFullName;
                result.Value = item.SubCategoryData.ID.ToString();
                result.Selected = item.SubCategoryData.ID == sTORE_PRODUCTS.SUB_CATEGORY_ID ? true : false;
                options.Add(result);
            }
            // add the 'ALL' option
            options.Insert(0, new SelectListItem() { Value = "-1", Text = "Select Subcategory" });
            ViewBag.SUB_CATEGORY_ID = options;
            ///// End 
            //ViewBag.SUB_CATEGORY_ID = new SelectList(db.STORE_SUB_CATEGORY.Where(x=>x.STORE_CATEGORY_ID == sTORE_PRODUCTS.CATEGORY_ID), "ID", "NAME", sTORE_PRODUCTS.SUB_CATEGORY_ID);
            ViewBag.PURCHASED_THROUGH = sTORE_PRODUCTS.PURCHASED_THROUGH;
            ViewBag.PAID_BY = sTORE_PRODUCTS.PAID_BY;
            DateTime PDate = Convert.ToDateTime(sTORE_PRODUCTS.PURCHASED_ON);
            ViewBag.PURCHASED_ON = PDate.ToString("dd/mm/yyyy");

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
            try { db.SaveChanges(); ViewBag.StoreDeleteMessage = "Product Deleted Successfully."; }
            catch (Exception e) { Console.WriteLine(e); ViewBag.StoreDeleteMessage =  e.InnerException.InnerException.Message; }

            ViewBag.CATEGORY_ID = new SelectList(db.STORE_CATEGORY, "ID", "NAME", sTORE_PRODUCTS.CATEGORY_ID);
            /////Query for selecting Subcategory**********************
            var queryCatSubcat = (from sc in db.STORE_CATEGORY
                                  join ssc in db.STORE_SUB_CATEGORY on sc.ID equals ssc.STORE_CATEGORY_ID
                                  select new Models.SubCategory { CategoryData = sc, SubCategoryData = ssc })
                        .OrderBy(x => x.CategoryData.ID).ToList();


            List<SelectListItem> options = new List<SelectListItem>();
            foreach (var item in queryCatSubcat)
            {
                string CatFullName = string.Concat(item.CategoryData.NAME, "-", item.SubCategoryData.NAME);
                var result = new SelectListItem();
                result.Text = CatFullName;
                result.Value = item.SubCategoryData.ID.ToString();
                result.Selected = item.SubCategoryData.ID == sTORE_PRODUCTS.SUB_CATEGORY_ID ? true : false;
                options.Add(result);
            }
            // add the 'ALL' option
            options.Insert(0, new SelectListItem() { Value = "-1", Text = "Select Subcategory" });
            ViewBag.SUB_CATEGORY_ID = options;
            ///// End 
            //ViewBag.SUB_CATEGORY_ID = new SelectList(db.STORE_SUB_CATEGORY.Where(x=>x.STORE_CATEGORY_ID == sTORE_PRODUCTS.CATEGORY_ID), "ID", "NAME", sTORE_PRODUCTS.SUB_CATEGORY_ID);
            ViewBag.PURCHASED_THROUGH = sTORE_PRODUCTS.PURCHASED_THROUGH;
            ViewBag.PAID_BY = sTORE_PRODUCTS.PAID_BY;
            DateTime PDate = Convert.ToDateTime(sTORE_PRODUCTS.PURCHASED_ON);
            ViewBag.PURCHASED_ON = PDate.ToString("dd/mm/yyyy");

            return View(sTORE_PRODUCTS);
        }

        // GET: Student
        public ActionResult ViewAll(string searchString, string searchString2)
        {
            ViewBag.CartItems = (from t in db.STORE_PURCHAGE_CART
                                 select t).Count();
            int? searchStringId = null;

            if (!string.IsNullOrEmpty(searchString) && searchString != "-1")
            {
                searchStringId = Convert.ToInt32(searchString);
                //searchString = db.STORE_CATEGORY.Find(searchStringId).NAME.ToString();
            }

            List<SelectListItem> options = new SelectList(db.STORE_CATEGORY.Where(x => x.IS_DEL == "N" && x.IS_ACT == "Y").OrderBy(x => x.ID), "ID", "NAME").ToList();
            // add the 'ALL' option
            options.Insert(0, new SelectListItem() { Value = "-1", Text = "ALL" });
            ViewBag.searchString = options;

            List<SelectListItem> options2 = new SelectList(db.STORE_SUB_CATEGORY.Where(x => x.IS_DEL == "N" && x.IS_ACT == "Y" && x.STORE_CATEGORY_ID == searchStringId).OrderBy(x => x.ID), "ID", "NAME", searchString2).ToList();
            // add the 'ALL' option
            options2.Insert(0, new SelectListItem() { Value = "-1", Text = "ALL" });
            ViewBag.searchString2 = options2;

            if (!string.IsNullOrEmpty(searchString) && searchString != "-1")
            {
                ViewBag.IsPostBack = 1;
            }
            return View(db.STORE_CATEGORY.ToList());
        }

        // GET: Store Products
        public ActionResult ListAllProducts(string sortOrder, string currentFilter, string searchString, string currentFilter2, string searchString2, int? page)
        {

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            int? searchStringId = null;
            int? searchStringId2 = null;

            if (!string.IsNullOrEmpty(searchString) && searchString != "-1")
            {
                page = 1;
                ///As Drop down list sends Id, we will ahve to convert this to text which is different from text box
                searchStringId = Convert.ToInt32(searchString);
                //searchString = db.STORE_CATEGORY.Find(searchStringId).NAME.ToString();
            } 
            else 
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            if (!string.IsNullOrEmpty(searchString2) && searchString2 != "-1") { page = 1; searchStringId2 = Convert.ToInt32(searchString2); }
            else { searchString2 = currentFilter2; }
            ViewBag.CurrentFilter2 = searchString2;

            var ProductS = (from pd in db.STORE_PRODUCTS
                            join ct in db.STORE_CATEGORY on pd.CATEGORY_ID equals ct.ID
                            join subcat in db.STORE_SUB_CATEGORY on pd.SUB_CATEGORY_ID equals subcat.ID into gsc
                            from subgsc in gsc.DefaultIfEmpty()
                            orderby pd.NAME, ct.NAME
                            where pd.IS_DEL == "N" && pd.IS_ACT == "Y"
                            select new Models.Products { ProductData = pd, CategoryData = ct, SubCategoryData = (subgsc == null ? null : subgsc) }).Distinct();

            if (!String.IsNullOrEmpty(searchString))
            {
                ProductS = ProductS.Where(s => s.CategoryData.ID == searchStringId);
            }
            if (!String.IsNullOrEmpty(searchString2))
            {
                ProductS = ProductS.Where(s => s.SubCategoryData.ID == searchStringId2);
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

            List<SelectListItem> options = new SelectList(db.STORE_CATEGORY.Where(x => x.IS_DEL == "N" && x.IS_ACT == "Y").OrderBy(x => x.ID), "ID", "NAME").ToList();
            // add the 'ALL' option
            options.Insert(0, new SelectListItem() { Value = "-1", Text = "ALL" });
            ViewBag.searchString = options;

            List<SelectListItem> options2 = new SelectList(db.STORE_SUB_CATEGORY.Where(x => x.IS_DEL == "N" && x.IS_ACT == "Y" && x.STORE_CATEGORY_ID == searchStringId).OrderBy(x => x.ID), "ID", "NAME", searchString2).ToList();
            // add the 'ALL' option
            options2.Insert(0, new SelectListItem() { Value = "-1", Text = "ALL" });
            ViewBag.searchString2 = options2;

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
            var sTOREcATEGORY = db.STORE_CATEGORY.Where(x=>x.IS_ACT=="Y" && x.IS_DEL=="N").ToList();
            return View(sTOREcATEGORY);
        }

        // GET: Student/Delete/5
        public ActionResult _stCategoriesDelete(int? id)
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
        [HttpPost, ActionName("_stCategoriesDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult _stCategoriesDeleteConfirmed(int id)
        {
            STORE_CATEGORY sTOREcATEGORY = db.STORE_CATEGORY.Find(id);
            db.STORE_CATEGORY.Remove(sTOREcATEGORY);
            try { db.SaveChanges(); ViewBag.CatDeleteMessage = "Store Category deleted successfully."; }
            catch (Exception e) { Console.WriteLine(e); ViewBag.CatDeleteMessage = e.InnerException.InnerException.Message; }
            return View(sTOREcATEGORY);
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
        public ActionResult _CategoriesEdit([Bind(Include = "ID,NAME,IS_ACT")] STORE_CATEGORY sTOREcATEGORY)
        {
            if (ModelState.IsValid)
            {
                STORE_CATEGORY sTOREcATEGORY_UPD = db.STORE_CATEGORY.Find(sTOREcATEGORY.ID);
                sTOREcATEGORY_UPD.NAME = sTOREcATEGORY.NAME;
                sTOREcATEGORY_UPD.IS_ACT = sTOREcATEGORY.IS_ACT;
                db.Entry(sTOREcATEGORY_UPD).State = EntityState.Modified;
                try { db.SaveChanges(); ViewBag.CatEditMessage = "Store Category edited successfully."; }
                catch (Exception e) { Console.WriteLine(e); ViewBag.CatEditMessage = e.InnerException.InnerException.Message; }
                return View(sTOREcATEGORY);
            }
            return View(sTOREcATEGORY);
        }

        // POST: Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CategoriesCreate([Bind(Include = "ID,NAME,IS_ACT")] STORE_CATEGORY sTOREcATEGORY)
        {
            if (ModelState.IsValid)
            {
                sTOREcATEGORY.IS_DEL = "N";
                sTOREcATEGORY.CREATED_AT = System.DateTime.Now;
                sTOREcATEGORY.UPDATED_AT = System.DateTime.Now;
                db.STORE_CATEGORY.Add(sTOREcATEGORY);
                try { db.SaveChanges(); ViewBag.CatCreateMessage = "Store Category created successfully."; }
                catch (Exception e) { Console.WriteLine(e); ViewBag.CatCreateMessage = e.InnerException.InnerException.Message; }
                return RedirectToAction("Categories");
            }

            return View(sTOREcATEGORY);
        }

        // GET: Student/Details/5
        public ActionResult SubCategories(int Category_Id)
        {
            ViewBag.STORE_CATEGORY_ID = Category_Id;
            ViewBag.CATEGORY_ID = new SelectList(db.STORE_CATEGORY, "ID", "NAME", Category_Id);
            return View();
        }

        // GET: Student/Details/5
        [ChildActionOnly]
        public ActionResult _SubCategoriesList(int Category_Id)
        {
            var sTOREsUBcATEGORY = (from subcat in db.STORE_SUB_CATEGORY
                                    join cat in db.STORE_CATEGORY on subcat.STORE_CATEGORY_ID equals cat.ID
                                    where cat.ID == Category_Id
                                    select new Models.SubCategory { SubCategoryData = subcat, CategoryData = cat })
                                    .OrderBy(x => x.SubCategoryData.NAME).ToList();

            return View(sTOREsUBcATEGORY);
        }

        // GET: Student/Delete/5
        public ActionResult _SubCategoriesDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STORE_SUB_CATEGORY sTOREsUBcATEGORY = db.STORE_SUB_CATEGORY.Find(id);
            if (sTOREsUBcATEGORY == null)
            {
                return HttpNotFound();
            }
            return View(sTOREsUBcATEGORY);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("_SubCategoriesDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult _SubCategoriesDeleteConfirmed(int id)
        {
            STORE_SUB_CATEGORY sTOREsUBcATEGORY = db.STORE_SUB_CATEGORY.Find(id);
            db.STORE_SUB_CATEGORY.Remove(sTOREsUBcATEGORY);
            try { db.SaveChanges(); ViewBag.SubCatDeteleMessage = "Store Sub Category deleted successfully."; }
            catch (Exception e) { Console.WriteLine(e); ViewBag.SubCatDeteleMessage = e.InnerException.InnerException.Message; }
            return View(sTOREsUBcATEGORY);
        }

        // GET: Student/Edit/5
        public ActionResult _SubCategoriesEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STORE_SUB_CATEGORY sTOREsUBcATEGORY = db.STORE_SUB_CATEGORY.Find(id);
            if (sTOREsUBcATEGORY == null)
            {
                return HttpNotFound();
            }
            return View(sTOREsUBcATEGORY);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _SubCategoriesEdit([Bind(Include = "ID,STORE_CATEGORY_ID,NAME,IS_ACT")] STORE_SUB_CATEGORY sTOREsUBcATEGORY)
        {
            if (ModelState.IsValid)
            {
                STORE_SUB_CATEGORY sTOREsUBcATEGORY_UPD = db.STORE_SUB_CATEGORY.Find(sTOREsUBcATEGORY.ID);
                sTOREsUBcATEGORY_UPD.NAME = sTOREsUBcATEGORY.NAME;
                sTOREsUBcATEGORY_UPD.IS_ACT = sTOREsUBcATEGORY.IS_ACT;
                db.Entry(sTOREsUBcATEGORY_UPD).State = EntityState.Modified;
                try { db.SaveChanges(); ViewBag.SubCatEditMessage = "Store Sub Category edited successfully."; }
                catch (Exception e) { Console.WriteLine(e); ViewBag.SubCatEditMessage = e.InnerException.InnerException.Message; }
                return View(sTOREsUBcATEGORY);
            }
            return View(sTOREsUBcATEGORY);
        }

        // POST: Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubCategoriesCreate([Bind(Include = "ID,STORE_CATEGORY_ID,NAME,IS_ACT")] STORE_SUB_CATEGORY sTOREsUBcATEGORY, string CATEGORY_ID)
        {
            if (ModelState.IsValid)
            {
                sTOREsUBcATEGORY.STORE_CATEGORY_ID = Convert.ToInt32(CATEGORY_ID);
                sTOREsUBcATEGORY.IS_DEL = "N";
                sTOREsUBcATEGORY.CREATED_AT = System.DateTime.Now;
                sTOREsUBcATEGORY.UPDATED_AT = System.DateTime.Now;
                db.STORE_SUB_CATEGORY.Add(sTOREsUBcATEGORY);
                try { db.SaveChanges(); ViewBag.SubCatCreateMessage = "Sub Category created successfully."; }
                catch (Exception e) { Console.WriteLine(e); ViewBag.SubCatCreateMessage = e.InnerException.InnerException.Message; }
                return RedirectToAction("SubCategories", new { Category_Id = sTOREsUBcATEGORY.STORE_CATEGORY_ID });
            }

            return View(sTOREsUBcATEGORY);
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
        public ActionResult ViewAllSelling(string sortOrder, string currentFilter, string searchString, int? page, string currentFilter2, string StudentName, string currentFilter3, string ContactNumber, string currentFilter4, string ReceivedBy, string currentFilter5, string MoneyDeposited, string currentFilter9, string SoldFromDate, string currentFilter10, string SoldToDate)
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

            int pageSize = 50;
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
        public ActionResult EditSelling([Bind(Include = "ID,PRODUCT_ID,UNIT_SOLD,SOLD_PRICE,SOLD_BY,SOLD_ON,STUDENT_NAME,STUDENT_CONTACT_NO,MONEY_RECEIVED_BY,IS_DEPOSITED,IS_ACT,IS_DEL")] STORE_PURCHAGE sTORE_PURCHASE)
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
                try { db.SaveChanges(); ViewBag.sellingEditMessage = "Selling Details are updated successfully."; }
                catch (Exception e) { Console.WriteLine(e); ViewBag.sellingEditMessage = e.InnerException.InnerException.Message; }
                ViewBag.PRODUCT_ID = new SelectList(db.STORE_PRODUCTS, "PRODUCT_ID", "NAME", sTORE_PURCHASE.PRODUCT_ID);
                DateTime SDate2 = Convert.ToDateTime(sTORE_PURCHASE.SOLD_ON);
                ViewBag.SOLD_ON = SDate2.ToShortDateString();
                return View(sTORE_PURCHASE);
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
        public ActionResult Payment(string PAYMENT_MODE, string STUDENT_NAME, decimal? PAYMENT_AMOUNT, long? PHONE_NUMBER, string PURCHAGE_DATE)
        {
            ViewBag.STUDENT_NAME = STUDENT_NAME;
            ViewBag.PHONE_NUMBER = PHONE_NUMBER;
            DateTime PDate = Convert.ToDateTime(PURCHAGE_DATE);
            ViewBag.PURCHAGE_DATE = PDate.ToShortDateString();
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

            var IDList = new int[100];
            int i = 0;

            //var pURcART = (from res in db.STORE_PURCHAGE_CART
            //             select res).ToList();

            var paymentsOrg = from p in db.STORE_PURCHAGE_CART
                               //where p.CREATED_AT.Value.ToShortDateString() == DateTime.Now.ToShortDateString()
                           group p by p.PRODUCT_ID into g
                           select new
                           {
                               ProductNo = g.Key,
                               UNIT_SOLD = g.Count(),
                               AMNT = g.Sum(x => x.SOLD_PRICE),
                               PUR_DATE = DateTime.Now,
                               SOLD_BY = g.FirstOrDefault().SOLD_BY,
                               CREATED_AT = g.FirstOrDefault().CREATED_AT,
                               UPDATED_AT = g.FirstOrDefault().UPDATED_AT
                           };

            var ProductSOrg = (from pd in db.STORE_PRODUCTS
                            join ct in db.STORE_CATEGORY on pd.CATEGORY_ID equals ct.ID
                            join pct in paymentsOrg on pd.PRODUCT_ID equals pct.ProductNo
                            orderby pd.NAME, ct.NAME
                            select new Models.PurchaseCart { ProductData = pd, CategoryData = ct, UNIT_SOLD = pct.UNIT_SOLD, SOLD_AMNT = pct.AMNT, PUR_DATE = pct.PUR_DATE, SOLD_BY=pct.SOLD_BY, CREATED_AT = pct.CREATED_AT, UPDATED_AT = pct.UPDATED_AT }).Distinct();

            foreach (var PurCarList in ProductSOrg.ToList())
            {

                //STORE_PURCHAGE StorePur = db.STORE_PURCHAGE.Find(PurCarList.ID);
                var StorePur = new STORE_PURCHAGE() { 
               PRODUCT_ID = PurCarList.ProductData.PRODUCT_ID,
               UNIT_SOLD = PurCarList.UNIT_SOLD,
               SOLD_PRICE = (int) PurCarList.SOLD_AMNT,
               SOLD_BY = PurCarList.SOLD_BY,
               SOLD_ON = PDate,
               STUDENT_NAME = STUDENT_NAME,
               STUDENT_CONTACT_NO = PHONE_NUMBER,
               MONEY_RECEIVED_BY = PAYMENT_MODE,
               IS_DEPOSITED = "N",
               IS_ACT = "Y",
               IS_DEL = "N",
               CREATED_AT = PurCarList.CREATED_AT,
               UPDATED_AT = PurCarList.UPDATED_AT,
                };
               db.STORE_PURCHAGE.Add(StorePur);
                try { db.SaveChanges(); ViewBag.PaymentMessage = "Payment Done Successfully."; }
                catch (Exception e) { Console.WriteLine(e); ViewBag.StoreDeleteMessage = e.InnerException.InnerException.Message; }
                IDList[i] = StorePur.ID;
                i++;

            }

            var payments = from p in db.STORE_PURCHAGE.Where(a => IDList.Any(s => a.ID.Equals(s)))
                           select new
                           {
                               ProductNo = p.PRODUCT_ID,
                               UNIT_SOLD = p.UNIT_SOLD,
                               AMNT = p.SOLD_PRICE,
                               PUR_DATE = DateTime.Now
                           };

            var ProductS = (from pd in db.STORE_PRODUCTS
                            join ct in db.STORE_CATEGORY on pd.CATEGORY_ID equals ct.ID
                            join pct in payments on pd.PRODUCT_ID equals pct.ProductNo
                            orderby pd.NAME, ct.NAME
                            select new Models.PurchaseCart { ProductData = pd, CategoryData = ct, UNIT_SOLD = pct.UNIT_SOLD, SOLD_AMNT = pct.AMNT, PUR_DATE = pct.PUR_DATE }).Distinct();

            try { db.Database.ExecuteSqlCommand("DELETE FROM STORE_PURCHAGE_CART"); ViewBag.PaymentMessage = string.Concat(ViewBag.PaymentMessage,"Cart cleared now."); }
            catch (Exception e) { Console.WriteLine(e); ViewBag.StoreDeleteMessage = string.Concat(ViewBag.PaymentMessage, e.InnerException.InnerException.Message); }
          
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


        public ActionResult BackupTransaction(string sortOrder, string currentFilter, string searchString, int? page, string currentFilter2, string StudentName, string currentFilter3, string ContactNumber, string currentFilter4, string ReceivedBy, string currentFilter5, string MoneyDeposited, string currentFilter9, string SoldFromDate, string currentFilter10, string SoldToDate)
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

            int pageSize = 50;
            int pageNumber = (page ?? 1);
            return View(PurchaseS.ToPagedList(pageNumber, pageSize));
            //return View(db.USERS.ToList());
        }

        // GET: Student
        [HttpGet]
        public ActionResult BackupSelectedTransactions(string sortOrder, string currentFilter, string searchString, int? page, string currentFilter2, string StudentName, string currentFilter3, string ContactNumber, string currentFilter4, string ReceivedBy, string currentFilter5, string MoneyDeposited, string currentFilter9, string SoldFromDate, string currentFilter10, string SoldToDate)
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
            string success1 = ""; string success2 = "";
            foreach (var item in PurchaseS.ToList())
            {
                var ST_PUR_BACKUp = new STORE_PURCHAGE_BACKUP() { ID = item.PurchaseData.ID, PRODUCT_ID = item.PurchaseData.PRODUCT_ID, UNIT_SOLD= item.PurchaseData.UNIT_SOLD, SOLD_PRICE=item.PurchaseData.SOLD_PRICE, SOLD_BY=item.PurchaseData.SOLD_BY, SOLD_ON=item.PurchaseData.SOLD_ON, STUDENT_NAME=item.PurchaseData.STUDENT_NAME, STUDENT_CONTACT_NO=item.PurchaseData.STUDENT_CONTACT_NO, MONEY_RECEIVED_BY=item.PurchaseData.MONEY_RECEIVED_BY, IS_DEPOSITED=item.PurchaseData.IS_DEPOSITED, IS_ACT=item.PurchaseData.IS_ACT, IS_DEL=item.PurchaseData.IS_DEL, CREATED_AT=item.PurchaseData.CREATED_AT, UPDATED_AT= item.PurchaseData.UPDATED_AT };
                db.STORE_PURCHAGE_BACKUP.Add(ST_PUR_BACKUp);
                try { db.SaveChanges(); success1 = "Y"; }
                catch (Exception e) { Console.WriteLine(e); Session["StoreBackUpMessage"] = e.InnerException.InnerException.Message; }
            }
            foreach (var item in PurchaseS.ToList())
            {
                STORE_PURCHAGE sPURCHAGE = db.STORE_PURCHAGE.Find(item.PurchaseData.ID);
                db.STORE_PURCHAGE.Remove(sPURCHAGE);
                try { db.SaveChanges(); success2 = "Y"; }
                catch (Exception e) { Console.WriteLine(e); Session["StoreBackUpMessage"] = string.Concat(Session["StoreBackUpMessage"], e.InnerException.InnerException.Message); }
            }

            if(success1 != "" && success2 != "")
            {
                Session["StoreBackUpMessage"] = "Selected Data is backed up successfully.";
            }
            //return RedirectToAction("ViewAll");
            return RedirectToAction("BackupTransaction", "Store", new { sortOrder = ViewBag.NameSortParm, currentFilter = ViewBag.CurrentFilter, currentFilter2 = ViewBag.CurrentFilter2, currentFilter3 = ViewBag.CurrentFilter3, currentFilter4 = ViewBag.CurrentFilter4, currentFilter5 = ViewBag.CurrentFilter5, currentFilter9 = ViewBag.CurrentFilter9, currentFilter10 = ViewBag.CurrentFilter10});

        }

        // GET: Student/Details/5
        public ActionResult PurchageOrder(string CATEGORY_ID, string SUBCATEGORY_ID, string PRODUCT_ID, string VENDOR_ID, int? ORDER_QUANTITY)
        {
            int? searchStringId = null;
            int? searchStringId2 = null;

            if (!string.IsNullOrEmpty(CATEGORY_ID) && CATEGORY_ID != "-1")
            {
                searchStringId = Convert.ToInt32(CATEGORY_ID);
                ViewBag.IsPostBack = 1;
            }

            if (!string.IsNullOrEmpty(SUBCATEGORY_ID) && SUBCATEGORY_ID != "-1")
            {
                searchStringId2 = Convert.ToInt32(SUBCATEGORY_ID);
                ViewBag.IsPostBack2 = 1;
            }
            ViewBag.ORDER_QUANTITY = ORDER_QUANTITY;

            List<SelectListItem> options = new SelectList(db.STORE_CATEGORY.Where(x => x.IS_DEL == "N" && x.IS_ACT == "Y").OrderBy(x => x.ID), "ID", "NAME", CATEGORY_ID).ToList();
            // add the 'ALL' option
            options.Insert(0, new SelectListItem() { Value = "-1", Text = "Select Product Category" });
            ViewBag.CATEGORY_ID = options;

            List<SelectListItem> options2 = new SelectList(db.STORE_SUB_CATEGORY.Where(x => x.IS_DEL == "N" && x.IS_ACT == "Y" && x.STORE_CATEGORY_ID == searchStringId).OrderBy(x => x.ID), "ID", "NAME", SUBCATEGORY_ID).ToList();
            // add the 'ALL' option
            options2.Insert(0, new SelectListItem() { Value = "-1", Text = "Select Sub Category" });
            ViewBag.SUBCATEGORY_ID = options2;

            List<SelectListItem> options3 = new SelectList(db.STORE_PRODUCTS.Where(x => x.IS_DEL == "N" && x.IS_ACT == "Y" && x.CATEGORY_ID == searchStringId && x.SUB_CATEGORY_ID == searchStringId2).OrderBy(x => x.PRODUCT_ID), "PRODUCT_ID", "NAME", PRODUCT_ID).ToList();
            // add the 'ALL' option
            options3.Insert(0, new SelectListItem() { Value = "-1", Text = "Select Product" });
            ViewBag.PRODUCT_ID = options3;

            List<SelectListItem> options4 = new SelectList(db.STORE_PURCHAGE_VENDOR.Where(x => x.IS_DEL == "N").OrderBy(x => x.ID), "ID", "NAME", VENDOR_ID).ToList();
            // add the 'ALL' option
            options4.Insert(0, new SelectListItem() { Value = "-1", Text = "Select Vendor" });
            ViewBag.VENDOR_ID = options4;

            return View();
        }

        // GET: Student/Details/5
        [ChildActionOnly]
        public ActionResult PurchageOrderList()
        {
            // A collection of strings to check against
            //var Status = new int[] { 1, "String B", "String C" };
            // Only grab the Actions where your Agent property contains any of the strings in your previous collection
            //actions = db.Actions.Where(a => search.Any(s => a.Agent.Contains(s)));
            var pURCHAGEoRDERS = (from po in db.STORE_PURCHAGE_ORDER
                                  join sps in db.STORE_PURCHAGE_STATUS on po.STATUS_ID equals sps.ID
                                  join spv in db.STORE_PURCHAGE_VENDOR on po.VENDOR_ID equals spv.ID into gspv
                                  from subgspv in gspv.DefaultIfEmpty()
                                  join pd in db.STORE_PRODUCTS on po.PRODUCT_ID equals pd.PRODUCT_ID
                            join ct in db.STORE_CATEGORY on pd.CATEGORY_ID equals ct.ID
                            join subcat in db.STORE_SUB_CATEGORY on pd.SUB_CATEGORY_ID equals subcat.ID into gsc
                            from subgsc in gsc.DefaultIfEmpty()
                            join usr in db.USERS on po.EMPLOYEE_ID equals usr.ID
                            orderby pd.NAME, ct.NAME
                            where sps.NAME == "Pending" || sps.NAME == "Approved"
                                  select new Models.PurchageOrder { PurchaseOrderData = po,PurchageStatusData = sps, PurchageVendorData= (subgspv == null ? null : subgspv), ProductData = pd, CategoryData = ct, SubCategoryData = (subgsc == null ? null : subgsc), EmployeeData = usr }).ToList();

            //var pURCHAGEoRDERS = db.STORE_PURCHAGE_ORDER.Where(x => x.STATUS == 1 || x.STATUS == 2).ToList();
            return View(pURCHAGEoRDERS);
        }

        // GET: Student/Delete/5
        public ActionResult PurchageOrderDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STORE_PURCHAGE_ORDER sTOREpURCHAGEoRDER = db.STORE_PURCHAGE_ORDER.Find(id);
            if (sTOREpURCHAGEoRDER == null)
            {
                return HttpNotFound();
            }
            return View(sTOREpURCHAGEoRDER);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("PurchageOrderDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult PurchageOrderDeleteConfirmed(int id)
        {
            STORE_PURCHAGE_ORDER sTOREpURCHAGEoRDER = db.STORE_PURCHAGE_ORDER.Find(id);
            db.STORE_PURCHAGE_ORDER.Remove(sTOREpURCHAGEoRDER);
            try { db.SaveChanges(); ViewBag.PODeleteMessage = "Purchage Order deleted successfully."; }
            catch (Exception e) { Console.WriteLine(e); ViewBag.PODeleteMessage = e.InnerException.InnerException.Message; }
            return View(sTOREpURCHAGEoRDER);
        }

        // GET: Student/Edit/5
        public ActionResult PurchageOrderEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STORE_PURCHAGE_ORDER sTOREpURCHAGEoRDER = db.STORE_PURCHAGE_ORDER.Find(id);

            sTOREpURCHAGEoRDER.REVISION_NUMBER += 1;
            DateTime OrdDate = Convert.ToDateTime(System.DateTime.Now);
            if (sTOREpURCHAGEoRDER.ORDER_DATE != null)
            {
                OrdDate = Convert.ToDateTime(sTOREpURCHAGEoRDER.ORDER_DATE);
            }
            ViewBag.ORDER_DATE = OrdDate.ToShortDateString();
            DateTime ShipDate = Convert.ToDateTime(System.DateTime.Now);
            if (sTOREpURCHAGEoRDER.SHIP_DATE != null)
            {
                ShipDate = Convert.ToDateTime(sTOREpURCHAGEoRDER.SHIP_DATE);
            }
            ViewBag.SHIP_DATE = ShipDate.ToShortDateString();

            STORE_PRODUCTS prod = db.STORE_PRODUCTS.Find(sTOREpURCHAGEoRDER.PRODUCT_ID);
            ViewBag.PRODUCT_ID = prod.NAME;

            List<SelectListItem> options2 = new SelectList(db.STORE_PURCHAGE_VENDOR.Where(x => x.IS_DEL == "N").OrderBy(x => x.ID), "ID", "NAME", sTOREpURCHAGEoRDER.VENDOR_ID).ToList();
            // add the 'ALL' option
            options2.Insert(0, new SelectListItem() { Value = "-1", Text = "Select Vendor" });
            ViewBag.VENDOR_ID = options2;

            List<SelectListItem> options3 = new SelectList(db.STORE_PURCHAGE_STATUS.Where(x => x.IS_DEL == "N").OrderBy(x => x.ID), "ID", "NAME", sTOREpURCHAGEoRDER.STATUS_ID).ToList();
            // add the 'ALL' option
            options3.Insert(0, new SelectListItem() { Value = "-1", Text = "Select PO Status" });
            ViewBag.STATUS_ID = options3;

            List<SelectListItem> options4 = new SelectList(db.EMPLOYEEs.OrderBy(x => x.ID), "ID", "EMP_NUM", sTOREpURCHAGEoRDER.EMPLOYEE_ID).ToList();
            // add the 'ALL' option
            options4.Insert(0, new SelectListItem() { Value = "-1", Text = "Select Employee" });
            ViewBag.EMPLOYEE_ID = options4;

            if (sTOREpURCHAGEoRDER == null)
            {
                return HttpNotFound();
            }
            return View(sTOREpURCHAGEoRDER);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PurchageOrderEdit([Bind(Include = "ID,PO_NUMBER,PRODUCT_ID,REVISION_NUMBER,STATUS_ID, EMPLOYEE_ID, VENDOR_ID, SHIP_METHOD_ID,ORDER_DATE, ORDER_QUANTITY, SHIP_DATE, SUB_TOTAL, TAX_AMT, FREIGHT, TOTAL_DUE")] STORE_PURCHAGE_ORDER sTOREpURCHAGEoRDER)
        {
            if (ModelState.IsValid)
            {
                STORE_PURCHAGE_ORDER sTOREpURCHAGEoRDER_UPD = db.STORE_PURCHAGE_ORDER.Find(sTOREpURCHAGEoRDER.ID);
                sTOREpURCHAGEoRDER_UPD.REVISION_NUMBER = sTOREpURCHAGEoRDER.REVISION_NUMBER;
                sTOREpURCHAGEoRDER_UPD.STATUS_ID = sTOREpURCHAGEoRDER.STATUS_ID;
                sTOREpURCHAGEoRDER_UPD.EMPLOYEE_ID = sTOREpURCHAGEoRDER.EMPLOYEE_ID;
                sTOREpURCHAGEoRDER_UPD.VENDOR_ID = sTOREpURCHAGEoRDER.VENDOR_ID;
                sTOREpURCHAGEoRDER_UPD.SHIP_METHOD_ID = sTOREpURCHAGEoRDER.SHIP_METHOD_ID;
                sTOREpURCHAGEoRDER_UPD.ORDER_DATE = sTOREpURCHAGEoRDER.ORDER_DATE;
                sTOREpURCHAGEoRDER_UPD.ORDER_QUANTITY = sTOREpURCHAGEoRDER.ORDER_QUANTITY;
                sTOREpURCHAGEoRDER_UPD.SHIP_DATE = sTOREpURCHAGEoRDER.SHIP_DATE;
                sTOREpURCHAGEoRDER_UPD.TOTAL_DUE = sTOREpURCHAGEoRDER.TOTAL_DUE;
                sTOREpURCHAGEoRDER_UPD.FREIGHT = sTOREpURCHAGEoRDER.FREIGHT;
                sTOREpURCHAGEoRDER_UPD.TAX_AMT = sTOREpURCHAGEoRDER.TAX_AMT;
                sTOREpURCHAGEoRDER_UPD.SUB_TOTAL = sTOREpURCHAGEoRDER.SUB_TOTAL;
                sTOREpURCHAGEoRDER_UPD.UPDATED_AT = System.DateTime.Now;
                if(sTOREpURCHAGEoRDER_UPD.STATUS_ID == 4 && (sTOREpURCHAGEoRDER_UPD.SHIP_DATE==null || sTOREpURCHAGEoRDER_UPD.TOTAL_DUE==null || sTOREpURCHAGEoRDER_UPD.FREIGHT==null || sTOREpURCHAGEoRDER_UPD.TAX_AMT==null || sTOREpURCHAGEoRDER_UPD.SUB_TOTAL==null))
                {
                    ViewBag.POEditMessage = "Ship Date, Total Due, Frieght Cost, Tax Amount and Sub Total Cost is Mandatory to make PO Complete.";
                }
                else
                {
                    db.Entry(sTOREpURCHAGEoRDER_UPD).State = EntityState.Modified;
                    try { db.SaveChanges(); ViewBag.POEditMessage = "Purchage Order edited successfully."; }
                    catch (Exception e) { Console.WriteLine(e); ViewBag.POEditMessage = e.InnerException.InnerException.Message; }
                    if (sTOREpURCHAGEoRDER_UPD.STATUS_ID == 4)
                    {
                        STORE_PRODUCTS NewProd = db.STORE_PRODUCTS.Find(sTOREpURCHAGEoRDER_UPD.PRODUCT_ID);

                        var sTORE_PRODUCTS_UPD = new STORE_PRODUCTS() { NAME = NewProd.NAME, CATEGORY_ID = NewProd.CATEGORY_ID,
                            SUB_CATEGORY_ID = NewProd.SUB_CATEGORY_ID,

                            //STORE_PRODUCTS sTORE_PRODUCTS_UPD = db.STORE_PRODUCTS.Find(sTOREpURCHAGEoRDER_UPD.PRODUCT_ID);
                            BRAND = null,
                        TOTAL_UNIT = sTOREpURCHAGEoRDER_UPD.ORDER_QUANTITY,
                        TOTAL_COST = Convert.ToInt32(sTOREpURCHAGEoRDER_UPD.SUB_TOTAL),
                        COST_PER_UNIT = Convert.ToInt32(Convert.ToInt32(sTOREpURCHAGEoRDER_UPD.SUB_TOTAL)/ sTOREpURCHAGEoRDER_UPD.ORDER_QUANTITY),
                        SELL_PRICE_PER_UNIT = null,
                        PURCHASED_ON = sTOREpURCHAGEoRDER_UPD.UPDATED_AT,
                        PURCHASED_THROUGH = null,
                        PAID_BY = "School",
                        UNIT_LEFT = NewProd.TOTAL_UNIT + sTOREpURCHAGEoRDER_UPD.ORDER_QUANTITY,
                        IS_ACT = "Y",
                        IS_DEL = "N",
                        CREATED_AT = DateTime.Now,
                        UPDATED_AT = DateTime.Now
                        };
                        db.STORE_PRODUCTS.Add(sTORE_PRODUCTS_UPD);
                        try { db.SaveChanges();}
                        catch (Exception e) { Console.WriteLine(e); ViewBag.POEditMessage = e.InnerException.InnerException.Message; }

                        STORE_PRODUCTS sTORE_PRODUCTS_ORG = db.STORE_PRODUCTS.Find(sTOREpURCHAGEoRDER_UPD.PRODUCT_ID);
                        sTORE_PRODUCTS_ORG.IS_ACT = "N";
                        sTORE_PRODUCTS_ORG.UPDATED_AT = DateTime.Now;
                        db.Entry(sTORE_PRODUCTS_ORG).State = EntityState.Modified;
                        try { db.SaveChanges(); ViewBag.POEditMessage = string.Concat(ViewBag.POEditMessage, "Purchaged Product is added in Product List. Please update further details."); }
                        catch (Exception e) { Console.WriteLine(e); ViewBag.POEditMessage = string.Concat(ViewBag.POEditMessage, e.InnerException.InnerException.Message); }

                    }
                }
                
                DateTime OrdDate = Convert.ToDateTime(System.DateTime.Now);
                if (sTOREpURCHAGEoRDER_UPD.ORDER_DATE != null)
                {
                    OrdDate = Convert.ToDateTime(sTOREpURCHAGEoRDER_UPD.ORDER_DATE);
                }
                ViewBag.ORDER_DATE = OrdDate.ToShortDateString();
                DateTime ShipDate = Convert.ToDateTime(System.DateTime.Now);
                if (sTOREpURCHAGEoRDER_UPD.SHIP_DATE != null)
                {
                    ShipDate = Convert.ToDateTime(sTOREpURCHAGEoRDER_UPD.SHIP_DATE);
                }
                ViewBag.SHIP_DATE = ShipDate.ToShortDateString();

                STORE_PRODUCTS prod = db.STORE_PRODUCTS.Find(sTOREpURCHAGEoRDER_UPD.PRODUCT_ID);
                ViewBag.PRODUCT_ID = prod.NAME;

                List<SelectListItem> options2 = new SelectList(db.STORE_PURCHAGE_VENDOR.Where(x => x.IS_DEL == "N").OrderBy(x => x.ID), "ID", "NAME", sTOREpURCHAGEoRDER_UPD.VENDOR_ID).ToList();
                // add the 'ALL' option
                options2.Insert(0, new SelectListItem() { Value = "-1", Text = "Select Vendor" });
                ViewBag.VENDOR_ID = options2;

                List<SelectListItem> options3 = new SelectList(db.STORE_PURCHAGE_STATUS.Where(x => x.IS_DEL == "N").OrderBy(x => x.ID), "ID", "NAME", sTOREpURCHAGEoRDER_UPD.STATUS_ID).ToList();
                // add the 'ALL' option
                options3.Insert(0, new SelectListItem() { Value = "-1", Text = "Select PO Status" });
                ViewBag.STATUS_ID = options3;

                List<SelectListItem> options4 = new SelectList(db.EMPLOYEEs.OrderBy(x => x.ID), "ID", "EMP_NUM", sTOREpURCHAGEoRDER_UPD.EMPLOYEE_ID).ToList();
                // add the 'ALL' option
                options4.Insert(0, new SelectListItem() { Value = "-1", Text = "Select Employee" });
                ViewBag.EMPLOYEE_ID = options4;

                return View(sTOREpURCHAGEoRDER_UPD);
            }
            return View(sTOREpURCHAGEoRDER);
        }

        // POST: Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PurchageOrderCreate([Bind(Include = "ID,PO_NUMBER,PRODUCT_ID, VENDOR_ID, ORDER_QUANTITY")] STORE_PURCHAGE_ORDER sTOREpURCHAGEoRDER, string CATEGORY_ID, string SUBCATEGORY_ID, string PRODUCT_ID, string VENDOR_ID)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(PRODUCT_ID) && PRODUCT_ID != "-1")
                {
                    sTOREpURCHAGEoRDER.REVISION_NUMBER = 1;
                    sTOREpURCHAGEoRDER.PO_NUMBER = string.Concat("PO-", CATEGORY_ID, "-", SUBCATEGORY_ID, "-", PRODUCT_ID);
                    sTOREpURCHAGEoRDER.STATUS_ID = 1;
                    sTOREpURCHAGEoRDER.EMPLOYEE_ID = Convert.ToInt32(this.Session["UserId"]);
                    if (!string.IsNullOrEmpty(VENDOR_ID) && VENDOR_ID != "-1")
                    {
                        sTOREpURCHAGEoRDER.VENDOR_ID = Convert.ToInt32(sTOREpURCHAGEoRDER.VENDOR_ID);
                    }
                    else { sTOREpURCHAGEoRDER.VENDOR_ID = null; }
                    sTOREpURCHAGEoRDER.ORDER_DATE = System.DateTime.Now;
                    sTOREpURCHAGEoRDER.CREATED_AT = System.DateTime.Now;
                    sTOREpURCHAGEoRDER.UPDATED_AT = System.DateTime.Now;
                    db.STORE_PURCHAGE_ORDER.Add(sTOREpURCHAGEoRDER);
                    try { db.SaveChanges(); ViewBag.POCreateMessage = "Purchage Order created successfully."; }
                    catch (Exception e) { Console.WriteLine(e); ViewBag.POCreateMessage = e.InnerException.InnerException.Message; }
                    return RedirectToAction("PurchageOrder");
                }
                else
                {
                    return RedirectToAction("PurchageOrder", new { CATEGORY_ID = CATEGORY_ID, SUBCATEGORY_ID = SUBCATEGORY_ID, PRODUCT_ID = PRODUCT_ID, VENDOR_ID = VENDOR_ID, ORDER_QUANTITY = sTOREpURCHAGEoRDER.ORDER_QUANTITY });

                }
                
            }

            return View(sTOREpURCHAGEoRDER);
        }

        // GET: Store Products
        public ActionResult ShortageProducts()
        {

            var ProductS = (from pd in db.STORE_PRODUCTS
                            join ct in db.STORE_CATEGORY on pd.CATEGORY_ID equals ct.ID
                            join subcat in db.STORE_SUB_CATEGORY on pd.SUB_CATEGORY_ID equals subcat.ID into gsc
                            from subgsc in gsc.DefaultIfEmpty()
                            orderby pd.NAME, ct.NAME
                            where pd.IS_DEL == "N" && pd.IS_ACT == "Y" && pd.UNIT_LEFT <= 2
                            select new Models.Products { ProductData = pd, CategoryData = ct, SubCategoryData = (subgsc == null ? null : subgsc) }).Distinct();

            return View(ProductS.ToList());
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
