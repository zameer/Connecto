using System.Collections.Generic;
using Connecto.BusinessObjects;

namespace Connecto.DataObjects
{
    public interface ISalesDetailDao
    {
        List<Invoice> GetInvoices(bool sold);
        List<SalesDetail> GetSalesDetail(string productCode);
        List<SalesDetail> GetSalesDetails(int invoiceId);
        List<SalesDetailCart> GetSalesDetailsCart(int invoiceId);
        List<SalesDetail> GetSoldSalesDetailsCart(int invoiceId);
        int AddSalesDetailCart(SalesDetailCart salesDetailCart);
        bool EditSalesDetailCart(SalesDetailCart salesDetailCart);
        bool UpdateInvoice(SalesDetailCart salesDetailCart);
        int DeleteSalesDetailCart(int id, int deletedBy);
        int AddSalesDetail(int invoiceId, decimal fluctuation);
        bool ReturnCart(SalesDetailCart salesDetailCart);
    }
}
