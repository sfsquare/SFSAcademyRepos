using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using System.ComponentModel.DataAnnotations;

namespace SFSAcademy.Models
{
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
        public DataTable Get_Latest()
        {
            var LatestNews = new DataTable();

            LatestNews.Columns.Add("Id", typeof(int));
            LatestNews.Columns.Add("NewsTitle", typeof(string));
            LatestNews.Columns.Add("NewsContent", typeof(string));
            LatestNews.Columns.Add("NewsDate", typeof(DateTime));

            var News = (from EV in db.NEWS
                                orderby EV.CREATED_AT
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