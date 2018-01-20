using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using SFSAcademy.Models;
using System.Net;
using System.Data.Entity;

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
                        select new Models.NewsDetails
                        {
                            newsId = nw.ID,
                            CreatedByUserId = (nw.AUTH_ID == null) ? 0 : nw.AUTH_ID,
                            newsTitle = nw.TIL,
                            newsContent = nw.CNTNT,
                            newsCreatedBy = (nw.AUTH_ID == 0) ? "User Deleted" : string.Concat(subus.FIRST_NAME, " ", subus.LAST_NAME, " ", (subus.ADMIN_IND == "Y") ? " - Admin" : ""),
                            newsCreatedDate = nw.CREATED_AT,
                            newsUpdatedDate = nw.UPDATED_AT,
                            newsCommentCount = db.NEWS_COMMENTS.Where(X => X.NEWS_ID == nw.ID && X.IS_APPR == "Y").Distinct().Count(),
                            isUserAdmin = subus.ADMIN_IND,
                            commentList = (from com in db.NEWS_COMMENTS
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


        private double GetDayOfAdding(DateTime createddate, DateTime updatedDate)
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

        private NewsDetails GetNewsDetails(int id)
        {
            int UserId = Convert.ToInt32(this.Session["UserId"]);
            NewsDetails newsDetail = (from nw in db.NEWS
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
                                          newsCommentCount = db.NEWS_COMMENTS.Where(X => X.NEWS_ID == nw.ID && X.IS_APPR == "Y").Distinct().Count(),
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

            return newsDetail;
        }

        public ActionResult view(int id)
        {
            var userdetails = this.Session["CurrentUser"] as UserDetails;
            NewsDetails newsDetail = new NewsDetails();
            int UserId = Convert.ToInt32(this.Session["UserId"]);
            ViewBag.current_user = UserId;
            newsDetail = GetNewsDetails(id);
            ViewBag.isModerator = (userdetails.privilage.Select(p => p.Name == "ManageNews").FirstOrDefault()) ? true : false;
            ViewBag.isAdminUser = (userdetails.ADMIN_IND == "Y") ? true : false;
            return View("view", newsDetail);
        }


        // GET: News/ view All
        public ActionResult all()
        {
            int UserId = Convert.ToInt32(this.Session["UserId"]);
            var News = (from nw in db.NEWS
                        join us in db.USERS on nw.AUTH_ID equals us.ID into uj
                        from subus in uj.DefaultIfEmpty()
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
                            newsCommentCount = db.NEWS_COMMENTS.Where(X => X.NEWS_ID == nw.ID && X.IS_APPR == "Y").Distinct().Count(),
                            isUserAdmin = subus.ADMIN_IND,
                            commentList = (from com in db.NEWS_COMMENTS
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
            return View("all", News);
        }

        // GET: News/add
        public ActionResult add()
        {
            NewsDetails news = new NewsDetails();
            return View(news);
        }

        // POST: News/add
        [HttpPost]
        public ActionResult add(NewsDetails news)
        {
            if (ModelState.IsValid)
            {
                int commitCount = 0;
                NEWS n = new NEWS();
                n.TIL = news.newsTitle;
                n.CNTNT = news.newsContent;
                n.AUTH_ID = Convert.ToInt32(this.Session["UserId"]);
                n.CREATED_AT = System.DateTime.Now;
                try
                {
                    db.NEWS.Add(n);
                    commitCount = db.SaveChanges();
                    if (commitCount > 0)
                    {
                        //To Do... Implement Logic to send SMS to all students who has send SMS is enabled
                        //SMS_SETTING s = new SMS_SETTING();

                    }
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = ex;
                }
                return RedirectToAction("view", "News", new { id = n.ID });
            }
            else
            {
                ViewBag.ErrorMessage = "Model is not valid..";
                return RedirectToAction("add");
            }
        }


        public ActionResult delete_comment(int commentId, int newsId)
        {
            NewsDetails newsDetail = new NewsDetails();
            int userId = Convert.ToInt32(this.Session["UserId"]);
            var userdetails = this.Session["CurrentUser"] as UserDetails;
            var newsComment = db.NEWS_COMMENTS.Find(commentId);
            ViewBag.current_user = userId;
            ViewBag.isModerator = (userdetails.privilage.Select(p => p.Name == "ManageNews").FirstOrDefault()) ? true : false;
            ViewBag.isAdminUser = (userdetails.ADMIN_IND == "Y") ? true : false;
            db.NEWS_COMMENTS.Remove(newsComment);
            db.SaveChanges();
            newsDetail = GetNewsDetails(newsId);
            //return RedirectToActionPermanent("view", newsId);
            //return PartialView("_CommentSections", newsDetail);
            //return View("view", newsDetail);
            return RedirectToAction("view", new { id = newsId });
        }
        //[HttpPost]
        public ActionResult add_comment(int newsId, string news_Comment)
        {
            NewsDetails newsDetail = new NewsDetails();
            try
            {
                //Configuration config = new Configuration();
                int userId = Convert.ToInt32(this.Session["UserId"]);
                var userdetails = this.Session["CurrentUser"] as UserDetails;
                var priv = userdetails.privilage.Where(p => p.Name == "ManageNews").FirstOrDefault();
                NEWS_COMMENTS newsComment = new NEWS_COMMENTS();
                //newsComment.CNTNT = comments.commentContent;
                newsComment.CNTNT = news_Comment;
                newsComment.AUTH_ID = Convert.ToInt32(userdetails.Id);
                newsComment.CREATED_AT = System.DateTime.Now;
                newsComment.NEWS_ID = newsId;
                newsComment.IS_APPR = (priv != null) ? "Y" :
                    (userdetails.ADMIN_IND == "Y") ? "Y" : "N";
                //var config = db.CONFIGURATIONs.Find("EnableNewsCommentModeration");
                //var _config = config.get_config_value("EnableNewsCommentModeration");
                db.NEWS_COMMENTS.Add(newsComment);
                db.SaveChanges();
                newsDetail = GetNewsDetails(newsId);
                ViewBag.current_user = userId;
                //ViewBag.isModerator = (newsDetail.isUserAdmin == "Y") ? true : false;
                //ViewBag.isAdminUser = (newsDetail.isUserAdmin == "Y") ? true : false;

                ViewBag.isModerator = (userdetails.privilage.Select(p => p.Name == "ManageNews").FirstOrDefault()) ? true : false;
                ViewBag.isAdminUser = (userdetails.ADMIN_IND == "Y") ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
                //return View("view", newsDetail);
            }
            return PartialView("_CommentSections", newsDetail);
            //return RedirectToAction("view", "News", new { id = newsId });
        }


        public ActionResult delete(int newsId)
        {
            var newsComment = db.NEWS_COMMENTS.Where(a => a.NEWS_ID == newsId).ToList();
            db.NEWS_COMMENTS.RemoveRange(newsComment);
            db.SaveChanges();
            var news = db.NEWS.Find(newsId);
            db.NEWS.Remove(news);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult edit(int newsId)
        {
            var news = db.NEWS.Find(newsId);
            NewsDetails News = new NewsDetails();
            //return RedirectToAction("view", "News", new { id = newsId });

            News.newsId = news.ID;
            News.newsTitle = news.TIL;
            News.newsContent = news.CNTNT;
            News.days = 3;
            News.newsCreatedDate = news.CREATED_AT;
            News.newsUpdatedDate = news.UPDATED_AT;

            return View("edit", News);
        }

        // POST: News/add
        [HttpPost]
        public ActionResult edit(NewsDetails news)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    //db.NEWS.Add(n);
                    int commitCount = 0;
                    NEWS n = new NEWS();
                    n.ID = news.newsId;
                    n.TIL = news.newsTitle;
                    n.CNTNT = news.newsContent;
                    n.AUTH_ID = Convert.ToInt32(this.Session["UserId"]);
                    n.CREATED_AT = news.newsCreatedDate;
                    n.UPDATED_AT = System.DateTime.Now;
                    //commitCount = db.SaveChanges();
                    db.Entry(n).State = EntityState.Modified;
                    db.SaveChanges();

                    if (commitCount > 0)
                    {
                        //To Do... Implement Logic to send SMS to all students who has send SMS is enabled
                        //SMS_SETTING s = new SMS_SETTING();

                    }
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = ex;
                }
                return RedirectToAction("view", "News", new { id = news.newsId });
            }
            else
            {
                ViewBag.ErrorMessage = "Model is not valid..";
                return RedirectToAction("add");
            }
        }

        public ActionResult comment_approved(int commentId, int newsId)
        {
            var newsComment = db.NEWS_COMMENTS.Find(commentId);
            newsComment.IS_APPR = "Y";
            newsComment.UPDATED_AT = System.DateTime.Now;
            db.Entry(newsComment).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("view", "News", new { id = newsId });

        }

    }
}