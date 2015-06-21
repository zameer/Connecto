using System.Collections.Generic;
using System.Linq;
using Connecto.BusinessObjects;
using Connecto.Common.Enumeration;
using Connecto.DataObjects.EntityFramework.ModelMapper;
using System;

namespace Connecto.DataObjects.EntityFramework.Implementation
{
    /// <summary>
    /// Entity Framework implementation of the IVendorDao interface.
    /// </summary>
    public class EntitySupplierDao : ISupplierDao
    {
        public Tuple<IList<Supplier>, int> GetSuppliers(FilterCriteria filter)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                List<Supplier> items;
                var count = context.Suppliers.Count();
                if (!string.IsNullOrEmpty(filter.sSearch))
                {
                    count = context.Suppliers.Count(e => e.Person.FirstName.ToLower().Contains(filter.sSearch) || e.Person.LastName.ToLower().Contains(filter.sSearch));
                    items = context.Suppliers.Where(e => e.Person.FirstName.ToLower().Contains(filter.sSearch) || e.Person.LastName.ToLower().Contains(filter.sSearch))
                        .OrderBy(e => e.PersonId).Skip(filter.iDisplayStart).Take(filter.iDisplayLength).Select(Mapper.Map).ToList();
                }
                else
                {
                    items = context.Suppliers.OrderBy(e => e.PersonId).Skip(filter.iDisplayStart).Take(filter.iDisplayLength).Select(Mapper.Map).ToList();
                }
                return new Tuple<IList<Supplier>, int>(items, count);
            }
        }
        public IList<Supplier> GetSuppliers()
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var suppliers = context.Suppliers.Where(e => e.Status == RecordStatus.Active).ToList();
                return suppliers.Select(Mapper.Map).ToList();
            }
        }
        public IList<Person> GetPeople()
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var personIds = context.Suppliers.Where(e => e.Status == RecordStatus.Active).Select(e => e.PersonId).ToArray();
                var people = context.People.Where(e=> !personIds.Contains(e.PersonId)).ToList();
                return people.Select(Mapper.Map).ToList();
            }
        }
        // get Supplier by id
        public Supplier GetSupplierById(int personId)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = context.Suppliers.FirstOrDefault(e => e.PersonId == personId);
                return entity == null ? null : Mapper.Map(entity);
            }
        }

        public int DeleteSupplier(int id, int deletedBy)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = context.Suppliers.FirstOrDefault(s => s.SupplierId == id);
                entity.Status = RecordStatus.Deleted;
                entity.EditedOn = DateTime.Now;
                entity.EditedBy = deletedBy;
                return context.SaveChanges();
            }
        } 

        public int AddSupplier(Supplier supplier)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = Mapper.Map(supplier);
                context.Suppliers.Add(entity);
                context.SaveChanges();
                return entity.SupplierId;
            }
        }
        public bool EditSupplier(Supplier supplier)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = context.People.FirstOrDefault(s => s.PersonId == supplier.Person.PersonId);
                entity.FirstName = supplier.Person.FirstName;
                entity.LastName = supplier.Person.LastName;
                entity.Description = supplier.Person.Description;
                entity.EditedBy = supplier.Person.EditedBy;
                entity.EditedOn = DateTime.Now;
                return context.SaveChanges() > 0;
            }
        }
    }
}
