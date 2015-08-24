using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Connecto.BusinessObjects;
using Connecto.DataObjects;

namespace Connecto.Repositories
{
    public class CustomerReturnRepository
    {
        private static readonly ICustomerReturnDao CustomerReturnDao = DataAccess.CustomerReturnDao;


        public IList<int> Get()
        {
            return CustomerReturnDao.Get();
        }
        public List<SalesDetail> GetSalesDetailByOrderId(int orderId)
        {
            return CustomerReturnDao.GetSalesDetailByOrderId(orderId);
        }
        public int ReturnProduct(ReturnProduct returnProduct)
        {
            return CustomerReturnDao.ReturnProduct(returnProduct);
        }

    }
}