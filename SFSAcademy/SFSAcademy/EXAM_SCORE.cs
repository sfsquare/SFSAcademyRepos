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
    
    public partial class EXAM_SCORE
    {
        public int ID { get; set; }
        public Nullable<int> STDNT_ID { get; set; }
        public Nullable<int> EXAM_ID { get; set; }
        public Nullable<int> MKS { get; set; }
        public Nullable<int> GRADING_LVL_ID { get; set; }
        public string RMK { get; set; }
        public string IS_FAIL { get; set; }
        public Nullable<System.DateTime> CREATED_AT { get; set; }
        public Nullable<System.DateTime> UPDATED_AT { get; set; }
    
        public virtual EXAM EXAM { get; set; }
        public virtual STUDENT STUDENT { get; set; }
        public virtual GRADING_LEVEL GRADING_LEVEL { get; set; }
    }
}
