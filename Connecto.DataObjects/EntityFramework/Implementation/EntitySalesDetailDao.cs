﻿using System;
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
        public List<Invoice> GetInvoices(bool sold)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var invoiceIds = sold ? context.SalesDetails.Where(e => (e.Quantity + e.QuantityActual + e.QuantityLower) > 0).Select(e => e.InvoiceId).Distinct().ToList()
                            : context.SalesDetailCarts.Where(e => (e.Quantity + e.QuantityActual + e.QuantityLower) > 0).Select(e => e.InvoiceId).Distinct().ToList();
                return context.Invoices.Where(e => invoiceIds.Contains(e.InvoiceId)).Select(Mapper.Map).ToList();
            }
        }

        public List<SalesDetail> GetSalesDetail(string productCode)
        {
            var salesDetails = new List<SalesDetail>();
            using (var context = DataObjectFactory.CreateContext())
            {
                var productDetails = context.ProductDetails.Where(e => (e.ProductCode.Equals(productCode) || e.Barcode.Equals(productCode)) && (e.Quantity + e.QuantityActual + e.QuantityLower) > 0).ToList();
                foreach (var productDetail in productDetails)
                {
                    var measure = productDetail.Product.ProductType.Measure;
                    salesDetails.Add( new SalesDetail
                    {
                        ProductDetailId = productDetail.ProductDetailId,
                        ProductCode = productDetail.ProductCode,
                        SellingLower = productDetail.Product.SellingLower,
                        StockInHand = new StockInHand { Quantity = productDetail.Product.StockInHand, QuantityActual = productDetail.Product.QuantityActual, QuantityLower = productDetail.Product.QuantityLower },
                        RowStockInHand = new StockInHand { Quantity = productDetail.Quantity, QuantityActual = productDetail.QuantityActual, QuantityLower = productDetail.QuantityLower },
                        SellingPrice = productDetail.SellingPrice,
                        UnitPrice = productDetail.UnitPrice,
                        SellingPriceActual = productDetail.SellingPrice,
                        StockAs = productDetail.Product.ProductType.StockAs,
                        Volume = measure.Volume,
                        ContainsQty = productDetail.Product.ContainsQty,
                        SellingMargin = productDetail.Product.SellingMargin,
                        MarginAmount = productDetail.Product.MarginAmount,
                        AutoSelling = productDetail.Product.AutoSelling,
                        AutoSellingQty = productDetail.Product.AutoSellingQty,
                        Measure = new Measure { Actual = measure.Actual, Lower = measure.Lower },
                        CreatedOnText = productDetail.CreatedOn.ToShortDateString(),
                        ReceivedInfo = string.Format("{0} - {1} - {2}{3}{4}", productDetail.CreatedOn.ToShortDateString(), productDetail.Product.Name,
                        productDetail.Quantity > 0 ? string.Format("{0} {1}s ", productDetail.Quantity, productDetail.Product.ProductType.StockAs) : string.Empty,
                        productDetail.QuantityActual > 0 ? string.Format("{0} {1}s ", productDetail.QuantityActual, measure.Actual) : string.Empty,
                        productDetail.QuantityLower > 0 ? string.Format("{0} {1}s ", productDetail.QuantityLower, measure.Lower) : string.Empty) 
                    });
                }
                return salesDetails;
            }
        }
        public List<SalesDetail> GetSalesDetails(int invoiceId)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var salesDetails = context.SalesDetails.Where(e => e.InvoiceId == invoiceId && e.Status == RecordStatus.Active).ToList();
                return salesDetails.Select(Mapper.Map).ToList();
            }
        }
        public List<SalesDetailCart> GetSalesDetailsCart(int invoiceId)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var salesDetailsCart = context.SalesDetailCarts.Where(e => e.InvoiceId == invoiceId && e.Status == RecordStatus.Active).ToList();
                return salesDetailsCart.Select(Mapper.Map).ToList();
            }
        }
        public List<SalesDetail> GetSoldSalesDetailsCart(int invoiceId)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var salesDetails = context.SalesDetails.Where(e => e.InvoiceId == invoiceId && e.Status == RecordStatus.Active).ToList();
                return salesDetails.Select(Mapper.Map).ToList();
            }
        }

        public int AddSalesDetailCart(SalesDetailCart salesDetailCart)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                salesDetailCart.SalesDetailGuid = Guid.NewGuid();
                salesDetailCart.CreatedOn = DateTime.Now;
                salesDetailCart.Status = RecordStatus.Active;
                var entity = Mapper.Map(salesDetailCart);

                if (salesDetailCart.InvoiceId.Equals(0))
                    entity.InvoiceId = AddInvoice(context, salesDetailCart);

                if (!UpdateSalesDetailCart(salesDetailCart, context)) context.SalesDetailCarts.Add(entity);
                context.SaveChanges();
                return entity.InvoiceId;
            }
        }
        private bool UpdateSalesDetailCart(SalesDetailCart salesDetailCart, ConnectoManagerEntities context)
        {
            var cart = context.SalesDetailCarts.FirstOrDefault(e => e.InvoiceId == salesDetailCart.InvoiceId && e.SalesDetailId == salesDetailCart.SalesDetailId && e.ProductCode == salesDetailCart.ProductCode);
            if (cart == null) return false;
            cart.EmployeeId = salesDetailCart.EmployeeId;
            cart.Quantity = salesDetailCart.Quantity;
            cart.QuantityActual = salesDetailCart.QuantityActual;
            cart.QuantityLower = salesDetailCart.QuantityLower;
            cart.DiscountBy = salesDetailCart.DiscountBy;
            cart.DiscountAs = salesDetailCart.DiscountAs;
            cart.Discount = salesDetailCart.Discount;
            cart.Price = salesDetailCart.Price;
            cart.NetPrice = salesDetailCart.NetPrice;
            cart.EditedBy = salesDetailCart.CreatedBy;
            cart.EditedOn = salesDetailCart.CreatedOn;
            return true;
        }
        public int AddSalesDetail(int invoiceId, decimal fluctuation)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var salesDetailsCart = context.SalesDetailCarts.Where(e => e.InvoiceId == invoiceId && e.Status == RecordStatus.Active).ToList();
                var cartsToRemove = new List<EntitySalesDetailCart>();
                foreach (var item in salesDetailsCart)
                {
                    var productDetail = context.ProductDetails.FirstOrDefault(e => e.ProductDetailId == item.ProductDetailId);
                    if (productDetail == null) continue;

                    Stock.SyncStock(productDetail, item.Quantity, item.QuantityActual, item.QuantityLower, true);

                    context.SalesDetails.Add(Mapper.MapDiff(item));
                    cartsToRemove.Add(item);
                }
                var order = context.Orders.FirstOrDefault(e => e.OrderId == invoiceId);
                if (order != null) order.Fluctuation = fluctuation;

                if (cartsToRemove.Count <= 0) return cartsToRemove.Count;
                context.SalesDetailCarts.RemoveRange(salesDetailsCart);
                context.SaveChanges();
                return cartsToRemove.Count;
            }
        }
        public bool ReturnCart(SalesDetailCart salesDetailCart)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var salesDetail = context.SalesDetails.FirstOrDefault(e => e.SalesDetailId == salesDetailCart.SalesDetailId && e.Status == RecordStatus.Active);
                if (salesDetail != null)
                {
                    var productDetail = context.ProductDetails.FirstOrDefault(e => e.ProductDetailId == salesDetail.ProductDetailId);
                    var rQty = salesDetail.Quantity - salesDetailCart.Quantity;
                    var rActQty = salesDetail.QuantityActual - salesDetailCart.QuantityActual;
                    var rLwrQty = salesDetail.QuantityLower - salesDetailCart.QuantityLower;
                    Stock.SyncStock(productDetail, rQty, rActQty, rLwrQty, false);

                    salesDetail.Quantity = salesDetailCart.Quantity;
                    salesDetail.QuantityActual = salesDetailCart.QuantityActual;
                    salesDetail.QuantityLower = salesDetailCart.QuantityLower;
                    salesDetail.DiscountBy = salesDetailCart.DiscountBy;
                    salesDetail.DiscountAs = salesDetailCart.DiscountAs;
                    salesDetail.Discount = salesDetailCart.Discount;
                    salesDetail.Price = salesDetailCart.Price;
                    salesDetail.NetPrice = salesDetailCart.NetPrice;
                    salesDetail.EditedBy = salesDetailCart.CreatedBy;
                    salesDetail.EditedOn = salesDetailCart.CreatedOn;

                    var productReturn = new EntityProductReturn
                    {
                        ProductReturnGuid = Guid.NewGuid(),
                        DateReturned = salesDetailCart.CreatedOn,
                        ProductDetailId = salesDetail.ProductDetailId,
                        SalesDetailId = salesDetail.SalesDetailId,
                        Quantity = rQty,
                        QuantityActual = rActQty,
                        QuantityLower = rLwrQty,
                        LocationId = salesDetailCart.LocationId,
                        Status = salesDetailCart.Status,
                        EmployeeId = salesDetailCart.CreatedBy,
                        CreatedBy = salesDetailCart.CreatedBy,
                        CreatedOn = salesDetailCart.CreatedOn
                    };
                    context.ProductReturns.Add(productReturn);

                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        internal int AddInvoice(ConnectoManagerEntities context, SalesDetailCart item)
        {
            var entity = new EntityInvoice
            {
                InvoiceGuid = Guid.NewGuid(),
                InvoiceDate = item.DateSold,
                EmployeeId = item.EmployeeId,
                CustomerId = item.CustomerId == 0 ? (int?) null : item.CustomerId,
                LocationId = item.LocationId,
                Status = item.Status,
                CreatedBy = item.CreatedBy,
                CreatedOn = item.CreatedOn,
                ReferenceCode = item.ReferenceCode,
            };
            context.Invoices.Add(entity);
            context.SaveChanges();
            return entity.InvoiceId;
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
        public bool UpdateInvoice(SalesDetailCart salesDetailCart)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var invoice = context.Invoices.FirstOrDefault(e => e.InvoiceId == salesDetailCart.InvoiceId);
                if (invoice == null) return false;
                invoice.InvoiceDate = salesDetailCart.DateSold;
                invoice.EmployeeId = salesDetailCart.EmployeeId;
                invoice.CustomerId = salesDetailCart.CustomerId == 0 ? (int?)null : salesDetailCart.CustomerId;
                invoice.ReferenceCode = salesDetailCart.ReferenceCode;
                invoice.InvoiceDate = salesDetailCart.DateSold;
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
