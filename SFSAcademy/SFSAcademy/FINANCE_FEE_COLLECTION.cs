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
    
    public partial class FINANCE_FEE_COLLECTION
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FINANCE_FEE_COLLECTION()
        {
            this.FINANCE_FEE = new HashSet<FINANCE_FEE>();
            this.FINANCE_FEE_STRUCTURE_ELEMENT = new HashSet<FINANCE_FEE_STRUCTURE_ELEMENT>();
            this.FEE_COLLECTION_PARTICULAR = new HashSet<FEE_COLLECTION_PARTICULAR>();
        }
    
        public int ID { get; set; }
        public string NAME { get; set; }
        public Nullable<System.DateTime> START_DATE { get; set; }
        public Nullable<System.DateTime> END_DATE { get; set; }
        public Nullable<int> FEE_CAT_ID { get; set; }
        public Nullable<int> BTCH_ID { get; set; }
        public string IS_DEL { get; set; }
        public Nullable<System.DateTime> DUE_DATE { get; set; }
    
        public virtual BATCH BATCH { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FINANCE_FEE> FINANCE_FEE { get; set; }
        public virtual FINANCE_FEE_CATGEORY FINANCE_FEE_CATGEORY { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FINANCE_FEE_STRUCTURE_ELEMENT> FINANCE_FEE_STRUCTURE_ELEMENT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FEE_COLLECTION_PARTICULAR> FEE_COLLECTION_PARTICULAR { get; set; }
    }
}
