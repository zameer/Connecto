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
        IList<Person> GetPeople();
        Supplier GetSupplierById(int id);
        int DeleteSupplier(int id, int deletedBy);
        int AddSupplier(Supplier supplier);
        bool EditSupplier(Supplier supplier);
    }
}
