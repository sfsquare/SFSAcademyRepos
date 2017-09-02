using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SFSAcademy.Models
{
    public class Products
    {
        public STORE_PRODUCTS ProductData { get; set; }
        public STORE_CATEGORY CategoryData { get; set; }
    }
    public class Purchase
    {
        public STORE_PURCHAGE PurchaseData { get; set; }
        public STORE_PRODUCTS ProductData { get; set; }
        public STORE_CATEGORY CategoryData { get; set; }
    }
}