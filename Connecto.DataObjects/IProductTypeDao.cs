using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Connecto.BusinessObjects;

namespace Connecto.DataObjects
{
    /// <summary>
    /// Defines methods to access Products Type
    /// </summary>
    /// <remarks>
    /// This is a database independent interface, implementations are database specific
    /// </remarks>
    public interface IProductTypeDao
    {
        List<ProductType> GetProductTypes();
        ProductType GetProductType(int id);
        int AddProductType(ProductType productType);

        /// <summary>
        /// Remove specific Product
        /// </summary>
        /// <param name="id">Unique Product identifier</param>
        /// <returns>No of Product Deleted</returns>
        int DeleteProductType(int id = 0);
        bool EditProductType(ProductType productType);
    }
}
