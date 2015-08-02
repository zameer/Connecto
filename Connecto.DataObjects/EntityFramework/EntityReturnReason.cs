using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Connecto.Common.Enumeration;

namespace Connecto.DataObjects.EntityFramework
{
    [Table("ReturnReason", Schema = "Product")]
    public class EntityReturnReason : EntityConnecto
    {
        [Key]
        public int ReturnReasonId { get; set; }
        public Guid ReturnReasonGuid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
