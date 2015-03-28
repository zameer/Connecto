using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connecto.DataObjects.EntityFramework
{
    [Table("Product", Schema = "Product")]
    public class EntityProduct : EntityConnecto
    {
        [Key]
        public int ProductId { get; set; }
        public Guid ProductGuid { get; set; }
        public int ProductTypeId { get; set; }
        public int VendorId { get; set; }
        public string Name { get; set; }
        public int StockInHand { get; set; }
        public double ContainsQty { get; set; }
        public int Quantity { get; set; }
        public int QuantityLower { get; set; }
        public int Reorderlevel { get; set; }
        public virtual EntityProductType ProductType { get; set; }
        public virtual EntityVendor Vendor { get; set; }
    }
}
