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
    
    public partial class FEE_COLLECTION_DISCOUNT
    {
        public int ID { get; set; }
        public string TYPE { get; set; }
        public string NAME { get; set; }
        public Nullable<int> RCVR_ID { get; set; }
        public Nullable<int> FIN_FEE_CLCT_ID { get; set; }
        public Nullable<decimal> DISC { get; set; }
        public string IS_AMT { get; set; }
        public Nullable<System.DateTime> CREATED_AT { get; set; }
        public Nullable<System.DateTime> UPDATED_AT { get; set; }
    
        public virtual FINANCE_FEE_CATGEORY FINANCE_FEE_CATGEORY { get; set; }
        public virtual STUDENT STUDENT { get; set; }
    }
}
