using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connecto.DataObjects.EntityFramework
{
    [Table("Person", Schema = "HumanResource")]
    public class EntityPerson : EntityConnecto
    {
        [Key]
        public int PersonId { get; set; }
        public Guid PersonGuid { get; set; }
        public string Description { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<EntityContact> Contacts { get; set; }
    }
}
