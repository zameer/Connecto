using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connecto.DataObjects.EntityFramework
{
    [Table("SalesDetailCart", Schema = "Product")]
    public class EntitySalesDetailCart : EntityConnecto
    {
        [Key]
        public int SalesDetailId { get; set; }
        public Guid SalesDetailGuid { get; set; }
        public string ProductCode { get; set; }
        public int OrderId { get; set; }
        public int ProductDetailId { get; set; }
        public int CustomerId { get; set; }
        public double UnitPrice { get; set; }
        public double SellingPrice { get; set; }
        public int Quantity { get; set; }
        public int QuantityLower { get; set; }
        public DateTime DateSold { get; set; }
        public virtual EntityCustomer Customer { get; set; }
    }
}
