using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Connecto.BusinessObjects;

namespace Connecto.DataObjects.EntityFramework.ModelMapper
{
    public class Mapper
    {
        internal static Vendor Map(EntityVendor entity)
        {
            return new Vendor
            {
                VendorId = entity.VendorId,
                Name = entity.Name
            };
        }
        internal static Product Map(EntityProduct entity)
        {
            return new Product
            {
                ProductId = entity.ProductId,
                Name = entity.Name
            };
        }
        internal static EntityProduct Map(Product entity)
        {
            return new EntityProduct
            {
                ProductId = entity.ProductId,
                Name = entity.Name
            };
        }

        internal static CompanyLocation Map(EntityCompanyLocation entity)
        {
            return new CompanyLocation
            {
                CompanyLocationId = entity.CompanyLocationId,
                CompanyLocationGuid = entity.CompanyLocationGuid,
                Name = entity.Name,
                StratDate = entity.StratDate,
                AddressNo = entity.AddressNo,
                AddressStreet = entity.AddressStreet,
                City = entity.City,
                Province = entity.Province,
                CountryId = entity.CountryId,
                Timezone = entity.Timezone,
                WorkingHrs = entity.WorkingHrs,
                CompanyId = entity.CompanyId,
                CurrencyTypeId = entity.CurrencyTypeId,
                CompanyLogo = entity.CompanyLogo,
                CreatedOn = entity.CreatedOn,
                CreatedBy = entity.CreatedBy
            };
        }
        internal static EntityCompanyLocation Map(CompanyLocation entity)
        {
            return new EntityCompanyLocation
            {
                CompanyLocationId = entity.CompanyLocationId,
                CompanyLocationGuid = entity.CompanyLocationGuid,
                Name = entity.Name,
                StratDate = entity.StratDate,
                AddressNo = entity.AddressNo,
                AddressStreet = entity.AddressStreet,
                City = entity.City,
                Province = entity.Province,
                CountryId = entity.CountryId,
                Timezone = entity.Timezone,
                WorkingHrs = entity.WorkingHrs,
                CompanyId = entity.CompanyId,
                CurrencyTypeId = entity.CurrencyTypeId,
                CompanyLogo = entity.CompanyLogo,
                CreatedOn = entity.CreatedOn,
                CreatedBy = entity.CreatedBy
            };
        }
        internal static Company Map(EntityCompany entity)
        {
            return new Company
            {
                CompanyId = entity.CompanyId,
                CompanyGuid = entity.CompanyGuid,
                Name = entity.Name,
                Description = entity.Description,
                CompanyVatRegNo = entity.CompanyVatRegNo,
                CreatedOn = entity.CreatedOn,
                CreatedBy = entity.CreatedBy
            };
        }
        internal static EntityCompany Map(Company entity)
        {
            return new EntityCompany
            {
                CompanyId = entity.CompanyId,
                CompanyGuid = entity.CompanyGuid,
                Name = entity.Name,
                Description = entity.Description,
                CompanyVatRegNo = entity.CompanyVatRegNo,
                CreatedOn = entity.CreatedOn,
                CreatedBy = entity.CreatedBy
            };
        }
    }
}
