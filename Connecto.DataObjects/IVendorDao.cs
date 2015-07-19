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
        Tuple<IList<Vendor>, int> GetVendorsSearch(FilterCriteria filter);
        IList<Vendor> GetVendors();
        Vendor GetVendorById(int id);
        int DeleteVendor(int id, int deletedBy);
        int AddVendor(Vendor vendor);
        bool EditVendor(Vendor vendor);
        bool IsExist(Vendor vendor);
        bool IsUsed(int id);
    }
}
