using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connecto.DataObjects.EntityFramework
{
    [Table("Supplier", Schema = "Product")]
    public class EntitySupplier : EntityConnecto
    {
        [Key]
        public int SupplierId { get; set; }
        public int PersonId { get; set; }
        public virtual EntityPerson Person { get; set; }
    }
}
