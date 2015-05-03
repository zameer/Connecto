using System;
using Connecto.BusinessObjects;
using Connecto.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Connecto.Test
{
    [TestClass]
    public class CompanyTest
    {
        private readonly CompanyRepository _company = ConnectoFactory.CompanyRepository;
        private readonly ProductRepository _product = ConnectoFactory.ProductRepository;
        private readonly SalesDetailRepository _sales = ConnectoFactory.SalesDetailRepository;
        [TestMethod]
        public void CompanyAndLocation()
        {
            var company = _company.Get(2);
            var locations = _company.Locations(company.CompanyId);
            Assert.AreEqual(2, locations.Count);
        }

        [TestMethod]
        public void Sales()
        {
            try
            {
                var stock = new ProductBase { Quantity = 199, QuantityActual = 24, QuantityLower = 550 };
                var sold = new ProductBase { Quantity = 2, QuantityActual = 30, QuantityLower = 600 };
                var syncedStock = new ProductBase { Quantity = 105, QuantityActual = 21, QuantityLower = 50};

                var sales = _sales.SyncSales(1000, 50, stock, sold);

                Assert.AreEqual(syncedStock.Quantity, sales.Quantity);
                Assert.AreEqual(syncedStock.QuantityActual, sales.QuantityActual);
                Assert.AreEqual(syncedStock.QuantityLower, sales.QuantityLower);
            }
            catch (Exception)
            {
                
                throw;
            }
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
