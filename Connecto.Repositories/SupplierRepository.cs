using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Connecto.BusinessObjects;
using Connecto.DataObjects;

namespace Connecto.Repositories
{
    public class SupplierRepository
    {
        private static readonly ISupplierDao SupplierDao = DataAccess.SupplierDao;

        /// <summary>
        /// Get List of Suppliers
        /// </summary>
        /// <returns>IList of Suppliers</returns>
        public IList<Supplier> GetAll()
        {
            return SupplierDao.GetSuppliers();
        }

        /// <summary>
        /// Get a specific Supplier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Return Supplier ID</returns>
        public Supplier GetSupplierById(int id)
        {
            return SupplierDao.GetSupplierById(id);
        }

        /// <summary>
        /// Removes specific Supplier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>No of Supplier Deleted</returns>
        public int Delete(int id = 0)
        {
            return SupplierDao.DeleteSupplier(id);
        }

        /// <summary>
        /// Create new Supplier
        /// </summary>
        /// <param name="vendor">Create Supplier object</param>
        public void Add(Supplier supplier)
        {
            SupplierDao.AddSupplier(supplier);
        }
        /// <summary>
        /// Create new Supplier
        /// </summary>
        /// <param name="vendor">Create Supplier object</param>
        public void Edit(Supplier supplier)
        {
            SupplierDao.EditSupplier(supplier);
        }
    }
}