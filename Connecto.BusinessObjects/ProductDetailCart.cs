using System;

namespace Connecto.BusinessObjects
{
    public class ProductDetailCart : Connecto
    {
        public int ProductDetailId { get; set; }
        public Guid ProductDetailGuid { get; set; }
        public int ProductId { get; set; }
        public int SupplierId { get; set; }
        public int ProductCode { get; set; }
        public double UnitPrice { get; set; }
        public double SellingPrice { get; set; }
        public int Quantity { get; set; }
        public DateTime DateReceived { get; set; }
        public virtual Product Product { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
