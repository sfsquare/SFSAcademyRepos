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
    
    public partial class STORE_PRODUCTS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public STORE_PRODUCTS()
        {
            this.STORE_PURCHAGE = new HashSet<STORE_PURCHAGE>();
            this.STORE_PURCHAGE_CART = new HashSet<STORE_PURCHAGE_CART>();
        }
    
        public int PRODUCT_ID { get; set; }
        public string NAME { get; set; }
        public Nullable<int> CATEGORY_ID { get; set; }
        public string BRAND { get; set; }
        public Nullable<int> TOTAL_UNIT { get; set; }
        public Nullable<int> TOTAL_COST { get; set; }
        public Nullable<int> COST_PER_UNIT { get; set; }
        public Nullable<int> SELL_PRICE_PER_UNIT { get; set; }
        public Nullable<System.DateTime> PURCHASED_ON { get; set; }
        public string PURCHASED_THROUGH { get; set; }
        public string BAR_CODE { get; set; }
        public string PAID_BY { get; set; }
        public Nullable<int> UNIT_LEFT { get; set; }
        public string IS_ACT { get; set; }
        public string IS_DEL { get; set; }
        public Nullable<System.DateTime> CREATED_AT { get; set; }
        public Nullable<System.DateTime> UPDATED_AT { get; set; }
    
        public virtual STORE_CATEGORY STORE_CATEGORY { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<STORE_PURCHAGE> STORE_PURCHAGE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<STORE_PURCHAGE_CART> STORE_PURCHAGE_CART { get; set; }
    }
}
