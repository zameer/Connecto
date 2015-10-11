using System;
using Connecto.Common.Enumeration;

namespace Connecto.BusinessObjects
{
    public class SalesDetailCart : ProductBase
    {
        public int SalesDetailId { get; set; }
        public Guid SalesDetailGuid { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int OrderId { get; set; }
        public int ProductDetailId { get; set; }
        public int? CustomerId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Price { get; set; }
        public decimal NetPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public DateTime DateSold { get; set; }
        public DiscountBy DiscountBy { get; set; }
        public double DiscountAs { get; set; }
        public decimal Discount { get; set; }
        public string ReferenceCode { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
