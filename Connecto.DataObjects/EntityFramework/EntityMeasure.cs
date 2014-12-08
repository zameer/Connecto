using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connecto.DataObjects.EntityFramework
{
    [Table("Measure", Schema = "Product")]
    public class EntityMeasure : EntityConnecto
    {
        [Key]
        public int MeasureId { get; set; }
        public Guid MeasureGuid { get; set; }
        public string Lower { get; set; }
        public int Volume { get; set; }
        public string Actual { get; set; }
    }
}
