using System;
using Connecto.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Connecto.Test
{
    [TestClass]
    public class CompanyTest
    {
        private readonly CompanyRepository _company = ConnectoFactory.CompanyRepository;
        private readonly ProductRepository _product = ConnectoFactory.ProductRepository;
        [TestMethod]
        public void CompanyAndLocation()
        {
            var company = _company.Get(2);
            var locations = _company.Locations(company.CompanyId);
            Assert.AreEqual(2, locations.Count);
        }

        [TestMethod]
        public void Product()
        {
            var products = _product.GetAll();
            foreach (var product in products)
            {
                var p = _product.Get(product.ProductId);
            }
            Assert.AreEqual(0, products.Count);
        }
    }
}
