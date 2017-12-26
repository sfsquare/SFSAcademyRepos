using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using SFSAcademy.Models;

namespace SFSAcademy.Controllers
{
    public class newsController : Controller
    {
        private SFSAcademyEntities db = new SFSAcademyEntities();

        // GET: News
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            int UserId = Convert.ToInt32(this.Session["UserId"]);
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

            var News = (from nw in db.NEWS
                        join us in db.USERS on nw.AUTH_ID equals us.ID into uj
                        from subus in uj.DefaultIfEmpty()
                        orderby nw.UPDATED_AT
                        select new Models.NewsDetails {
                            newsId = nw.ID,
                            CreatedByUserId = (nw.AUTH_ID == null) ? 0 : nw.AUTH_ID,
                            newsTitle = nw.TIL,
                            newsContent = nw.CNTNT,
                            newsCreatedBy = (nw.AUTH_ID == 0) ? "User Deleted" :  string.Concat(subus.FIRST_NAME, " ", subus.LAST_NAME, " ", (subus.ADMIN_IND == "Y") ? " - Admin" : ""),
                            newsCreatedDate = nw.CREATED_AT,
                            newsUpdatedDate = nw.UPDATED_AT,
                            newsCommentCount = db.NEWS_COMMENTS.Where(X=>X.NEWS_ID == nw.ID).Distinct().Count(),
                            isUserAdmin = subus.ADMIN_IND,
                            commentList = (from com in  db.NEWS_COMMENTS
                                          join us in db.USERS on com.AUTH_ID equals us.ID into gj
                                          from comus in gj.DefaultIfEmpty()
                                          where com.NEWS_ID == nw.ID
                                          orderby com.ID
                                          select new Models.NewsComments
                                          {
                                              commentId = com.ID,
                                              newsId = com.NEWS_ID,
                                              commentContent = com.CNTNT,
                                              commentAddedBy = string.Concat(comus.FIRST_NAME, " ", comus.LAST_NAME),
                                              commentAddedDate = com.CREATED_AT,
                                              commentUpdatedDate = com.UPDATED_AT,
                                              isApproved = com.IS_APPR
                                          }).ToList()
                        }).ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                News = News.Where(s => s.newsTitle.Contains(searchString)).ToList();
            }

            switch (sortOrder)
            {
                case "title_desc":
                    News = News.OrderByDescending(s => s.newsTitle).ToList();
                    break;
                case "Author":
                    News = News.OrderBy(s => s.newsCreatedBy).ToList();
                    break;
                case "date_desc":
                    News = News.OrderByDescending(s => s.newsCreatedDate).ToList();
                    break;
                default:  // Name ascending 
                    News = News.OrderBy(s => s.newsTitle).ToList();
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(News.ToPagedList(pageNumber, pageSize));
        }


        private double GetDayOfAdding(DateTime  createddate,DateTime updatedDate)
        {
            double days = 0;
            DateTime actualDate = new DateTime();
            if (updatedDate != null)
                actualDate = updatedDate;
            else
                actualDate = createddate;
            days = (actualDate - System.DateTime.Now).TotalDays;
            DateTime d1 = DateTime.Now;
            DateTime d2 = DateTime.Now.AddDays(-1);

            TimeSpan t = d1 - d2;
            double NrOfDays = t.TotalDays;
            return 0;
        }


        public ActionResult view(int id)
        {
            NewsDetails newsDetail = new NewsDetails();
            int UserId = Convert.ToInt32(this.Session["UserId"]);
            ViewBag.current_user = UserId;

            newsDetail = (from nw in db.NEWS
                        join us in db.USERS on nw.AUTH_ID equals us.ID into uj
                        from subus in uj.DefaultIfEmpty()
                        where nw.ID == id
                        orderby nw.UPDATED_AT
                        select new Models.NewsDetails
                        {
                            newsId = nw.ID,
                            CreatedByUserId = (nw.AUTH_ID == null) ? 0 : nw.AUTH_ID,
                            newsTitle = nw.TIL,
                            newsContent = nw.CNTNT,
                            newsCreatedBy = (nw.AUTH_ID == 0) ? "User Deleted" : string.Concat(subus.FIRST_NAME, " ", subus.LAST_NAME, " ", (subus.ADMIN_IND == "Y") ? " - Admin" : ""),
                            newsCreatedDate = nw.CREATED_AT,
                            newsUpdatedDate = nw.UPDATED_AT,
                            newsCommentCount = db.NEWS_COMMENTS.Where(X => X.NEWS_ID == nw.ID).Distinct().Count(),
                            isUserAdmin = subus.ADMIN_IND,
                            isModerator = subus.ADMIN_IND,
                        }).FirstOrDefault();

            newsDetail.commentList = (from com in db.NEWS_COMMENTS
                                      join us in db.USERS on com.AUTH_ID equals us.ID into gj
                                      from comus in gj.DefaultIfEmpty()
                                      where com.NEWS_ID == id
                                      orderby com.ID
                                      select new Models.NewsComments
                                      {
                                          commentId = com.ID,
                                          newsId = com.NEWS_ID,
                                          commentContent = com.CNTNT,
                                          commentAddedBy = (com.AUTH_ID == 0) ? "User Deleted" : string.Concat(comus.FIRST_NAME, " ", comus.LAST_NAME),
                                          AddedByUserId = com.AUTH_ID,
                                          //commentAddedBy = string.Concat(comus.FIRST_NAME, " ", comus.LAST_NAME),
                                          commentAddedDate = com.CREATED_AT,
                                          commentUpdatedDate = com.UPDATED_AT,
                                          isApproved = com.IS_APPR,
                                          EnableNewsCommentModeration = (from config in db.CONFIGURATIONs
                                                                         where config.CONFIG_KEY == "EnableNewsCommentModeration"
                                                                         select config).FirstOrDefault().CONFIG_VAL
                                      }).ToList();
            ViewBag.isModerator = (newsDetail.isUserAdmin == "Y") ? true : false;
            ViewBag.isAdminUser = (newsDetail.isUserAdmin == "Y") ? true : false;

            return View("view",newsDetail);
        }

        // GET: USERs/Create
        public ActionResult Create()
        {
            return View();
        }
    }
}