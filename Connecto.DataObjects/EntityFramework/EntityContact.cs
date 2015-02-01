using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connecto.DataObjects.EntityFramework
{
    [Table("Contact", Schema = "HumanResource")]
    public class EntityContact : EntityConnecto
    {
        [Key]
        public int ContactId { get; set; }
        public Guid ContactGuid { get; set; }
        public int PersonId { get; set; }
        public string LandNumber { get; set; }
        public string MobileNumber { get; set; }
        public string AddressNo { get; set; }
        public string AddressStreet { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
    }
}
