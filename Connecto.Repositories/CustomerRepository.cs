using System.Collections.Generic;
using Connecto.BusinessObjects;
using Connecto.DataObjects;

namespace Connecto.Repositories
{
    public class CustomerRepository
    {
        private static readonly ICustomerDao Repo = DataAccess.CustomerDao;

        public IList<Customer> GetAll()
        {
            return Repo.GetCustomers();
        }

        public IList<Person> GetPeople()
        {
            return Repo.GetPeople();
        }

        public Customer GetCustomerById(int id)
        {
            return Repo.GetCustomerById(id);
        }

        public int Delete(int id, int deletedBy)
        {
            return Repo.DeleteCustomer(id, deletedBy);
        }

        public void Add(Customer customer)
        {
            Repo.AddCustomer(customer);
        }

        public void Edit(Customer customer)
        {
            Repo.EditCustomer(customer);
        }
    }
}