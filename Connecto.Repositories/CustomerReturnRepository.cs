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
        public List<SalesDetail> GetSalesDetailByInvoiceId(int invoiceId)
        {
            return CustomerReturnDao.GetSalesDetailByInvoiceId(invoiceId);
        }
        public int ReturnProduct(ReturnProduct returnProduct)
        {
            return CustomerReturnDao.ReturnProduct(returnProduct);
        }
        public bool IsExist(CustomerReturn customerReturn)
        {
            return CustomerReturnDao.IsExist(customerReturn);
        }

        public bool IsUsed(int id)
        {
            return CustomerReturnDao.IsUsed(id);
        }

    }
}