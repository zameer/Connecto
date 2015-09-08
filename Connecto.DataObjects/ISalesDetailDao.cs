using System.Collections.Generic;
using Connecto.BusinessObjects;

namespace Connecto.DataObjects
{
    public interface ISalesDetailDao
    {
        List<int> GetOrders();
        //SalesDetail GetSalesDetail(string productCode);
        List<SalesDetail> GetSalesDetail(string productCode);
        List<SalesDetail> GetSalesDetails(int orderId);
        List<SalesDetailCart> GetSalesDetailsCart(int orderId);
        int AddSalesDetailCart(SalesDetailCart salesDetailCart);
        bool EditSalesDetailCart(SalesDetailCart salesDetailCart);
        int DeleteSalesDetailCart(int id, int deletedBy);
        int AddSalesDetail(int orderId, decimal fluctuation);
    }
}
