using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Connecto.BusinessObjects;
using Connecto.DataObjects;

namespace Connecto.Repositories
{
    public class ProductTypeRepository
    {
        private static readonly IProductTypeDao ProductTypeDao = DataAccess.ProductTypeDao;

        /// <summary>
        /// Get List of ProductType
        /// </summary>
        /// <returns>IList of ProductType</returns>
        public IList<ProductType> GetAll()
        {
            return ProductTypeDao.GetProductTypes();
        }

        /// <summary>
        /// Get List of ProductTypes
        /// </summary>
        /// <returns>IList of ProductType</returns>
        public ProductType GetProductTypeById(int id)
        {
            return ProductTypeDao.GetProductType(id);
        }

        /// <summary>
        /// Removes specific ProductType
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>No of ProductTypes Deleted</returns>
        public int Delete(int id, int deletedBy)
        {
            return ProductTypeDao.DeleteProductType(id, deletedBy);
        }

        /// <summary>
        /// Create new ProductType
        /// </summary>
        /// <param name="product">Create ProductType object</param>
        public int Add(ProductType productType)
        {
            return ProductTypeDao.AddProductType(productType);
        }

        /// <summary>
        /// Create new ProductType
        /// </summary>
        /// <param name="vendor">Create ProductType object</param>
        public void Edit(ProductType productType)
        {
            ProductTypeDao.EditProductType(productType);
        }

        public bool IsExist(ProductType productType)
        {
            return ProductTypeDao.IsExist(productType);
        }

        public bool IsUsed(int id)
        {
            return ProductTypeDao.IsUsed(id);
        }
    }
}