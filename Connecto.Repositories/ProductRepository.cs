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

        public IList<Product> GetAll()
        {
            return ProductDao.GetProducts();
        }
        public Product Get(int id)
        {
            return ProductDao.GetProduct(id);
        }
        public int Delete(int id, int deletedBy)
        {
            return ProductDao.DeleteProduct(id, deletedBy);
        }
        public int Add(Product product)
        {
            return ProductDao.AddProduct(product);
        }
        public void Edit(Product product)
        {
            ProductDao.EditProduct(product);
        }
    }
}