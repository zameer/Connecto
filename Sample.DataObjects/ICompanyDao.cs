using System.Collections.Generic;
using Connecto.BusinessObjects;

namespace Connecto.DataObjects
{
    public interface ICompanyDao
    {
        Company GetCompany(int companyId);
        IList<Company> GetCompanies();
        int AddCompany(Company company);
        IList<CompanyLocation> GetLocations(int companyId);

        CompanyLocation GetLocation(int locationId);
        IList<CompanyLocation> GetLocations();
        int AddLocation(CompanyLocation companyLocation);
    }
}
