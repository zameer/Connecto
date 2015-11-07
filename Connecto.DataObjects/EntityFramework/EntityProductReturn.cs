using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Connecto.Common.Enumeration;

namespace Connecto.DataObjects.EntityFramework
{
    [Table("ProductReturn", Schema = "Product")]
    public class EntityProductReturn : EntityProductBase
    {
        [Key]
        public int ProductReturnId { get; set; }
        public Guid ProductReturnGuid { get; set; }
        public DateTime DateReturned { get; set; }
        public int? ProductDetailId { get; set; }
        public int EmployeeId { get; set; }
        public virtual EntityProductDetail ProductDetail { get; set; }
        public int? SalesDetailId { get; set; }
        public virtual EntitySalesDetail SalesDetail { get; set; }
        public int? ReturnReasonId { get; set; }
        public virtual EntityReturnReason ReturnReason { get; set; }
        public virtual EntityEmployee Employee { get; set; }

    }
}
