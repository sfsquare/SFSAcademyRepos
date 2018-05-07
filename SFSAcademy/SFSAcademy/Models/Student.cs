using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        public GUARDIAN GuardianData { get; set; }
    }

    public class StudentsBatch
    {
        public IEnumerable<BATCH> BatchList { get; set; }
        public string SearchString { get; set; }
    }

    public class RadioCourseBatch
    {
        public BATCH BatchData { get; set; }
        public COURSE CourseData { get; set; }
        public string BatchId { get; set; }
    }

    public class StudentsGuardians
    {
        public GUARDIAN GuardianData { get; set; }
        public STUDENT StudentData { get; set; }
        public BATCH BatchData { get; set; }
        public COURSE CourseData { get; set; }
        public EMPLOYEE EmployeeData { get; set; }
    }
    public class SelectGuardian
    {
        public GUARDIAN GuardianList { get; set; }
        public bool Selected { get; set; }
    }

    public enum MissingDetl
    {
        DateOfBirth,
        PhoneNumber,
        ParentDetails,
        StundetsPicture
    }

    public enum HadPdFees
    {
        Y,
        N
    }
    public enum BloodGroup
    {
        [Display(Name = "O+")]
        OPositive,
        [Display(Name = "A+")]
        APositive,
        [Display(Name = "B+")]
        BPositive,
        [Display(Name = "AB+")]
        ABPositive,
        [Display(Name = "AB-")]
        ABNegative,
        [Display(Name = "A-")]
        ANegative,
        [Display(Name = "B-")]
        BNegative,
        [Display(Name = "O-")]
        ONegative

    }
}