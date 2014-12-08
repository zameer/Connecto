using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Connecto.BusinessObjects;

namespace Connecto.DataObjects
{
    public interface ISupplierDao
    {
        IList<Supplier> GetSuppliers();
        int DeleteSupplier(int id = 0);
        int AddSupplier(Supplier vendor);
    }
}
