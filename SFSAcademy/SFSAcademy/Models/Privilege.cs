using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SFSAcademy.Models
{
    public class Privilege
    {
        private SFSAcademyEntities db = new SFSAcademyEntities();

        public long privilegeId { get; set; }
        [Required]
        [Display(Name = "Privilage Name")]
        public string Name { get; set; }

        public long UserId { get; set; }
    }
}