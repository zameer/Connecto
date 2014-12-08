using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connecto.DataObjects.EntityFramework
{
    [Table("ProductType", Schema = "Product")]
    public class EntityProductType : EntityConnecto
    {
        [Key]
        public int ProductTypeId { get; set; }
        public Guid ProductTypeGuid { get; set; }
        public int MeasureId { get; set; }
        public string Type { get; set; }
        public string StockAs { get; set; }
        public virtual EntityMeasure Measure { get; set; }
    }
}
