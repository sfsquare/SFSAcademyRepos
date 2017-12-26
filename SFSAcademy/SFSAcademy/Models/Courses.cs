using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SFSAcademy.Models
{
    public class CoursesBatch
    {
        public COURSE CourseData { get; set; }
        public BATCH BatchData { get; set; }
    }

    public class SelectCourseBatch
    {
        public COURSE CourseData { get; set; }
        public BATCH BatchData { get; set; }
        public FINANCE_FEE_CATGEORY FeeCategoryData { get; set; }
        public bool Selected { get; set; }
    }

}