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
    
    public partial class SUBJECT_LEAVE
    {
        public int ID { get; set; }
        public Nullable<int> STDNT_ID { get; set; }
        public Nullable<System.DateTime> MONTH_DATE { get; set; }
        public Nullable<int> SUBJ_ID { get; set; }
        public Nullable<int> EMP_ID { get; set; }
        public Nullable<int> CLS_TMNG_ID { get; set; }
        public string RSN { get; set; }
        public Nullable<System.DateTime> CREATED_AT { get; set; }
        public Nullable<System.DateTime> UPDATED_AT { get; set; }
        public Nullable<int> BTCH_ID { get; set; }
    
        public virtual BATCH BATCH { get; set; }
        public virtual CLASS_TIMING CLASS_TIMING { get; set; }
        public virtual EMPLOYEE EMPLOYEE { get; set; }
        public virtual STUDENT STUDENT { get; set; }
        public virtual SUBJECT SUBJECT { get; set; }
    }
}
