﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Connecto.BusinessObjects;

namespace Connecto.DataObjects
{
    /// <summary>
    /// Defines methods to access ProductReturn
    /// </summary>
    /// <remarks>
    /// This is a database independent interface, implementations are database specific
    /// </remarks>
    public interface ICustomerReturnDao
    {

        List<int> Get();
        List<SalesDetail> GetSalesDetailByOrderId(int orderId);
        int ReturnProduct(ReturnProduct returnProduct);
        bool IsExist(CustomerReturn customerReturn);
        bool IsUsed(int id);
    }
}