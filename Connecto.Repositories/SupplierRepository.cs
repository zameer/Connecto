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
        public Tuple<IList<Supplier>, int> GetAll(FilterCriteria filter)
        {
            return SupplierDao.GetSuppliers(filter);
        }
        /// <summary>
        /// Get List of Suppliers
        /// </summary>
        /// <returns>IList of Suppliers</returns>
        public IList<Supplier> GetAll()
        {
            return SupplierDao.GetSuppliers();
        }
        /// <summary>
        /// Get List of Person
        /// </summary>
        /// <returns>IList of Person</returns>
        public IList<Person> GetPeople()
        {
            return SupplierDao.GetPeople();
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
        public int Delete(int id, int deletedBy)
        {
            return SupplierDao.DeleteSupplier(id, deletedBy);
        }

        /// <summary>
        /// Create new Supplier
        /// </summary>
        /// <param name="supplier">Create Supplier object</param>
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