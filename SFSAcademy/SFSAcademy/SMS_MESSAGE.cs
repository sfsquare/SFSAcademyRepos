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
    
    public partial class SMS_MESSAGE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SMS_MESSAGE()
        {
            this.SMS_LOG = new HashSet<SMS_LOG>();
        }
    
        public int ID { get; set; }
        public string BODY { get; set; }
        public Nullable<System.DateTime> CREATED_AT { get; set; }
        public Nullable<System.DateTime> UPDATED_AT { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SMS_LOG> SMS_LOG { get; set; }
    }
}
