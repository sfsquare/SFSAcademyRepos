using System.Collections.Generic;

namespace SFSAcademy.Models
{
    public class Student
    {
        public STUDENT StudentData { get; set; }
        public BATCH BatcheData { get; set; }
        public COURSE CourseData { get; set; }
        public COUNTRY CountryData { get; set; }
        public STUDENT_CATGEORY CategoryData { get; set; }
        public EMPLOYEE EmployeeData { get; set; }
        public GRADING_LEVEL GradeData { get; set; }
        public IMAGE_DOCUMENTS ImageData { get; set; }
    }

    public class StudentsBatch
    {
        public IEnumerable<BATCH> BatchList { get; set; }
        public string SearchString { get; set; }
    }

    public class SelectGuardian
    {
        public GUARDIAN GuardianList { get; set; }
        public bool Selected { get; set; }
    }
}