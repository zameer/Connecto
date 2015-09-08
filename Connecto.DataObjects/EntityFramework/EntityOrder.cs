using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Connecto.Common.Enumeration;

namespace Connecto.DataObjects.EntityFramework
{
    [Table("Order", Schema = "Product")]
    public class EntityOrder : EntityConnecto
    {
        [Key]
        public int OrderId { get; set; }
        public Guid OrderGuid { get; set; }
        public OrderType OrderType { get; set; }
        public decimal Fluctuation { get; set; }
    }
}
