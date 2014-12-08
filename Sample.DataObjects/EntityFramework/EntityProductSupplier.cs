using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connecto.DataObjects.EntityFramework
{
    [Table("ProductSupplier", Schema = "Product")]
    public class EntityProductSupplier : EntityConnecto
    {
        [Key]
        public int ProductSupplierId { get; set; }
        public Guid ProductSupplierGuid { get; set; }
        public int ProductId { get; set; }
        public int SupplierId { get; set; }
        public virtual EntityProduct Product { get; set; }
        public virtual EntitySupplier Supplier { get; set; }
    }
}
