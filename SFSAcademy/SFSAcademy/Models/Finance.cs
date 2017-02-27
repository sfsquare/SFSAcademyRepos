using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SFSAcademy.Models
{
    public class FeeCategory
    {
        public FINANCE_FEE_CATGEORY FinanceFeeCategoryData { get; set; }
        public BATCH BatchData { get; set; }
        public FINANCE_FEE_PARTICULAR FinanceFeeParticularData { get; set; }
        public STUDENT_CATGEORY StudentCategoryData { get; set; }
        public STUDENT StudentData { get; set; }
    }

    public class SelectFeeCategory
    {
        public FINANCE_FEE_CATGEORY FinanceFeeCategoryData { get; set; }
        public BATCH BatchData { get; set; }
        public bool Selected { get; set; }
    }

    public class CreateParticularOption
    {
        public int Id { set; get; }
        public string Name { set; get; }
    }

    public class FeeDiscount
    {
        public FEE_DISCOUNT FeeDiscountData { get; set; }
        public FINANCE_FEE_CATGEORY FinanceFeeCategoryData { get; set; }
        public STUDENT_CATGEORY StudentCategoryData { get; set; }
        public STUDENT StudentData { get; set; }
        public BATCH BatchData { get; set; }
    }

    public class BATCHList
    {
        public int ID { set; get; }
        public string NAME { set; get; }
    }

    public class FeeCollection
    {
        public FINANCE_FEE_COLLECTION FinanceFeeCollectionData { get; set; }
        public FINANCE_FEE_CATGEORY FinanceFeeCategoryData { get; set; }
        public BATCH BatchData { get; set; }
    }

}