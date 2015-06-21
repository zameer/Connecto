using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Connecto.BusinessObjects;

namespace Connecto.DataObjects
{
    public interface ICustomerDao
    {
        Tuple<IList<Customer>, int> GetCustomers(FilterCriteria filter);
        IList<Customer> GetCustomers();
        IList<Person> GetPeople();
        Customer GetCustomerById(int id);
        int DeleteCustomer(int id, int deletedBy);
        int AddCustomer(Customer customer);
        bool EditCustomer(Customer customer);
    }
}
