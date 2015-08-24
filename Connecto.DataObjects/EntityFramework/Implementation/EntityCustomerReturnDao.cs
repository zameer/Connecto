using System.Collections.Generic;
using System.Linq;
using Connecto.BusinessObjects;
using Connecto.DataObjects.EntityFramework.ModelMapper;
using System;
using Connecto.Common.Enumeration;

namespace Connecto.DataObjects.EntityFramework.Implementation
{
    /// <summary>
    /// Entity Framework implementation of the IProductReturnDao interface.
    /// </summary>
    public class EntityCustomerReturnDao : ICustomerReturnDao
    {
        public List<int> Get()
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                return context.CustomerReturns.Select(e => e.CustomerReturnId).Distinct().ToList();
            }
        }
        public List<SalesDetail> GetSalesDetailByOrderId(int orderId)
        {
            var productReturns = new List<SalesDetail>();
            using (var context = DataObjectFactory.CreateContext())
            {
                var salesDetails = context.SalesDetails.Where(e => e.OrderId.Equals(orderId));
                foreach (var salesDetail in salesDetails)
                {
                    var product = salesDetail.ProductDetail.Product;
                    productReturns.Add(new SalesDetail
                    {
                        ProductName = product.Name,
                        ProductCode= salesDetail.ProductDetail.ProductCode,
                    });
                }   
                return productReturns;
            }
        }
        public IList<ReturnReason> GetProductReturns()
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var returnReasons = context.ReturnReasons.ToList();
                return returnReasons.Select(Mapper.Map).ToList();
            }
        }
        public int ReturnProduct(ReturnProduct returnProduct)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = Mapper.Map(returnProduct);
               
                //Update sales details
                var salesDetail = new SalesDetail {
                    SalesDetailId = (int)returnProduct.SalesDetailId,
                    Quantity = returnProduct.ReturnQuantity,
                    QuantityActual = returnProduct.ReturnQuantityActual,
                    QuantityLower = returnProduct.ReturnQuantityLower
                };
                UpdateSalesDetail(salesDetail, returnProduct.SyncStock, context);

                context.CustomerReturns.Add(entity);
                context.SaveChanges();
                return entity.CustomerReturnId;
            }
        }
        private bool UpdateSalesDetail(SalesDetail salesDetail, bool syncStock, ConnectoManagerEntities context)
        {
            var entity = context.SalesDetails.FirstOrDefault(e => e.SalesDetailId == salesDetail.SalesDetailId);
            if (entity == null) return false;
            if (salesDetail.Quantity > 0) entity.Quantity -= salesDetail.Quantity;
            if (salesDetail.QuantityActual > 0) entity.QuantityActual -= salesDetail.QuantityActual;
            if (salesDetail.QuantityLower > 0) entity.QuantityLower -= salesDetail.QuantityLower;

            if (syncStock)
            {
                var prodDetail = context.ProductDetails.FirstOrDefault(e => e.ProductDetailId == entity.ProductDetailId);
                var product = context.Products.FirstOrDefault(e => e.ProductId == prodDetail.ProductId);

                if (salesDetail.Quantity > 0) {
                    product.Quantity += salesDetail.Quantity;
                    product.StockInHand += salesDetail.Quantity;

                    prodDetail.Quantity += salesDetail.Quantity;
                }
                if (salesDetail.QuantityActual > 0) { 
                    product.QuantityActual += salesDetail.QuantityActual;
                    prodDetail.QuantityActual += salesDetail.QuantityActual;
                }
                if (salesDetail.QuantityLower > 0)
                {
                    product.QuantityLower += salesDetail.QuantityLower;
                    prodDetail.QuantityLower += salesDetail.QuantityLower;
                }
            }

            return true;
        }

    }
}
