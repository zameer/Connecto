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
        public IList<Order> GetOrders(bool sold)
        {
            return Repo.GetOrders(sold);
        }
        public List<SalesDetail> GetSalesDetail(string productCode)
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

        public IList<SalesDetail> GetSoldCart(int orderId)
        {
            return Repo.GetSoldSalesDetailsCart(orderId);
        }

        public int AddToCart(SalesDetailCart salesDetailCart)
        {
            return Repo.AddSalesDetailCart(salesDetailCart);
        }
        public int Add(int orderId, decimal fluctuation)
        {
            return Repo.AddSalesDetail(orderId, fluctuation);
        }
        public void EditCart(SalesDetailCart salesDetailCart)
        {
            Repo.EditSalesDetailCart(salesDetailCart);
        }
        public bool UpdateOrder(SalesDetailCart salesDetailCart)
        {
            return Repo.UpdateOrder(salesDetailCart);
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