using System.Collections.Generic;
using Connecto.BusinessObjects;

namespace Connecto.DataObjects
{
    public interface ICustomerDao
    {
        IList<Customer> GetCustomers();
        IList<Person> GetPeople();

        Customer GetCustomerById(int id);
        int DeleteCustomer(int id, int deletedBy);
        int AddCustomer(Customer customer);
        bool EditCustomer(Customer customer);
    }
}
