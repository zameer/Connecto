using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connecto.DataObjects.EntityFramework
{
    [Table("Product", Schema = "Product")]
    public class EntityProduct : EntityProductBase
    {
        [Key]
        public int ProductId { get; set; }
        public Guid ProductGuid { get; set; }
        public int ProductTypeId { get; set; }
        public int VendorId { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public int StockInHand { get; set; }
        public decimal ContainsQty { get; set; }
        public int Reorderlevel { get; set; }
        public bool SellingDown { get; set; }
        public bool SellingLower { get; set; }
        public bool SellingMargin { get; set; }
        public decimal MarginAmount { get; set; }
        public bool AutoSelling { get; set; }
        public int AutoSellingQty { get; set; }
        public virtual EntityProductType ProductType { get; set; }
        public virtual EntityVendor Vendor { get; set; }
    }
}
