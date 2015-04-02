using System;
using System.Collections.Generic;
using System.Linq;
using Connecto.BusinessObjects;
using Connecto.Common.Enumeration;
using Connecto.DataObjects.EntityFramework.ModelMapper;

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

        public SalesDetail GetSalesDetail(string productCode)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var productDetail = context.ProductDetails.FirstOrDefault(e => e.ProductCode.Equals(productCode));
                if (productDetail == null) return null;
                var measure = productDetail.Product.ProductType.Measure;
                return new SalesDetail
                {
                    ProductDetailId = productDetail.ProductDetailId,
                    ProductCode = productCode,
                    SellingLower = productDetail.Product.SellingLower,
                    StockInHand = new StockInHand { Quantity = productDetail.Product.StockInHand, QuantityActual = productDetail.Product.QuantityActual, QuantityLower = productDetail.Product.QuantityLower},
                    SellingPrice = productDetail.SellingPrice,
                    StockAs = productDetail.Product.ProductType.StockAs,
                    Volume = measure.Volume,
                    ContainsQty = productDetail.Product.ContainsQty,
                    Measure = new Measure { Actual = measure.Actual, Lower = measure.Lower }
                };
            }
        }

        internal string BuildDetailStockInHand(EntityProduct item, EntityMeasure measure)
        {
            var res = string.Format("{0}", item.StockInHand > 0 ? string.Format("{0} {1}", item.StockInHand, item.ProductType.StockAs) : string.Empty);
            res = string.Format("{0} {1}", res, item.QuantityActual > 0 ? string.Format("{0} {1}", item.QuantityActual, measure.Actual) : string.Empty);
            return string.Format("{0} {1}", res, item.QuantityLower > 0 ? string.Format("{0} {1}", item.QuantityLower, measure.Lower) : string.Empty).Trim();
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
                
                context.SalesDetailCarts.Add(entity);
                context.SaveChanges();
                return entity.SalesDetailId;
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
