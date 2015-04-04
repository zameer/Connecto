using System.Collections.Generic;
using System.Linq;
using Connecto.BusinessObjects;
using Connecto.Common.Enumeration;
using Connecto.DataObjects.EntityFramework.ModelMapper;
using System;

namespace Connecto.DataObjects.EntityFramework.Implementation
{
    /// <summary>
    /// Entity Framework implementation of the IEmployeeDao interface.
    /// </summary>
    public class EntityCustomerDao : ICustomerDao
    {
        public IList<Customer> GetCustomers()
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var customers = context.Customers.Where(e => e.Status == RecordStatus.Active).ToList();
                return customers.Select(Mapper.Map).ToList();
            }
        }
        public IList<Person> GetPeople()
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var personIds = context.Customers.Where(e => e.Status == RecordStatus.Active).Select(e => e.PersonId).ToArray();
                var people = context.People.Where(e => !personIds.Contains(e.PersonId)).ToList();
                return people.Select(Mapper.Map).ToList();
            }
        }

        public Customer GetCustomerById(int personId)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = context.Customers.FirstOrDefault(e => e.PersonId == personId);
                return entity == null ? null : Mapper.Map(entity);
            }
        }

        public int DeleteCustomer(int id, int deletedBy)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = context.Customers.FirstOrDefault(s => s.CustomerId == id);
                entity.Status = RecordStatus.Deleted;
                entity.EditedOn = DateTime.Now;
                entity.EditedBy = deletedBy;
                return context.SaveChanges();
            }
        }

        public int AddCustomer(Customer customer)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = Mapper.Map(customer);
                context.Customers.Add(entity);
                context.SaveChanges();
                return entity.CustomerId;
            }
        }

        public bool EditCustomer(Customer customer)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = context.People.FirstOrDefault(s => s.PersonId == customer.Person.PersonId);
                entity.FirstName = customer.Person.FirstName;
                entity.LastName = customer.Person.LastName;
                entity.Description = customer.Person.Description;
                entity.EditedBy = customer.Person.EditedBy;
                entity.EditedOn = DateTime.Now;
                return context.SaveChanges() > 0;
            }
        }
    }
}
