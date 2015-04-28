using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connecto.DataObjects.EntityFramework
{
    [Table("SalesDetailCart", Schema = "Product")]
    public class EntitySalesDetailCart : EntityProductBase
    {
        [Key]
        public int SalesDetailId { get; set; }
        public Guid SalesDetailGuid { get; set; }
        public string ProductCode { get; set; }
        public int OrderId { get; set; }
        public int ProductDetailId { get; set; }
        public int? CustomerId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public DateTime DateSold { get; set; }
        public virtual EntityCustomer Customer { get; set; }
    }
}
