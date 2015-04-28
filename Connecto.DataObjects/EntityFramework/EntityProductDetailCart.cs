using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Connecto.Common.Enumeration;

namespace Connecto.DataObjects.EntityFramework
{
    [Table("ProductDetailCart", Schema = "Product")]
    public class EntityProductDetailCart : EntityProductBase
    {
        [Key]
        public int ProductDetailId { get; set; }
        public Guid ProductDetailGuid { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int SupplierId { get; set; }
        public string ProductCode { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public DateTime DateReceived { get; set; }
        public DiscountBy DiscountBy { get; set; }
        public double DiscountAs { get; set; }
        public decimal Discount { get; set; }
        public virtual EntityOrder Order { get; set; }
        public virtual EntityProduct Product { get; set; }
        public virtual EntitySupplier Supplier { get; set; }
    }
}
