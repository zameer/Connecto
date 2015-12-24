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
        public Tuple<IList<Product>, int> GetAllSearch(FilterCriteria filter)
        {
            return ProductDao.GetProductsSearch(filter);
        }
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
        public bool IsExist(Product product)
        {
            return ProductDao.IsExist(product);
        }

        public bool IsUsed(int id)
        {
            return ProductDao.IsUsed(id);
        }
    }
}