using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using PagedList;
using SFSAcademy.Models;
using System.Collections.Generic;

namespace SFSAcademy.Controllers
{
    public class USERsController : Controller
    {
        private SFSAcademyEntities db = new SFSAcademyEntities();

        // GET: USERs
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

            var User = from s in db.USERS
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                User = User.Where(s => s.LAST_NAME.Contains(searchString)
                                       || s.FIRST_NAME.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    User = User.OrderByDescending(s => s.LAST_NAME);
                    break;
                case "Date":
                    User = User.OrderBy(s => s.CREATED_AT);
                    break;
                case "date_desc":
                    User = User.OrderByDescending(s => s.CREATED_AT);
                    break;
                default:  // Name ascending 
                    User = User.OrderBy(s => s.LAST_NAME);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(User.ToPagedList(pageNumber, pageSize));
            //return View(db.USERS.ToList());
        }

        // GET: USERs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER uSER = db.USERS.Find(id);
            if (uSER == null)
            {
                return HttpNotFound();
            }
            return View(uSER);
        }

        // GET: USERs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: USERs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,USRNAME,FIRST_NAME,LAST_NAME,EML,ADMIN_IND,STDNT_IND,EMP_IND,HASHED_PSWRD,SALT,RST_PSWRD_CODE,RST_PSWRD_CODE_UNTL,CREATED_AT,UPDATED_AT,PARNT_IND")] USER uSER)
        {
            if (ModelState.IsValid)
            {
                db.USERS.Add(uSER);
                db.SaveChanges();
                if (uSER.ADMIN_IND.Equals("Y"))
                {
                    foreach (var entity in db.USERS_ACCESS.Select(s => new { s.USRS_ID, s.LIST_ITEM, s.LVL_1_MENU, s.LVL_2_MENU, s.CTL, s.ACTN, s.IS_ACCBLE }).Distinct().Where(a => a.USRS_ID.Equals(1)).ToList())
                    {
                        var UserAccess = new USERS_ACCESS() { USRS_ID = uSER.ID, LIST_ITEM = entity.LIST_ITEM, LVL_1_MENU = entity.LVL_1_MENU, LVL_2_MENU = entity.LVL_2_MENU, CTL = entity.CTL, ACTN = entity.ACTN, IS_ACCBLE = entity.IS_ACCBLE };
                        db.USERS_ACCESS.Add(UserAccess);
                        db.SaveChanges();
                    }
                }
                else if(uSER.EMP_IND.Equals("Y"))
                {
                    foreach (var entity in db.USERS_ACCESS.Select(s => new { s.USRS_ID, s.LIST_ITEM, s.LVL_1_MENU, s.LVL_2_MENU, s.CTL, s.ACTN, s.IS_ACCBLE }).Distinct().Where(a => a.USRS_ID.Equals(2)).ToList())
                    {
                        var UserAccess = new USERS_ACCESS() { USRS_ID = uSER.ID, LIST_ITEM = entity.LIST_ITEM, LVL_1_MENU = entity.LVL_1_MENU, LVL_2_MENU = entity.LVL_2_MENU, CTL = entity.CTL, ACTN = entity.ACTN, IS_ACCBLE = entity.IS_ACCBLE };
                        db.USERS_ACCESS.Add(UserAccess);
                        db.SaveChanges();
                    }
                }
                else if (uSER.STDNT_IND.Equals("Y"))
                {
                    foreach (var entity in db.USERS_ACCESS.Select(s => new { s.USRS_ID, s.LIST_ITEM, s.LVL_1_MENU, s.LVL_2_MENU, s.CTL, s.ACTN, s.IS_ACCBLE }).Distinct().Where(a => a.USRS_ID.Equals(4)).ToList())
                    {
                        var UserAccess = new USERS_ACCESS() { USRS_ID = uSER.ID, LIST_ITEM = entity.LIST_ITEM, LVL_1_MENU = entity.LVL_1_MENU, LVL_2_MENU = entity.LVL_2_MENU, CTL = entity.CTL, ACTN = entity.ACTN, IS_ACCBLE = entity.IS_ACCBLE };
                        db.USERS_ACCESS.Add(UserAccess);
                        db.SaveChanges();
                    }
                }
                else if (uSER.PARNT_IND.Equals("Y"))
                {
                    foreach (var entity in db.USERS_ACCESS.Select(s => new { s.USRS_ID, s.LIST_ITEM, s.LVL_1_MENU, s.LVL_2_MENU, s.CTL, s.ACTN, s.IS_ACCBLE }).Distinct().Where(a => a.USRS_ID.Equals(3)).ToList())
                    {
                        var UserAccess = new USERS_ACCESS() { USRS_ID = uSER.ID, LIST_ITEM = entity.LIST_ITEM, LVL_1_MENU = entity.LVL_1_MENU, LVL_2_MENU = entity.LVL_2_MENU, CTL = entity.CTL, ACTN = entity.ACTN, IS_ACCBLE = entity.IS_ACCBLE };
                        db.USERS_ACCESS.Add(UserAccess);
                        db.SaveChanges();
                    }
                }
                else
                {
                    foreach (var entity in db.USERS_ACCESS.Select(s => new { s.USRS_ID, s.LIST_ITEM, s.LVL_1_MENU, s.LVL_2_MENU, s.CTL, s.ACTN, s.IS_ACCBLE }).Distinct().Where(a => a.USRS_ID.Equals(3)).ToList())
                    {
                        var UserAccess = new USERS_ACCESS() { USRS_ID = uSER.ID, LIST_ITEM = entity.LIST_ITEM, LVL_1_MENU = entity.LVL_1_MENU, LVL_2_MENU = entity.LVL_2_MENU, CTL = entity.CTL, ACTN = entity.ACTN, IS_ACCBLE = entity.IS_ACCBLE };
                        db.USERS_ACCESS.Add(UserAccess);
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("Edit_Privilege", "USERs", new { id = uSER.ID });

            }

            return View(uSER);
        }

        // GET: USERs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER uSER = db.USERS.Find(id);
            if (uSER == null)
            {
                return HttpNotFound();
            }
            return View(uSER);
        }

        // POST: USERs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,USRNAME,FIRST_NAME,LAST_NAME,EML,ADMIN_IND,STDNT_IND,EMP_IND,HASHED_PSWRD,SALT,RST_PSWRD_CODE,RST_PSWRD_CODE_UNTL,CREATED_AT,UPDATED_AT,PARNT_IND")] USER uSER)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uSER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(uSER);
        }

        // GET: USERs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER uSER = db.USERS.Find(id);
            if (uSER == null)
            {
                return HttpNotFound();
            }
            return View(uSER);
        }

        // POST: USERs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            foreach (var entity in db.USERS_ACCESS.Select(s => new { s.USRS_ID, s.ID }).Distinct().Where(a => a.USRS_ID.Equals(id)).ToList())
            {
                USERS_ACCESS uSERaCCCESS = db.USERS_ACCESS.Find(entity.ID);
                db.USERS_ACCESS.Remove(uSERaCCCESS);
                db.SaveChanges();
            }
            USER uSER = db.USERS.Find(id);
            db.USERS.Remove(uSER);
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


        public ActionResult Dashboard(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER uSER = db.USERS.Find(id);
            if (uSER == null)
            {
                return HttpNotFound();
            }
            return View(uSER);

        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();

        }

        [HttpPost]
        public ActionResult Login(Models.User user)
        {
            if (ModelState.IsValid)
            {
                if (user.IsValid(user.UserName, user.Password))
                {
                    FormsAuthentication.SetAuthCookie(user.UserName, user.RememberMe);
                    int UserId = Convert.ToInt32(this.Session["UserId"]);
                    return RedirectToAction("Dashboard", "USERs", new { id = UserId });
                }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect!");
                }
            }
            return View(user);
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Edit_Privilege(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var uSERaACCESS = (from C in db.USERS_ACCESS
                               where C.USRS_ID == id
                               select new Models.SelectUserAccess { AccessList = C, Selected= (C.IS_ACCBLE == "Y" ? true : false) }).OrderBy(g => g.AccessList.ID).ToList();


            var uSERaCCESfINAL = (from u in uSERaACCESS
                                      //where conditions or joins with other tables to be included here
                                  group u by new { u.AccessList.LIST_ITEM, u.AccessList.LVL_1_MENU } into grp
                                  let MaxOrderDatePerPerson = grp.Max(g => g.AccessList.ID)
                                  from p in grp
                                  where p.AccessList.ID == MaxOrderDatePerPerson
                                  select p).ToList();
            return View(uSERaCCESfINAL);

        }


        // POST: USERs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_Privilege(IList<SelectUserAccess> model)
        {
            if (ModelState.IsValid)
            {
                foreach (SelectUserAccess item in model)
                {
                    if (item.Selected)
                    {
                        var uACCESSVal = (from C in db.USERS_ACCESS
                                           where (C.LVL_1_MENU.Equals(item.AccessList.LVL_1_MENU)  && C.USRS_ID == item.AccessList.USRS_ID)
                                          select new { id = C.ID, usr_id = C.USRS_ID, usr_role = C.USR_ROLE, l_item=C.LIST_ITEM, l_1_menu=C.LVL_1_MENU, l_2_menu=C.LVL_2_MENU, ctl= C.CTL, act=C.ACTN, is_accble=C.IS_ACCBLE }).FirstOrDefault();
                        USERS_ACCESS uSERaCCCESS = db.USERS_ACCESS.Find(uACCESSVal.id);
                        uSERaCCCESS.IS_ACCBLE = "Y";
                        db.SaveChanges();
                    }
                    else
                    {
                        var uACCESSVal = (from C in db.USERS_ACCESS
                                          where (C.LVL_1_MENU.Equals(item.AccessList.LVL_1_MENU) && C.USRS_ID == item.AccessList.USRS_ID)
                                          select new { id = C.ID, usr_id = C.USRS_ID, usr_role = C.USR_ROLE, l_item = C.LIST_ITEM, l_1_menu = C.LVL_1_MENU, l_2_menu = C.LVL_2_MENU, ctl = C.CTL, act = C.ACTN, is_accble = C.IS_ACCBLE }).FirstOrDefault();
                        USERS_ACCESS uSERaCCCESS = db.USERS_ACCESS.Find(uACCESSVal.id);
                        uSERaCCCESS.IS_ACCBLE = "N";
                        db.SaveChanges();
                    }
                }
            }
            return RedirectToAction("Index");
        }
    }
}
