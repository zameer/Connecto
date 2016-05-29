using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Connecto.Common.Enumeration;

namespace Connecto.DataObjects.EntityFramework
{
    [Table("WriteoffDetail", Schema = "Product")]
    public class EntityWriteoffDetail : EntityProductBase
    {
        [Key]
        public int WriteoffDetailId { get; set; }
        public Guid WriteoffDetailGuid { get; set; }
        public string ProductCode { get; set; }
        public int ProductDetailId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Price { get; set; }
        public decimal NetPrice { get; set; }
        public DateTime DateWriteoff { get; set; }
        public int EmployeeId { get; set; }
        public int WriteoffId { get; set; }
        public virtual EntityProductDetail ProductDetail { get; set; }
    }
}
