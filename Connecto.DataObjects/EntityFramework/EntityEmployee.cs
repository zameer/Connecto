using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connecto.DataObjects.EntityFramework
{
    [Table("Employee", Schema = "HumanResource")]
    public class EntityEmployee
    {
        [Key]
        public int EmployeeId { get; set; }
        public int PersonId { get; set; }
        public virtual EntityPerson Person { get; set; }
    }
}
