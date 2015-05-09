using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Connecto.Common.Enumeration;

namespace Connecto.DataObjects.EntityFramework
{
    [Table("ProductDetail", Schema = "Product")]
    public class EntityProductDetail : EntityProductBase
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
        public decimal Price { get; set; }
        public decimal NetPrice { get; set; }
        public DateTime DateReceived { get; set; }
        public DiscountBy DiscountBy { get; set; }
        public double DiscountAs { get; set; }
        public decimal Discount { get; set; }
        public virtual EntityOrder Order { get; set; }
        public virtual EntityProduct Product { get; set; }
        public virtual EntitySupplier Supplier { get; set; }
    }
}
