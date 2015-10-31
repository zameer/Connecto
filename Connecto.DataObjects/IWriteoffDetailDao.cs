using System.Collections.Generic;
using Connecto.BusinessObjects;

namespace Connecto.DataObjects
{
    public interface IWriteoffDetailDao
    {
        List<int> GetOrders(bool sold);
        List<WriteoffDetail> GetSalesDetail(string productCode);
        List<WriteoffDetail> GetSalesDetails(int orderId);
        List<WriteoffDetailCart> GetSalesDetailsCart(int orderId);
        List<WriteoffDetail> GetSoldSalesDetailsCart(int orderId);
        int AddSalesDetailCart(WriteoffDetailCart salesDetailCart);
        bool EditSalesDetailCart(WriteoffDetailCart salesDetailCart);
        int DeleteSalesDetailCart(int id, int deletedBy);
        int AddSalesDetail(int orderId, decimal fluctuation);
        bool ReturnCart(WriteoffDetailCart salesDetailCart);
    }
}
