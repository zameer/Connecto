using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Connecto.BusinessObjects;
using Connecto.DataObjects;

namespace Connecto.Repositories
{
    public class ProductReturnRepository
    {
        private static readonly IProductReturnDao ProductReturnDao = DataAccess.ProductReturnDao;

        /// <summary>
        /// Get a specific ProductReturn
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Return ProductReturn ID</returns>
        public ProductReturn GetProductReturnById(int id)
        {
            return ProductReturnDao.GetProductReturnById(id);
        }

        /// <summary>
        /// Removes specific ProductReturn
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>No of ProductReturns Deleted</returns>
        public int Delete(int id, int deletedBy)
        {
            return ProductReturnDao.DeleteProductReturn(id, deletedBy);
        }

        /// <summary>
        /// Create new ProductReturn
        /// </summary>
        /// <param name="ProductReturn">Create ProductReturn object</param>
        public void Add(ProductReturn productReturn)
        {
            ProductReturnDao.AddProductReturn(productReturn);
        }

        /// <summary>
        /// Create new ProductReturn
        /// </summary>
        /// <param name="ProductReturn">Create ProductReturn object</param>
        public void Edit(ProductReturn productReturn)
        {
            ProductReturnDao.EditProductReturn(productReturn);
        }
        
    }
}