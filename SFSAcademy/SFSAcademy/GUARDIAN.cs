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
    
    public partial class GUARDIAN
    {
        public int ID { get; set; }
        public Nullable<int> WARD_ID { get; set; }
        public string FIRST_NAME { get; set; }
        public string LAST_NAME { get; set; }
        public string REL { get; set; }
        public string EML { get; set; }
        public string OFF_PH1 { get; set; }
        public string OFF_PH2 { get; set; }
        public string MOBL_PH { get; set; }
        public string OFF_ADDR_LINE1 { get; set; }
        public string OFF_ADDR_LINE2 { get; set; }
        public string CITY { get; set; }
        public string STATE { get; set; }
        public Nullable<int> CTRY_ID { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public string OCCP { get; set; }
        public string INCM { get; set; }
        public string ED { get; set; }
        public Nullable<System.DateTime> CREATED_AT { get; set; }
        public Nullable<System.DateTime> UPDATED_AT { get; set; }
        public Nullable<int> USRID { get; set; }
    
        public virtual USER USER { get; set; }
        public virtual STUDENT STUDENT { get; set; }
    }
}
