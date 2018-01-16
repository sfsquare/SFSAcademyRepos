using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SFSAcademy.Models
{
    public class CommonModel
    {
        public Dictionary<string, string> STUDENT_ATTENDANCE_TYPE_OPTIONS { get; set; }
        public Dictionary<string, string> NETWORK_STATES { get; set; }
        public string[] LOCALES { get; set; }
    }
}