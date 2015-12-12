using System;
using Connecto.Common.Enumeration;

namespace Connecto.BusinessObjects
{
    public class ProductDetail : ProductBase
    {
        public int ProductDetailId { get; set; }
        public Guid ProductDetailGuid { get; set; }
        public int OrderId { get; set; }
        public int EmployeeId { get; set; }
        public int ProductId { get; set; }
        public int SupplierId { get; set; }
        public string ProductCode { get; set; }
        public string Barcode { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal Price { get; set; }
        public decimal NetPrice { get; set; }
        public DateTime DateReceived { get; set; }
        public DiscountBy DiscountBy { get; set; }
        public double DiscountAs { get; set; }
        public decimal Discount { get; set; }
        public virtual Product Product { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
