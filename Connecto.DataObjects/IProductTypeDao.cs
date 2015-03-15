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
        int DeleteProductType(int id, int deletedBy);
        bool EditProductType(ProductType productType);
        bool IsExist(ProductType productType);
        bool IsUsed(int id);
    }
}
