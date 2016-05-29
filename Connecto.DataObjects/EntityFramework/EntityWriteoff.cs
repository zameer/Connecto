using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connecto.DataObjects.EntityFramework
{
    [Table("Writeoff", Schema = "Product")]
    public class EntityWriteoff : EntityConnecto
    {
        [Key]
        public int WriteoffId { get; set; }
        public Guid WriteoffGuid { get; set; }
        public int EmployeeId { get; set; }
        public DateTime WriteoffDate { get; set; }
        public virtual EntityEmployee Employee { get; set; }
    }
}
