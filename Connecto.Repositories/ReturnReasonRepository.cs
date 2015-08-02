using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Connecto.BusinessObjects;
using Connecto.DataObjects;

namespace Connecto.Repositories
{
    public class ReturnReasonRepository
    {
        private static readonly IReturnReasonDao ReturnReasonDao = DataAccess.ReturnReasonDao;
        public Tuple<IList<ReturnReason>, int> GetAllSearch(FilterCriteria filter)
        {
            return ReturnReasonDao.GetReturnReasonsSearch(filter);
        }
        /// <summary>
        /// Get List of ReturnReasons
        /// </summary>
        /// <returns>IList of ReturnReasons</returns>
        public IList<ReturnReason> GetAll()
        {
            return ReturnReasonDao.GetReturnReasons();
        }

        /// <summary>
        /// Get a specific Vendor
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Return Vendor ID</returns>
        public ReturnReason GetReturnReasonById(int id)
        {
            return ReturnReasonDao.GetReturnReasonById(id);
        }

        /// <summary>
        /// Removes specific vendor
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>No of vendors Deleted</returns>
        public int Delete(int id, int deletedBy)
        {
            return ReturnReasonDao.DeleteReturnReason(id, deletedBy);
        }

        /// <summary>
        /// Create new vendor
        /// </summary>
        /// <param name="vendor">Create vendor object</param>
        public void Add(ReturnReason returnReason)
        {
            ReturnReasonDao.AddReturnReason(returnReason);
        }

        /// <summary>
        /// Create new Vendor
        /// </summary>
        /// <param name="vendor">Create Vendor object</param>
        public void Edit(ReturnReason returnReason)
        {
            ReturnReasonDao.EditReturnReason(returnReason);
        }
        public bool IsExist(ReturnReason returnReason)
        {
            return ReturnReasonDao.IsExist(returnReason);
        }

        public bool IsUsed(int id)
        {
            return ReturnReasonDao.IsUsed(id);
        }
    }
}