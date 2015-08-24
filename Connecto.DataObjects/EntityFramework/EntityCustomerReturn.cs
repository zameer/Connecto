using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Connecto.Common.Enumeration;

namespace Connecto.DataObjects.EntityFramework
{
    [Table("CustomerReturn", Schema = "Product")]
    public class EntityCustomerReturn : EntityProductBase
    {
        [Key]
        public int CustomerReturnId { get; set; }
        public Guid CustomerReturnGuid { get; set; }
        public DateTime DateReturned { get; set; }
        public int? SalesDetailId { get; set; }
        public virtual EntitySalesDetail SalesDetail { get; set; }
        public int? ReturnReasonId { get; set; }
        public virtual EntityReturnReason ReturnReason{ get; set; }
    }
}
