﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connecto.DataObjects.EntityFramework
{
    [Table("CompanyLocation", Schema = "Connecto")]
    public class EntityCompanyLocation : EntityConnecto
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
        public string Timezone { get; set; }
        public int WorkingHrs { get; set; }
        public int? CompanyId { get; set; }
        public int? CurrencyTypeId { get; set; }
        public string CompanyLogo { get; set; }
        public virtual EntityCompany Company { get; set; }
    }
}
