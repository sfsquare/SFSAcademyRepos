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
    
    public partial class EVENT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EVENT()
        {
            this.EMPLOYEE_DEPARTMENT_EVENT = new HashSet<EMPLOYEE_DEPARTMENT_EVENT>();
            this.EXAMs = new HashSet<EXAM>();
        }
    
        public int ID { get; set; }
        public string TTIL { get; set; }
        public string DESCR { get; set; }
        public Nullable<System.DateTime> START_DATE { get; set; }
        public Nullable<System.DateTime> END_DATE { get; set; }
        public string IS_COMN { get; set; }
        public string IS_HOL { get; set; }
        public string IS_EXAM { get; set; }
        public Nullable<System.DateTime> CREATED_AT { get; set; }
        public Nullable<System.DateTime> UPDATED_AT { get; set; }
        public string IS_DUE { get; set; }
        public Nullable<int> ORIGIN_ID { get; set; }
        public string ORIGIN_TYPE { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EMPLOYEE_DEPARTMENT_EVENT> EMPLOYEE_DEPARTMENT_EVENT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXAM> EXAMs { get; set; }
    }
}
