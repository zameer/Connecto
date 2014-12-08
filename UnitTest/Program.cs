using System;
using System.Collections.Generic;
using Connecto.BusinessObjects;
using Connecto.Repositories;

namespace UnitTest
{
    class Program
    {
        private static readonly CompanyRepository Company = ConnectoFactory.CompanyRepository;
        private static readonly LocationRepository Location = ConnectoFactory.LocationRepository;
        // Create static data access objects
        static void Main(string[] args)
        {
            TestCompany();
        }

        static void TestCompany()
        {
            ////Case 1 - Create Company
            //var companyId = Create();
            //CreateDescendants(companyId);
            var company = Company.Get(2);
            var locations = Company.Locations(company.CompanyId);
        }

        static int Create()
        {
            return Company.Add(new Company
            {
                Name = "ConnecTo",
                Description = "Meet developement needs",
                CompanyVatRegNo = "2233R",
                Status = 1,
                CreatedBy = 2,
                CreatedOn = DateTime.Now
            });
        }
        static void CreateDescendants(int id)
        {
            var descendants = new List<CompanyLocation>
            {
                new CompanyLocation
                {
                    CompanyId = id,
                    StratDate = DateTime.Now,
                    Name = "oLiya",
                    AddressNo = "ting 345/A",
                    AddressStreet = "Gular directive",
                    City = "Lift",
                    Province = "Western",
                    CountryId = 1,
                    WorkingHrs = 8,
                    Status = 1,
                    CreatedBy = 2,
                    CreatedOn = DateTime.Now,
                    Timezone = "Sri Jaya 5:30"
                },
                new CompanyLocation
                {
                    CompanyId = id,
                    StratDate = DateTime.Now,
                    Name = "dReams",
                    AddressNo = "rims B009",
                    AddressStreet = "Service resource",
                    City = "Singha",
                    Province = "Northern",
                    CountryId = 1,
                    WorkingHrs = 8,
                    Status = 1,
                    CreatedBy = 2,
                    CreatedOn = DateTime.Now,
                    Timezone = "Sri Jaya 5:30"
                }
            };
            foreach (var companyLocation in descendants)
            {
                Location.Add(companyLocation);
            }
        }
    }
}
