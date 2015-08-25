using System;
using System.Collections.Generic;
using System.Linq;
using Connecto.BusinessObjects;
using Connecto.Common.Enumeration;
using Connecto.DataObjects.EntityFramework.ModelMapper;
using Connecto.DataObjects.EntityFramework.Utility;

namespace Connecto.DataObjects.EntityFramework.Implementation
{
    public class EntitySalesDetailDao : ISalesDetailDao
    {
        public List<int> GetOrders()
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                return context.SalesDetailCarts.Select(e => e.OrderId).Distinct().ToList();
            }
        }

        public List<SalesDetail> GetSalesDetail(string productCode)
        {
            var salesDetails = new List<SalesDetail>();
            using (var context = DataObjectFactory.CreateContext())
            {
                var productDetails = context.ProductDetails.Where(e => e.ProductCode.Equals(productCode));
                foreach (var productDetail in productDetails)
                {
                    var measure = productDetail.Product.ProductType.Measure;
                    salesDetails.Add( new SalesDetail
                    {
                        ProductDetailId = productDetail.ProductDetailId,
                        ProductCode = productCode,
                        SellingLower = productDetail.Product.SellingLower,
                        StockInHand = new StockInHand { Quantity = productDetail.Product.StockInHand, QuantityActual = productDetail.Product.QuantityActual, QuantityLower = productDetail.Product.QuantityLower },
                        RowStockInHand = new StockInHand { Quantity = productDetail.Quantity, QuantityActual = productDetail.QuantityActual, QuantityLower = productDetail.QuantityLower },
                        SellingPrice = productDetail.SellingPrice,
                        SellingPriceActual = productDetail.SellingPrice,
                        StockAs = productDetail.Product.ProductType.StockAs,
                        Volume = measure.Volume,
                        ContainsQty = productDetail.Product.ContainsQty,
                        SellingMargin = productDetail.Product.SellingMargin,
                        MarginAmount = productDetail.Product.MarginAmount,
                        Measure = new Measure { Actual = measure.Actual, Lower = measure.Lower },
                        CreatedOnText = productDetail.CreatedOn.ToShortDateString()
                    });
                }
                return salesDetails;
            }
        }
        public List<SalesDetail> GetSalesDetails(int orderId)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var salesDetails = context.SalesDetails.Where(e => e.OrderId == orderId && e.Status == RecordStatus.Active).ToList();
                return salesDetails.Select(Mapper.Map).ToList();
            }
        }
        public List<SalesDetailCart> GetSalesDetailsCart(int orderId)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var salesDetailsCart = context.SalesDetailCarts.Where(e => e.OrderId == orderId && e.Status == RecordStatus.Active).ToList();
                return salesDetailsCart.Select(Mapper.Map).ToList();
            }
        }

        public int AddSalesDetailCart(SalesDetailCart salesDetailCart)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = Mapper.Map(salesDetailCart);
                if (salesDetailCart.OrderId.Equals(0))
                    entity.OrderId = AddOrder(context, OrderType.Buying, salesDetailCart);

                if (!UpdateSalesDetailCart(salesDetailCart, context)) context.SalesDetailCarts.Add(entity);
                context.SaveChanges();
                return entity.OrderId;
            }
        }
        private bool UpdateSalesDetailCart(SalesDetailCart salesDetailCart, ConnectoManagerEntities context)
        {
            var cart = context.SalesDetailCarts.FirstOrDefault(e => e.OrderId == salesDetailCart.OrderId && e.SalesDetailId == salesDetailCart.SalesDetailId && e.ProductCode == salesDetailCart.ProductCode);
            if (cart == null) return false;
            cart.Quantity += salesDetailCart.Quantity;
            return true;
        }
        public int AddSalesDetail(int invoiceId)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var salesDetailsCart = context.SalesDetailCarts.Where(e => e.OrderId == invoiceId && e.Status == RecordStatus.Active).ToList();
                var cartsToRemove = new List<EntitySalesDetailCart>();
                foreach (var item in salesDetailsCart)
                {
                    var productDetail = context.ProductDetails.FirstOrDefault(e => e.ProductDetailId == item.ProductDetailId);
                    if (productDetail == null) continue;

                    Stock.SyncStock(productDetail, item.Quantity, item.QuantityActual, item.QuantityLower);

                    context.SalesDetails.Add(Mapper.MapDiff(item));
                    cartsToRemove.Add(item);
                }
                if (cartsToRemove.Count <= 0) return cartsToRemove.Count;
                context.SalesDetailCarts.RemoveRange(salesDetailsCart);
                context.SaveChanges();
                return cartsToRemove.Count;
            }
        }

        internal int AddOrder(ConnectoManagerEntities context, OrderType orderType, SalesDetailCart item)
        {
            var entity = new EntityOrder
            {
                OrderGuid = Guid.NewGuid(),
                OrderType = orderType,
                LocationId = item.LocationId,
                Status = item.Status,
                CreatedBy = item.CreatedBy,
                CreatedOn = item.CreatedOn
            };
            context.Orders.Add(entity);
            context.SaveChanges();
            return entity.OrderId;
        }
        public bool EditSalesDetailCart(SalesDetailCart cart)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = context.SalesDetailCarts.FirstOrDefault(s => s.SalesDetailId == cart.ProductDetailId);
                entity.ProductCode = cart.ProductCode;
                entity.Quantity = cart.Quantity;
                entity.UnitPrice = cart.UnitPrice;
                entity.SellingPrice = cart.SellingPrice;
                entity.EditedBy = cart.EditedBy;
                entity.EditedOn = cart.EditedOn;
                return context.SaveChanges() > 0;
            }
        }
        public int DeleteSalesDetailCart(int id, int deletedBy)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = context.SalesDetailCarts.FirstOrDefault(s => s.SalesDetailId == id);
                entity.Status = RecordStatus.Deleted;
                entity.EditedOn = DateTime.Now;
                entity.EditedBy = deletedBy;
                return context.SaveChanges();
            }
        }
    }
}
