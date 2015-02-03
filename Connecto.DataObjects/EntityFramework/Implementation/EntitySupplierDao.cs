using System.Collections.Generic;
using System.Linq;
using Connecto.BusinessObjects;
using Connecto.DataObjects.EntityFramework.ModelMapper;
using System;

namespace Connecto.DataObjects.EntityFramework.Implementation
{
    /// <summary>
    /// Entity Framework implementation of the IVendorDao interface.
    /// </summary>
    public class EntitySupplierDao : ISupplierDao
    {
        public IList<Supplier> GetSuppliers()
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var suppliers = context.Suppliers.ToList();
                return suppliers.Select(Mapper.Map).ToList();
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

        public int DeleteSupplier(int id = 0)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = context.Suppliers.FirstOrDefault(s => s.SupplierId == id);
                context.Suppliers.Remove(entity);
                return context.SaveChanges();
            }
        } 

        public int AddSupplier(Supplier supplier)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = Mapper.Map(supplier);
                context.People.Add(entity.Person);
                entity.PersonId = entity.Person.PersonId;

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
