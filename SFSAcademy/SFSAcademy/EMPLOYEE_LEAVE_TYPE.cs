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
    
    public partial class EMPLOYEE_LEAVE_TYPE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EMPLOYEE_LEAVE_TYPE()
        {
            this.APPLY_LEAVE = new HashSet<APPLY_LEAVE>();
            this.EMPLOYEE_ATTENDENCES = new HashSet<EMPLOYEE_ATTENDENCES>();
            this.EMPLOYEE_LEAVE = new HashSet<EMPLOYEE_LEAVE>();
        }
    
        public int ID { get; set; }
        public string NAME { get; set; }
        public string CODE { get; set; }
        public string STAT { get; set; }
        public string MAX_LEAVE_CNT { get; set; }
        public string CARR_FRWD { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<APPLY_LEAVE> APPLY_LEAVE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EMPLOYEE_ATTENDENCES> EMPLOYEE_ATTENDENCES { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EMPLOYEE_LEAVE> EMPLOYEE_LEAVE { get; set; }
    }
}
