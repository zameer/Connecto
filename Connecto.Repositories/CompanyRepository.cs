using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Connecto.BusinessObjects;
using Connecto.DataObjects;

namespace Connecto.Repositories
{
    public class CompanyRepository
    {
        private static readonly ICompanyDao CompanyDao = DataAccess.CompanyDao;
        public IList<Company> Get()
        {
            return CompanyDao.GetCompanies();
        }
        public Company Get(int id)
        {
            return CompanyDao.GetCompany(id);
        }
        public int Add(Company company)
        {
            return CompanyDao.AddCompany(company);
        }
        public IList<CompanyLocation> Locations(int id)
        {
            return CompanyDao.GetLocations(id);
        }
        public ReportSetting GetReportSetting(Guid reportGuid)
        {
            return CompanyDao.GetReportSetting(reportGuid);
        }
    }
    public class LocationRepository
    {
        private static readonly ICompanyDao LocationDao = DataAccess.CompanyDao;
        public IList<CompanyLocation> Get()
        {
            return LocationDao.GetLocations();
        }
        public CompanyLocation Get(int id)
        {
            return LocationDao.GetLocation(id);
        }
        public int Add(CompanyLocation companyLocation)
        {
            return LocationDao.AddLocation(companyLocation);
        }
    }
}