using System;
using System.Collections.Generic;
using System.Linq;
using Connecto.BusinessObjects;
using Connecto.DataObjects.EntityFramework.ModelMapper;

namespace Connecto.DataObjects.EntityFramework.Implementation
{
    public class EntityCompanyDao : ICompanyDao
    {
        public IList<Company> GetCompanies()
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                return context.Companies.Select(Mapper.Map).ToList();
            }
        }
        public Company GetCompany(int companyId)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = context.Companies.FirstOrDefault(e => e.CompanyId == companyId);
                return entity == null ? null : Mapper.Map(entity);
            }
        }
        public int AddCompany(Company company)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                company.CompanyGuid = Guid.NewGuid();
                var entity = Mapper.Map(company);
                context.Companies.Add(entity);
                context.SaveChanges();
                return entity.CompanyId;
            }
        }

        public IList<CompanyLocation> GetLocations(int companyId)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                return context.CompanyLocations.Where(e=> e.CompanyId == companyId).Select(Mapper.Map).ToList();
            }
        }

        public IList<CompanyLocation> GetLocations()
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                return context.CompanyLocations.Select(Mapper.Map).ToList();
            }
        }
        public CompanyLocation GetLocation(int locationId)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = context.CompanyLocations.FirstOrDefault(e => e.CompanyLocationId == locationId);
                return entity == null ? null : Mapper.Map(entity);
            }
        }
        public int AddLocation(CompanyLocation companyLocation)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                companyLocation.CompanyLocationGuid = Guid.NewGuid();
                var entity = Mapper.Map(companyLocation);
                context.CompanyLocations.Add(entity);
                context.SaveChanges();
                return entity.CompanyLocationId;
            }
        }

        public ReportSetting GetReportSetting(Guid reportGuid)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var item = context.ReportSettings.FirstOrDefault(e => e.ReportGuid == reportGuid);
                if (item == null) return null;
                return new ReportSetting
                {
                    ReportGuid = item.ReportGuid,
                    ReportName = item.ReportName,
                    ReportTitle = item.ReportTitle,
                    ReportPath = item.ReportPath,
                    CommandText = item.CommandText,
                    Parameters = item.Parameters
                };
            }
        }
    }
}
