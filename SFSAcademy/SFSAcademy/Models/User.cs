using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;

namespace SFSAcademy.Models
{
    public class UserPreviligeDetails
    {
        public UserDetails user { get; set; }
        public List<Privilege> privilage { get; set; }
        public List<USERS_ACCESS> useraccess { get; set; }
    }
    public class UserDetails
    {
        public int Id { get; set; }
        public string USRNAME { get; set; }
        public string FIRST_NAME { get; set; }
        public string LAST_NAME { get; set; }
        public string EML { get; set; }
        public string ADMIN_IND { get; set; }
        public string STDNT_IND { get; set; }
        public string EMP_IND { get; set; }
        public string HASHED_PSWRD { get; set; }
        public string SALT { get; set; }

        public string RST_PSWRD_CODE { get; set; }
        public DateTime RST_PSWRD_CODE_UNTL { get; set; }
        public DateTime CREATED_AT { get; set; }
        public DateTime UPDATED_AT { get; set; }
        public string PARNT_IND { get; set; }

        public List<Privilege> privilage { get; set; }
        public List<USERS_ACCESS> useraccess { get; set; }
    }
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
            UserDetails user = new UserDetails();
            user.Id = v.ID;
            user.USRNAME = v.USRNAME;
            user.FIRST_NAME = v.FIRST_NAME;
            user.LAST_NAME = v.LAST_NAME;
            user.EML = v.EML;
            user.ADMIN_IND = v.ADMIN_IND;
            user.STDNT_IND = v.STDNT_IND;
            user.EMP_IND = v.EMP_IND;
            user.HASHED_PSWRD = v.HASHED_PSWRD;
            user.SALT = v.SALT;
            user.RST_PSWRD_CODE = v.RST_PSWRD_CODE;
            user.RST_PSWRD_CODE_UNTL = Convert.ToDateTime(v.RST_PSWRD_CODE_UNTL);
            user.CREATED_AT = Convert.ToDateTime(v.CREATED_AT);
            user.UPDATED_AT = Convert.ToDateTime(v.UPDATED_AT);
            user.PARNT_IND = v.PARNT_IND;

            user.privilage = GetUserPrivilage(Convert.ToInt32(v.ID));

            if (v != null)
            {
                int value = Convert.ToInt32(v.ID);
                System.Web.HttpContext.Current.Session["UserId"] = value;

                //To Do... Needs tovalidate the user access rights
                //var useraccess = db.USERS_ACCESS.Where(a => a.USRS_ID == v.ID).ToList();
                //
                //foreach (var access in useraccess)
                //{
                //    userrights.Add(new USERS_ACCESS)
                //}

                user.privilage = GetUserPrivilage(Convert.ToInt32(v.ID));
                System.Web.HttpContext.Current.Session["CurrentUser"] = user;
                //userrights.user = v;
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Privilege> GetUserPrivilage(int UserId)
        {
            var userprevilege = (from userprevl in db.Privileges_Users
                                 join prev in db.Privileges on userprevl.PrivilegeId equals prev.ID into userp
                                 from subus in userp.DefaultIfEmpty()
                                 where userprevl.UserId == UserId
                                 orderby userprevl.PrivilegeId
                                 select new Models.Privilege
                                 {
                                     privilegeId = userprevl.PrivilegeId,
                                     Name = subus.Name,
                                 }).ToList();

            return userprevilege;
        }
    }

    public class SelectUserAccess
    {
        public USERS_ACCESS AccessList { get; set; }
        public bool Selected { get; set; }
    }


}