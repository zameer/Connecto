using System;
using Connecto.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Connecto.Test
{
    [TestClass]
    public class CompanyTest
    {
        private readonly CompanyRepository _company = ConnectoFactory.CompanyRepository;
        [TestMethod]
        public void CompanyAndLocation()
        {
            var company = _company.Get(2);
            var locations = _company.Locations(company.CompanyId);
            Assert.AreEqual(2, locations.Count);
        }
    }
}
