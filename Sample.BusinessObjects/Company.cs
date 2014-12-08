using System;
using System.Collections.Generic;

namespace Connecto.BusinessObjects
{
    public class Company : Connecto
    {
        public int CompanyId { get; set; }
        public Guid CompanyGuid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CompanyVatRegNo { get; set; }
        public ICollection<CompanyLocation> CompanyInLocations { get; set; }
    }
}
