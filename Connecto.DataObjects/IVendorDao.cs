using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Connecto.BusinessObjects;

namespace Connecto.DataObjects
{
    /// <summary>
    /// Defines methods to access Vendor
    /// </summary>
    /// <remarks>
    /// This is a database independent interface, implementations are database specific
    /// </remarks>
    public interface IVendorDao
    {
        /// <summary>
        /// Gets List of Vendors
        /// </summary>
        /// <returns>List of Vendors</returns>
        IList<Vendor> GetVendors();

        /// <summary>
        /// Get specific vendor
        /// </summary>
        /// <param name="id">Unique vendor identifier</param>
        /// <returns>Specific Vendor Details</returns>
        Vendor GetVendorById(int id);

        /// <summary>
        /// Remove specific vendor
        /// </summary>
        /// <param name="id">Unique vendor identifier</param>
        /// <returns>No of vendors Deleted</returns>
        int DeleteVendor(int id = 0);

        

        /// <summary>
        /// Add specific vendor
        /// </summary>
        /// <param name="id">Unique vendor identifier</param>
        /// <returns>Vendor ID</returns>
        int AddVendor(Vendor vendor);
    }
}
