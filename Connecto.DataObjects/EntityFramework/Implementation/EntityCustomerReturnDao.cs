using System.Collections.Generic;
using System.Linq;
using Connecto.BusinessObjects;
using Connecto.DataObjects.EntityFramework.ModelMapper;
using System;
using Connecto.Common.Enumeration;
using Connecto.DataObjects.EntityFramework.Utility;

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
            var prodDetail = context.ProductDetails.FirstOrDefault(e => e.ProductDetailId == entity.ProductDetailId);
            var product = context.Products.FirstOrDefault(e => e.ProductId == prodDetail.ProductId);
            var measure = product.ProductType.Measure;

            var returned = new ProductBase{ Quantity = salesDetail.Quantity, QuantityActual = salesDetail.QuantityActual, QuantityLower = salesDetail.QuantityLower};

            var syncedSales = Stock.SyncStock(measure.Volume, (int)product.ContainsQty, new ProductBase{ Quantity = entity.Quantity, 
                QuantityActual = entity.QuantityActual, QuantityLower = entity.QuantityLower}, returned);
            entity.Quantity = syncedSales.Quantity;
            entity.QuantityActual = syncedSales.QuantityActual;
            entity.QuantityLower = syncedSales.QuantityLower;

            if (syncStock)
            {
                var syncedProdDetail = Stock.SyncStock(measure.Volume, (int)product.ContainsQty, new ProductBase{ Quantity = prodDetail.Quantity, 
                QuantityActual = prodDetail.QuantityActual, QuantityLower = prodDetail.QuantityLower}, returned, false);

                prodDetail.Quantity = syncedProdDetail.Quantity;
                prodDetail.QuantityActual = syncedProdDetail.QuantityActual;
                prodDetail.QuantityLower = syncedProdDetail.QuantityLower;
                
                var syncedProd = Stock.SyncStock(measure.Volume, (int)product.ContainsQty, new ProductBase{ Quantity = product.Quantity, 
                QuantityActual = product.QuantityActual, QuantityLower = product.QuantityLower}, returned, false);
                product.Quantity = syncedProd.Quantity;
                product.QuantityActual = syncedProd.QuantityActual;
                product.QuantityLower = syncedProd.QuantityLower;
            }

            return true;
        }
        public bool IsExist(CustomerReturn customerReturn)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                if (customerReturn.SalesDetailId > 0)
                    return context.CustomerReturns.Any(e => e.SalesDetailId != customerReturn.SalesDetailId);
                return context.SalesDetails.Any(e => e.SalesDetailId == customerReturn.SalesDetailId);
            }
        }

        public bool IsUsed(int id)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                return context.SalesDetails.Any(s => s.OrderId == id && s.Status == RecordStatus.Active);
            }
        }

    }
}
