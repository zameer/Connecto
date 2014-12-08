using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Connecto.BusinessObjects;
using Connecto.DataObjects;

namespace Connecto.Repositories
{
    public class ProductRepository
    {
        private static readonly IProductDao ProductDao = DataAccess.ProductDao;

        /// <summary>
        /// Get List of Vendors
        /// </summary>
        /// <returns>IList of Vendors</returns>
        public IList<Product> GetAll()
        {
            return ProductDao.GetProducts();
        }

        ///// <summary>
        ///// Removes specific vendor
        ///// </summary>
        ///// <param name="id">Identifier</param>
        ///// <returns>No of vendors Deleted</returns>
        //public int Delete(int id = 0)
        //{
        //    return ProductDao.De(id);
        //}

        /// <summary>
        /// Create new vendor
        /// </summary>
        /// <param name="product">Create vendor object</param>
        public void Add(Product product)
        {
            ProductDao.AddProduct(product);
        }
    }
}