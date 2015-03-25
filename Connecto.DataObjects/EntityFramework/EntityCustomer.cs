using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connecto.DataObjects.EntityFramework
{
    [Table("Customer", Schema = "Product")]
    public class EntityCustomer : EntityConnecto
    {
        [Key]
        public int CustomerId { get; set; }
        public int PersonId { get; set; }
        public virtual EntityPerson Person { get; set; }
    }
}
