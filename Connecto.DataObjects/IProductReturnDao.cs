using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Connecto.BusinessObjects;

namespace Connecto.DataObjects
{
    /// <summary>
    /// Defines methods to access ProductReturn
    /// </summary>
    /// <remarks>
    /// This is a database independent interface, implementations are database specific
    /// </remarks>
    public interface IProductReturnDao
    {

        ProductReturn GetProductReturnById(int id);
        int DeleteProductReturn(int id, int deletedBy);
        int AddProductReturn(ProductReturn productReturn);
        bool EditProductReturn(ProductReturn productReturn);
    }
}
