using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using SFSAcademy.Helpers;
using System.Web.Mvc;

namespace SFSAcademy.Models
{
    //public class NewsMaster
    //{
    //    public NEWS NewsData { get; set; }
    //    public NEWS_COMMENTS NewsCommentData { get; set; }
    //    public USER userData { get; set; }
    //}

    public class NewsComments
    {
        public int commentId { get; set; }
        public int? newsId { get; set; }
        public string commentContent { get; set; }
        public string commentAddedBy { get; set; }
        public int? AddedByUserId { get; set; }
        public DateTime? commentAddedDate { get; set; }
        public DateTime? commentUpdatedDate { get; set; }
        public string isApproved { get; set; }
        public string EnableNewsCommentModeration { get; set; }
        public User Author { get; set; }

        public void reload_news_bar()
        {
            //ActionHelper.expire_fragment(cache_fragment_name());
            News nw = new News();
            DataTable dt = nw.Get_Latest();
        }

        public string cache_fragment_name()
        {
            return "News_latest_fragment";
        }

    }

    public class NewsDetails
    {
        public int newsId { get; set; }
        [Required]
        [Display(Name = "News Title")]
        public string newsTitle { get; set; }
        [Required]
        [Display(Name = "News Content")]
        public string newsContent { get; set; }
        public DateTime? newsCreatedDate { get; set; }
        public DateTime? newsUpdatedDate { get; set; }
        [Display(Name = "Author of this News")]
        public string newsCreatedBy { get; set; }
        public int newsCommentCount { get; set; }
        public List<NewsComments> commentList { get; set; }
        public string isUserAdmin { get; set; }
        public string isModerator { get; set; }
        public int? CreatedByUserId { get; set; }
        public double days { get; set; }
        public string newsComment { get; set; }

        public User Author { get; set; }

        public void reload_news_bar()
        {
            //ActionHelper.expire_fragment(cache_fragment_name());
            News nw = new News();
            DataTable dt = nw.Get_Latest();
        }

        public string cache_fragment_name()
        {
            return "News_latest_fragment";
        }
    }
    public class News
    {
        private SFSAcademyEntities db = new SFSAcademyEntities();
        [Required]
        [Display(Name = "News Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "News Content")]
        public string Content { get; set; }

        [Display(Name = "Author of this News")]
        public bool Author { get; set; }

        /// <summary>
        /// Checks if user with given password exists in the database
        /// </summary>
        /// <param name="_username">User name</param>
        /// <param name="_password">User password</param>
        /// <returns>True if user exist and password is correct</returns>

        [OutputCache(Duration = 10, VaryByParam = "*")]
        public DataTable Get_Latest()
        {
            var LatestNews = new DataTable();

            LatestNews.Columns.Add("Id", typeof(int));
            LatestNews.Columns.Add("NewsTitle", typeof(string));
            LatestNews.Columns.Add("NewsContent", typeof(string));
            LatestNews.Columns.Add("NewsDate", typeof(DateTime));

            var News = (from EV in db.NEWS
                                orderby EV.CREATED_AT descending
                                select new { ID = EV.ID, TITLE = EV.TIL, CONTENT = EV.CNTNT, DATE = EV.CREATED_AT }).Take(3);

            foreach (var entity in News.ToList())
            {
                var row = LatestNews.NewRow();
                row["Id"] = entity.ID;
                row["NewsTitle"] = entity.TITLE;
                row["NewsContent"] = entity.CONTENT;
                row["NewsDate"] = entity.DATE;
                LatestNews.Rows.Add(row);
            }
            return LatestNews;

        }
    }
}