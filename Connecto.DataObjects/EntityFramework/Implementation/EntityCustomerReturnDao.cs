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
                        /*ProductDetailId = productDetail.ProductDetailId,
                        ProductCode = productCode,
                        SellingLower = productDetail.Product.SellingLower,
                        StockInHand = new StockInHand { Quantity = productDetail.Product.StockInHand, QuantityActual = productDetail.Product.QuantityActual, QuantityLower = productDetail.Product.QuantityLower },
                        SellingPrice = productDetail.SellingPrice,
                        SellingPriceActual = productDetail.SellingPrice,
                        StockAs = productDetail.Product.ProductType.StockAs,
                        Volume = measure.Volume,
                        ContainsQty = productDetail.Product.ContainsQty,
                        SellingMargin = productDetail.Product.SellingMargin,
                        MarginAmount = productDetail.Product.MarginAmount,
                        Measure = new Measure { Actual = measure.Actual, Lower = measure.Lower },
                        CreatedOnText = productDetail.CreatedOn.ToShortDateString()*/
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
        
    }
}
