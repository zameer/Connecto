using System;

namespace Connecto.BusinessObjects
{
    public class ProductDetailCart : ProductBase
    {
        public int ProductDetailId { get; set; }
        public Guid ProductDetailGuid { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int SupplierId { get; set; }
        public string ProductCode { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public DateTime DateReceived { get; set; }
        public virtual Product Product { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
