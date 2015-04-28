using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Connecto.Common.Enumeration;

namespace Connecto.DataObjects.EntityFramework
{
    [Table("SalesDetail", Schema = "Product")]
    public class EntitySalesDetail : EntityProductBase
    {
        [Key]
        public int SalesDetailId { get; set; }
        public Guid SalesDetailGuid { get; set; }
        public string ProductCode { get; set; }
        public int OrderId { get; set; }
        public int ProductDetailId { get; set; }
        public int? CustomerId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public DateTime DateSold { get; set; }
        public DiscountBy DiscountBy { get; set; }
        public double DiscountAs { get; set; }
        public decimal Discount { get; set; }
        public virtual EntityCustomer Customer { get; set; }
    }
}
