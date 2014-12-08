using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connecto.DataObjects.EntityFramework
{
    [Table("Vendor", Schema = "Product")]
    public class EntityVendor : EntityConnecto
    {
        [Key]
        public int VendorId { get; set; }
        public Guid VendorGuid { get; set; }
        public string Name { get; set; }
    }
}
