using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connecto.DataObjects.EntityFramework
{
    [Table("Employee", Schema = "HumanResource")]
    public class EntityEmployee : EntityConnecto
    {
        [Key]
        public int EmployeeId { get; set; }
        public Guid EmployeeGuid { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public ICollection<EntityContact> Contacts { get; set; }
    }
}
