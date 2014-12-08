using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connecto.DataObjects.EntityFramework
{
    [Table("Company", Schema = "Connecto")]
    public class EntityCompany : EntityConnecto
    {
        [Key]
        public int CompanyId { get; set; }
        public Guid CompanyGuid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CompanyVatRegNo { get; set; }
        public virtual ICollection<EntityCompanyLocation> CompanyInLocations { get; set; }
    }
}
