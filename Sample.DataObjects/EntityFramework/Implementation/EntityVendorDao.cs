using System.Collections.Generic;
using System.Linq;
using Connecto.BusinessObjects;
using Connecto.DataObjects.EntityFramework.ModelMapper;

namespace Connecto.DataObjects.EntityFramework.Implementation
{
    /// <summary>
    /// Entity Framework implementation of the IVendorDao interface.
    /// </summary>
    public class EntityVendorDao : IVendorDao
    {
        public IList<Vendor> GetVendors()
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var vendors = context.Vendors.ToList();
                return vendors.Select(Mapper.Map).ToList();
            }
        }

        public int DeleteVendor(int id = 0)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = context.Vendors.FirstOrDefault(s => s.VendorId == id);
                context.Vendors.Remove(entity);
                return context.SaveChanges();
            }
        }

        public int AddVendor(Vendor vendor)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = new EntityVendor {Name = vendor.Name};
                context.Vendors.Add(entity);
                context.SaveChanges();
                return entity.VendorId;
            }
        }
    }
}
