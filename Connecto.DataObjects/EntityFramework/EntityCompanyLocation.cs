using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Connecto.Common.Enumeration;

namespace Connecto.DataObjects.EntityFramework
{
    [Table("CompanyLocation", Schema = "Connecto")]
    public class EntityCompanyLocation 
    {
        [Key]
        public int CompanyLocationId { get; set; }
        public Guid CompanyLocationGuid { get; set; }
        public string Name { get; set; }
        public DateTime StratDate { get; set; }
        public string AddressNo { get; set; }
        public string AddressStreet { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public int? CountryId { get; set; }
        public string Contact { get; set; }
        public string Timezone { get; set; }
        public int WorkingHrs { get; set; }
        public int? CompanyId { get; set; }
        public int? CurrencyTypeId { get; set; }
        public string CompanyLogo { get; set; }
        public RecordStatus Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? EditedBy { get; set; }
        public DateTime? EditedOn { get; set; }
        public virtual EntityCompany Company { get; set; }
    }
}
