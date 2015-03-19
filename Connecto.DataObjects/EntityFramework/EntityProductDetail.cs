using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connecto.DataObjects.EntityFramework
{
    [Table("ProductDetail", Schema = "Product")]
    public class EntityProductDetail : EntityConnecto
    {
        [Key]
        public int ProductDetailId { get; set; }
        public Guid ProductDetailGuid { get; set; }
        public int InvoiceId { get; set; }
        public int ProductId { get; set; }
        public int SupplierId { get; set; }
        public string ProductCode { get; set; }
        public double UnitPrice { get; set; }
        public double SellingPrice { get; set; }
        public int Quantity { get; set; }
        public DateTime DateReceived { get; set; }
        public virtual EntityInvoice Invoice { get; set; }
        public virtual EntityProduct Product { get; set; }
        public virtual EntitySupplier Supplier { get; set; }
    }
}
