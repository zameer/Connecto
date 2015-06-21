using System.Collections.Generic;
using System.Linq;
using System;
using Connecto.BusinessObjects;
using Connecto.Common.Enumeration;
using Connecto.DataObjects.EntityFramework.ModelMapper;

namespace Connecto.DataObjects.EntityFramework.Implementation
{
    /// <summary>
    /// Entity Framework implementation of the IEmployeeDao interface.
    /// </summary>
    public class EntityCustomerDao : ICustomerDao
    {
        public Tuple<IList<Customer>, int> GetCustomers(FilterCriteria filter)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                List<Customer> items;
                var count = context.Customers.Count();
                if (!string.IsNullOrEmpty(filter.sSearch))
                {
                    count = context.Customers.Count(e => e.Person.FirstName.ToLower().Contains(filter.sSearch) || e.Person.LastName.ToLower().Contains(filter.sSearch));
                    items = context.Customers.Where(e => e.Person.FirstName.ToLower().Contains(filter.sSearch) || e.Person.LastName.ToLower().Contains(filter.sSearch))
                        .OrderBy(e => e.CustomerId).Skip(filter.iDisplayStart).Take(filter.iDisplayLength).Select(Mapper.Map).ToList();
                }
                else
                {
                    items = context.Customers.OrderBy(e => e.CustomerId).Skip(filter.iDisplayStart).Take(filter.iDisplayLength).Select(Mapper.Map).ToList();
                }
                return new Tuple<IList<Customer>, int>(items, count);
            }
        }
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
