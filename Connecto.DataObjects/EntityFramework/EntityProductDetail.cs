using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connecto.DataObjects.EntityFramework
{
    [Table("ProductDetail", Schema = "Product")]
    public class EntityProductDetail : EntityConnecto
    {
        [Key]
        public int ProductDetailId { get; set; }
        public Guid ProductDetailGuid { get; set; }
        public int ProductId { get; set; }
        public double UnitPrice { get; set; }
        public double SellingPrice { get; set; }
        public int Quantity { get; set; }
        public DateTime DateReceived { get; set; }
        public virtual EntityProduct Product { get; set; }
    }
}
