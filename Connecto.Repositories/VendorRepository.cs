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
        /// Get a specific Vendor
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Return Vendor ID</returns>
        public Vendor GetVendorById(int id)
        {
            return VendorDao.GetVendorById(id);
        }

        /// <summary>
        /// Removes specific vendor
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>No of vendors Deleted</returns>
        public int Delete(int id, int deletedBy)
        {
            return VendorDao.DeleteVendor(id, deletedBy);
        }

        /// <summary>
        /// Create new vendor
        /// </summary>
        /// <param name="vendor">Create vendor object</param>
        public void Add(Vendor vendor)
        {
            VendorDao.AddVendor(vendor);
        }

        /// <summary>
        /// Create new Vendor
        /// </summary>
        /// <param name="vendor">Create Vendor object</param>
        public void Edit(Vendor vendor)
        {
            VendorDao.EditVendor(vendor);
        }

        public bool IsExist(Vendor vendor)
        {
            return VendorDao.IsExist(vendor);
        }

        public bool IsUsed(int id)
        {
            return VendorDao.IsUsed(id);
        }
    }
}