using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;

namespace SFSAcademy.Models
{
    public class User
    {
        private SFSAcademyEntities db = new SFSAcademyEntities();
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember on this computer")]
        public bool RememberMe { get; set; }


        /// <summary>
        /// Checks if user with given password exists in the database
        /// </summary>
        /// <param name="_username">User name</param>
        /// <param name="_password">User password</param>
        /// <returns>True if user exist and password is correct</returns>
        public bool IsValid(string _username, string _password)
        {

            if (_username == null)
            {
                return false;
            }
            else if (_password == null)
            {
                return false;
            }

            var v = db.USERS.Where(a => a.USRNAME.Equals(_username) && a.HASHED_PSWRD.Equals(_password)).FirstOrDefault();

            if (v != null)
            {
                int value = Convert.ToInt32(v.ID);
                System.Web.HttpContext.Current.Session["UserId"] = value;
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class SelectUserAccess
    {
        public USERS_ACCESS AccessList { get; set; }
        public bool Selected { get; set; }
    }
}