﻿using System;
using System.Collections.Generic;
using System.Linq;
using Connecto.BusinessObjects;
using Connecto.Common.Enumeration;
using Connecto.DataObjects.EntityFramework.ModelMapper;

namespace Connecto.DataObjects.EntityFramework.Implementation
{
    public class EntityProductDetailDao : IProductDetailDao
    {
        public List<ProductDetail> GetProductDetails(int invoiceId)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var productDetails = context.ProductDetails.Where(e => e.InvoiceId == invoiceId && e.Status == RecordStatus.Active).ToList();
                return productDetails.Select(Mapper.Map).ToList();
            }
        }
        public List<ProductDetailCart> GetProductDetailsCart(int invoiceId)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var productDetailsCart = context.ProductDetailCarts.Where(e => e.InvoiceId == invoiceId && e.Status == RecordStatus.Active).ToList();
                return productDetailsCart.Select(Mapper.Map).ToList();
            }
        }

        public int AddProductDetailCart(ProductDetailCart productDetailCart)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = Mapper.Map(productDetailCart);
                entity.InvoiceId = AddInvoice(context, InvoiceType.Buying, productDetailCart);
                
                context.ProductDetailCarts.Add(entity);
                context.SaveChanges();
                return entity.ProductDetailId;
            }
        }

        internal int AddInvoice(ConnectoManagerEntities context, InvoiceType invoiceType, ProductDetailCart item)
        {
            var entity = new EntityInvoice
            {
                InvoiceGuid = Guid.NewGuid(),
                InvoiceType = invoiceType,
                LocationId = item.LocationId,
                Status = item.Status,
                CreatedBy = item.CreatedBy,
                CreatedOn = item.CreatedOn
            };
            context.Invoices.Add(entity);
            context.SaveChanges();
            return entity.InvoiceId;
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
        public int DeleteProductDetailCart(int id, int deletedBy)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = context.ProductDetailCarts.FirstOrDefault(s => s.ProductId == id);
                entity.Status = RecordStatus.Deleted;
                entity.EditedOn = DateTime.Now;
                entity.EditedBy = deletedBy;
                return context.SaveChanges();
            }
        }

    }
}