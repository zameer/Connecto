using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Connecto.BusinessObjects;
using Connecto.DataObjects;

namespace Connecto.Repositories
{
    public class VendorRepository
    {
        private static readonly IVendorDao VendorDao = DataAccess.VendorDao;

        /// <summary>
        /// Get List of Vendors
        /// </summary>
        /// <returns>IList of Vendors</returns>
        public IList<Vendor> GetAll()
        {
            return VendorDao.GetVendors();
        }

        /// <summary>
        /// Removes specific vendor
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>No of vendors Deleted</returns>
        public int Delete(int id = 0)
        {
            return VendorDao.DeleteVendor(id);
        }

        /// <summary>
        /// Create new vendor
        /// </summary>
        /// <param name="vendor">Create vendor object</param>
        public void Add(Vendor vendor)
        {
            VendorDao.AddVendor(vendor);
        }
    }
}