using System.Collections.Generic;
using System.Linq;
using Connecto.BusinessObjects;
using Connecto.DataObjects.EntityFramework.ModelMapper;
using System;
using Connecto.Common.Enumeration;

namespace Connecto.DataObjects.EntityFramework.Implementation
{
    /// <summary>
    /// Entity Framework implementation of the IVendorDao interface.
    /// </summary>
    public class EntityVendorDao : IVendorDao
    {
        public Tuple<IList<Vendor>, int> GetVendorsSearch(FilterCriteria filter)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                List<Vendor> items;
                var count = context.Vendors.Count();
                if (!string.IsNullOrEmpty(filter.sSearch))
                {
                    count = context.Vendors.Count(e => e.Name.ToLower().Contains(filter.sSearch) );
                    items = context.Vendors.Where(e => e.Name.ToLower().Contains(filter.sSearch) )
                        .OrderBy(e => e.VendorId).Skip(filter.iDisplayStart).Take(filter.iDisplayLength).Select(Mapper.Map).ToList();
                }
                else
                {
                    items = context.Vendors.OrderBy(e => e.VendorId).Skip(filter.iDisplayStart).Take(filter.iDisplayLength).Select(Mapper.Map).ToList();
                }
                return new Tuple<IList<Vendor>, int>(items, count);
            }
        }
        public IList<Vendor> GetVendors()
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var vendors = context.Vendors.ToList();
                return vendors.Select(Mapper.Map).ToList();
            }
        }

        // get Vendor by id
        public Vendor GetVendorById(int vendorId)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = context.Vendors.FirstOrDefault(e => e.VendorId == vendorId);
                return entity == null ? null : Mapper.Map(entity);
            }
        }

        public int DeleteVendor(int id, int deletedBy)
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
                var entity = Mapper.Map(vendor);
                context.Vendors.Add(entity);
                context.SaveChanges();
                return entity.VendorId;
            }
        }

        public bool EditVendor(Vendor vendor)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = context.Vendors.FirstOrDefault(s => s.VendorId == vendor.VendorId);
                entity.Name = vendor.Name;
                entity.EditedBy = vendor.EditedBy;
                entity.EditedOn = DateTime.Now;
                return context.SaveChanges() > 0;
            }
        }

        public bool IsExist(Vendor vendor)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                if (vendor.VendorId > 0)
                    return context.Vendors.Any(e => e.VendorId != vendor.VendorId && e.Name.ToLower() == vendor.Name.ToLower());
                return context.Vendors.Any(e => e.Name.ToLower() == vendor.Name.ToLower());
            }
        }

        public bool IsUsed(int id)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                return context.Products.Any(s => s.VendorId == id && s.Status == RecordStatus.Active);
            }
        }
    }
}
