using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Connecto.Common.Enumeration;

namespace Connecto.DataObjects.EntityFramework
{
    [Table("SalesDetailCart", Schema = "Product")]
    public class EntitySalesDetailCart : EntityProductBase
    {
        [Key]
        public int SalesDetailId { get; set; }
        public Guid SalesDetailGuid { get; set; }
        public string ProductCode { get; set; }
        public int OrderId { get; set; }
        public int ProductDetailId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal Price { get; set; }
        public decimal NetPrice { get; set; }
        public DateTime DateSold { get; set; }
        public DiscountBy DiscountBy { get; set; }
        public double DiscountAs { get; set; }
        public decimal Discount { get; set; }
        public virtual EntityProductDetail ProductDetail { get; set; }
    }
}
