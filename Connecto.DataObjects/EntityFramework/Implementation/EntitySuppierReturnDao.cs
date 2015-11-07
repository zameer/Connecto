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
    public class EntitySuppierReturnDao : ISupplierReturnDao
    {
        public List<int> GetOrders()
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                return context.SalesDetailCarts.Select(e => e.InvoiceId).Distinct().ToList();
            }
        }
        public List<ProductReturn> GetProductReturnByCode(string productCode)
        {
            var productReturns = new List<ProductReturn>();
            using (var context = DataObjectFactory.CreateContext())
            {
                var productDetails = context.ProductDetails.Where(e => e.ProductCode.Equals(productCode));
                foreach (var productDetail in productDetails)
                {
                    var measure = productDetail.Product.ProductType.Measure;
                    productReturns.Add(new ProductReturn
                    {
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
        public List<ProductReturn> GetProductReturnsById(int productReturnId)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var productReturns = context.ProductReturns.Where(e => e.ProductReturnId == productReturnId && e.Status == RecordStatus.Active).ToList();
                return productReturns.Select(Mapper.Map).ToList();
            }
        }

        // get ProductReturn by id
        public ProductReturn GetProductReturnById(int productReturnId)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = context.ProductReturns.FirstOrDefault(e => e.ProductReturnId == productReturnId);
                return entity == null ? null : Mapper.Map(entity);
            }
        }

        public int DeleteProductReturn(int id, int deletedBy)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = context.ProductReturns.FirstOrDefault(s => s.ProductReturnId == id);
                context.ProductReturns.Remove(entity);
                return context.SaveChanges();
            }
        } 

        public int AddProductReturn(ProductReturn productReturn)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = Mapper.Map(productReturn);
                context.ProductReturns.Add(entity);
                context.SaveChanges();
                return entity.ProductReturnId;
            }
        }

        public bool EditProductReturn(ProductReturn productReturn)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = context.ProductReturns.FirstOrDefault(s => s.ProductReturnId == productReturn.ProductReturnId);
                entity.EditedBy = productReturn.EditedBy;
                entity.DateReturned = DateTime.Now;
                return context.SaveChanges() > 0;
            }
        }

        /*public bool IsExist(ProductReturn productReturn)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                if (productReturn.ProductReturnId > 0)
                    return context.ProductReturns.Any(e => e.ProductReturnId != productReturn.ProductReturnId && e.Name.ToLower() == vendor.Name.ToLower());
                return context.Vendors.Any(e => e.Name.ToLower() == vendor.Name.ToLower());
            }
        }*/

        public bool IsUsed(int id)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                return context.ProductReturns.Any(s => s.ProductReturnId == id && s.Status == RecordStatus.Active);
            }
        }
    }
}
