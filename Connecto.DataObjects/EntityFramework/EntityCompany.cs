using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Connecto.Common.Enumeration;

namespace Connecto.DataObjects.EntityFramework
{
    [Table("Company", Schema = "Connecto")]
    public class EntityCompany 
    {
        [Key]
        public int CompanyId { get; set; }
        public Guid CompanyGuid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CompanyVatRegNo { get; set; }
        public RecordStatus Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? EditedBy { get; set; }
        public DateTime? EditedOn { get; set; }
        public virtual ICollection<EntityCompanyLocation> CompanyInLocations { get; set; }
    }
}
