using System;
using System.Collections.Generic;
using System.Linq;
using Connecto.BusinessObjects;
using Connecto.Common.Enumeration;
using Connecto.DataObjects.EntityFramework.ModelMapper;

namespace Connecto.DataObjects.EntityFramework.Implementation
{
    public class EntityProductDetailDao : IProductDetailDao
    {
        public List<Order> GetOrders()
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var orderIds = context.ProductDetailCarts.Select(e => e.OrderId).Distinct().ToList();
                return context.Orders.Where(e => orderIds.Contains(e.OrderId)).Select(Mapper.Map).ToList();
            }
        }

        public List<ProductDetail> GetProductDetails(int orderId)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var productDetails = context.ProductDetails.Where(e => e.OrderId == orderId && e.Status == RecordStatus.Active).ToList();
                return productDetails.Select(Mapper.Map).ToList();
            }
        }
        public List<ProductDetailCart> GetProductDetailsCart(int orderId)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var productDetailsCart = context.ProductDetailCarts.Where(e => e.OrderId == orderId && e.Status == RecordStatus.Active).ToList();
                return productDetailsCart.Select(Mapper.Map).ToList();
            }
        }

        public int AddProductDetailCart(ProductDetailCart productDetailCart)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                productDetailCart.ProductDetailGuid = Guid.NewGuid();
                productDetailCart.CreatedOn = DateTime.Now;
                productDetailCart.Status = RecordStatus.Active;
                var entity = Mapper.Map(productDetailCart);
                if(productDetailCart.OrderId.Equals(0))
                    entity.OrderId = AddInvoice(context, productDetailCart);

                if (!UpdateProductDetailCart(productDetailCart, context)) context.ProductDetailCarts.Add(entity);
                context.SaveChanges();
                return entity.OrderId;
            }
        }
        private bool UpdateProductDetailCart(ProductDetailCart productDetailCart, ConnectoManagerEntities context)
        {
            var cart = context.ProductDetailCarts.FirstOrDefault(e => e.OrderId == productDetailCart.OrderId && e.ProductCode == productDetailCart.ProductCode);
            if (cart == null) return false;
            cart.EmployeeId = productDetailCart.EmployeeId;
            cart.Quantity = productDetailCart.Quantity;
            cart.QuantityActual = productDetailCart.QuantityActual;
            cart.QuantityLower = productDetailCart.QuantityLower;
            cart.UnitPrice = productDetailCart.UnitPrice;
            cart.SellingPrice = productDetailCart.SellingPrice;
            cart.ProductId = productDetailCart.ProductId;
            cart.SupplierId = productDetailCart.SupplierId;
            cart.Status = productDetailCart.Status;
            return true;
        }
        public int AddProductDetail(int invoiceId)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var productDetailsCart = context.ProductDetailCarts.Where(e => e.OrderId == invoiceId && e.Status == RecordStatus.Active).ToList();
                var cartsToRemove = new List<EntityProductDetailCart>();
                foreach (var item in productDetailsCart)
                {
                    var product = context.Products.FirstOrDefault(e => e.ProductId == item.ProductId);
                    if (product == null) continue;
                    product.Quantity += item.Quantity;
                    product.QuantityActual += item.QuantityActual;
                    product.QuantityLower += item.QuantityLower;
                    product.StockInHand += item.Quantity;
                    context.ProductDetails.Add(Mapper.MapDiff(item));
                    cartsToRemove.Add(item);
                }
                if (cartsToRemove.Count <= 0) return cartsToRemove.Count;
                context.ProductDetailCarts.RemoveRange(productDetailsCart);
                context.SaveChanges();
                return cartsToRemove.Count;
            }
        }

        internal int AddInvoice(ConnectoManagerEntities context, ProductDetailCart item)
        {
            var entity = new EntityOrder
            {
                OrderGuid = Guid.NewGuid(),
                LocationId = item.LocationId,
                Status = item.Status,
                OrderDate = item.DateReceived,
                CreatedBy = item.CreatedBy,
                CreatedOn = item.CreatedOn
            };
            context.Orders.Add(entity);
            context.SaveChanges();
            return entity.OrderId;
        }
        public bool EditProductDetailCart(ProductDetailCart cart)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = context.ProductDetailCarts.FirstOrDefault(s => s.ProductDetailId == cart.ProductDetailId);
                entity.ProductCode = cart.ProductCode;
                entity.ProductId = cart.ProductId;
                entity.SupplierId = cart.SupplierId;
                entity.Quantity = cart.Quantity;
                entity.UnitPrice = cart.UnitPrice;
                entity.SellingPrice = cart.SellingPrice;
                entity.EditedBy = cart.EditedBy;
                entity.EditedOn = cart.EditedOn;
                return context.SaveChanges() > 0;
            }
        }
        public bool UpdateOrder(ProductDetailCart cart)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var invoice = context.Orders.FirstOrDefault(e => e.OrderId == cart.OrderId);
                if (invoice == null) return false;
                invoice.OrderDate = cart.DateReceived;
                invoice.EmployeeId = cart.EmployeeId;
                invoice.SupplierId = cart.SupplierId == 0 ? (int?)null : cart.SupplierId;
                invoice.ReferenceCode = cart.ReferenceCode;
                return context.SaveChanges() > 0;
            }
        }
        public int DeleteProductDetailCart(int id, int deletedBy)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = context.ProductDetailCarts.FirstOrDefault(s => s.ProductDetailId == id);
                entity.Status = RecordStatus.Deleted;
                entity.EditedOn = DateTime.Now;
                entity.EditedBy = deletedBy;
                return context.SaveChanges();
            }
        }

        public List<ProductDetail> GetProductCodes(int locationId)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var productDetails = context.ProductDetails.Where(s => s.LocationId == locationId).ToList();
                var items = new List<ProductDetail>();
                foreach (var detail in productDetails.Where(detail => !items.Any(e => e.ProductCode == detail.ProductCode && e.ProductId == detail.ProductId)))
                {
                    items.Add(Mapper.Map(detail));
                }
                return items;
            }
        }
    }
}
