//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SFSAcademy
{
    using System;
    using System.Collections.Generic;
    
    public partial class BATCH
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BATCH()
        {
            this.ASSESSMENT_SCORE = new HashSet<ASSESSMENT_SCORE>();
            this.ATTENDENCEs = new HashSet<ATTENDENCE>();
            this.FINANCE_FEE_CATGEORY = new HashSet<FINANCE_FEE_CATGEORY>();
            this.FINANCE_FEE_COLLECTION = new HashSet<FINANCE_FEE_COLLECTION>();
            this.FINANCE_FEE_STRUCTURE_ELEMENT = new HashSet<FINANCE_FEE_STRUCTURE_ELEMENT>();
            this.SUBJECTs = new HashSet<SUBJECT>();
            this.STUDENT_SUBJECT = new HashSet<STUDENT_SUBJECT>();
            this.CLASS_TIMING = new HashSet<CLASS_TIMING>();
            this.SUBJECT_LEAVE = new HashSet<SUBJECT_LEAVE>();
            this.EXAM_GROUP = new HashSet<EXAM_GROUP>();
            this.GRADING_LEVEL = new HashSet<GRADING_LEVEL>();
            this.CCE_REPORTS = new HashSet<CCE_REPORTS>();
            this.TIMETABLE_ENTRY = new HashSet<TIMETABLE_ENTRY>();
            this.WEEKDAYs = new HashSet<WEEKDAY>();
        }
    
        public int ID { get; set; }
        public string NAME { get; set; }
        public Nullable<int> CRS_ID { get; set; }
        public Nullable<System.DateTime> START_DATE { get; set; }
        public Nullable<System.DateTime> END_DATE { get; set; }
        public string IS_DEL { get; set; }
        public Nullable<int> EMP_ID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ASSESSMENT_SCORE> ASSESSMENT_SCORE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ATTENDENCE> ATTENDENCEs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FINANCE_FEE_CATGEORY> FINANCE_FEE_CATGEORY { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FINANCE_FEE_COLLECTION> FINANCE_FEE_COLLECTION { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FINANCE_FEE_STRUCTURE_ELEMENT> FINANCE_FEE_STRUCTURE_ELEMENT { get; set; }
        public virtual COURSE COURSE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SUBJECT> SUBJECTs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<STUDENT_SUBJECT> STUDENT_SUBJECT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CLASS_TIMING> CLASS_TIMING { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SUBJECT_LEAVE> SUBJECT_LEAVE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXAM_GROUP> EXAM_GROUP { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GRADING_LEVEL> GRADING_LEVEL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CCE_REPORTS> CCE_REPORTS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TIMETABLE_ENTRY> TIMETABLE_ENTRY { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WEEKDAY> WEEKDAYs { get; set; }
    }
}
