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
    
    public partial class FA_GROUP
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FA_GROUP()
        {
            this.FA_CRITERIAS = new HashSet<FA_CRITERIAS>();
        }
    
        public int ID { get; set; }
        public string NAME { get; set; }
        public string DESCR { get; set; }
        public Nullable<int> CCE_EXAM_CAT_ID { get; set; }
        public Nullable<System.DateTime> CREATED_AT { get; set; }
        public Nullable<System.DateTime> UPDATED_AT { get; set; }
        public Nullable<int> CCE_GRADE_SET_ID { get; set; }
        public Nullable<double> MAX_MKS { get; set; }
        public string IS_DEL { get; set; }
    
        public virtual CCE_EXAM_CATEGORY CCE_EXAM_CATEGORY { get; set; }
        public virtual CCE_GRADE_SET CCE_GRADE_SET { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FA_CRITERIAS> FA_CRITERIAS { get; set; }
    }
}
