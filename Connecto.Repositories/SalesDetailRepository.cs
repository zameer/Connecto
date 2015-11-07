using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Connecto.BusinessObjects;
using Connecto.DataObjects;
using Connecto.DataObjects.EntityFramework.Utility;

namespace Connecto.Repositories
{
    public class SalesDetailRepository
    {
        private static readonly ISalesDetailDao Repo = DataAccess.SalesDetailDao;
        public IList<Invoice> GetInvoices(bool sold)
        {
            return Repo.GetInvoices(sold);
        }
        public List<SalesDetail> GetSalesDetail(string productCode)
        {
            return Repo.GetSalesDetail(productCode);
        }
        public IList<SalesDetail> GetAll(int invoiceId)
        {
            return Repo.GetSalesDetails(invoiceId);
        }

        public IList<SalesDetailCart> GetCart(int orderId)
        {
            return Repo.GetSalesDetailsCart(orderId);
        }

        public IList<SalesDetail> GetSoldCart(int invoiceId)
        {
            return Repo.GetSoldSalesDetailsCart(invoiceId);
        }

        public int AddToCart(SalesDetailCart salesDetailCart)
        {
            return Repo.AddSalesDetailCart(salesDetailCart);
        }
        public int Add(int invoiceId, decimal fluctuation)
        {
            return Repo.AddSalesDetail(invoiceId, fluctuation);
        }
        public void EditCart(SalesDetailCart salesDetailCart)
        {
            Repo.EditSalesDetailCart(salesDetailCart);
        }
        public bool UpdateInvoice(SalesDetailCart salesDetailCart)
        {
            return Repo.UpdateInvoice(salesDetailCart);
        }
        public bool ReturnCart(SalesDetailCart salesDetailCart)
        {
            return Repo.ReturnCart(salesDetailCart);
        }

        public int Delete(int id, int deletedBy)
        {
            return Repo.DeleteSalesDetailCart(id, deletedBy);
        }
        public ProductBase SyncSales(int volume, int containsQty, ProductBase stock, ProductBase sold)
        {
            return Stock.SyncStock(volume, containsQty, stock, sold);
        }
        public ProductBase SyncSales(int volume, int containsQty, ProductBase stock, ProductBase sold, bool buildQuantity)
        {
            return Stock.SyncStock(volume, containsQty, stock, sold, buildQuantity);
        }
    }
}