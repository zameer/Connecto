using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connecto.DataObjects.EntityFramework
{
    [Table("ProductDetailCart", Schema = "Product")]
    public class EntityProductDetailCart : EntityConnecto
    {
        [Key]
        public int ProductDetailId { get; set; }
        public Guid ProductDetailGuid { get; set; }
        public int InvoiceId { get; set; }
        public int ProductId { get; set; }
        public int SupplierId { get; set; }
        public int ProductCode { get; set; }
        public double UnitPrice { get; set; }
        public double SellingPrice { get; set; }
        public int Quantity { get; set; }
        public DateTime DateReceived { get; set; }
        public virtual EntityInvoice Invoice { get; set; }
        public virtual EntityProduct Product { get; set; }
        public virtual EntitySupplier Supplier { get; set; }
    }
}
