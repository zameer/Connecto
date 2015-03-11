using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Connecto.BusinessObjects;

namespace Connecto.DataObjects
{
    /// <summary>
    /// Defines methods to access Products
    /// </summary>
    /// <remarks>
    /// This is a database independent interface, implementations are database specific
    /// </remarks>
    public interface IProductDao
    {
        List<Product> GetProducts();
        Product GetProduct(int id);
        int AddProduct(Product product);
        bool EditProduct(Product product);
        int DeleteProduct(int id, int deletedBy);
    }
}
