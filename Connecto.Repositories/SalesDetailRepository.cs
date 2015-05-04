using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Connecto.BusinessObjects;
using Connecto.DataObjects;

namespace Connecto.Repositories
{
    public class SalesDetailRepository
    {
        private static readonly ISalesDetailDao Repo = DataAccess.SalesDetailDao;
        public IList<int> GetOrders()
        {
            return Repo.GetOrders();
        }

        public SalesDetail GetSalesDetail(string productCode)
        {
            return Repo.GetSalesDetail(productCode);
        }


        public IList<SalesDetail> GetAll(int orderId)
        {
            return Repo.GetSalesDetails(orderId);
        }

        public IList<SalesDetailCart> GetCart(int orderId)
        {
            return Repo.GetSalesDetailsCart(orderId);
        }

        public int AddToCart(SalesDetailCart salesDetailCart)
        {
            return Repo.AddSalesDetailCart(salesDetailCart);
        }
        public int Add(int orderId)
        {
            return Repo.AddSalesDetail(orderId);
        }
        public void EditCart(SalesDetailCart salesDetailCart)
        {
            Repo.EditSalesDetailCart(salesDetailCart);
        }

        public int Delete(int id, int deletedBy)
        {
            return Repo.DeleteSalesDetailCart(id, deletedBy);
        }
        public ProductBase SyncSales(int volume, int containsQty, ProductBase stock, ProductBase sold)
        {
            return Repo.SyncStock(volume, containsQty, stock, sold);
        }
    }
}