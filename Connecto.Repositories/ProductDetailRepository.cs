﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Connecto.BusinessObjects;
using Connecto.DataObjects;

namespace Connecto.Repositories
{
    public class ProductDetailRepository
    {
        private static readonly IProductDetailDao ProductDetailDao = DataAccess.ProductDetailDao;
        public IList<Order> GetOrders(bool fromCart)
        {
            return ProductDetailDao.GetOrders(fromCart);
        }
        public IList<ProductDetail> GetAll(int orderId)
        {
            return ProductDetailDao.GetProductDetails(orderId);
        }

        public IList<ProductDetailCart> GetCart(int orderId)
        {
            return ProductDetailDao.GetProductDetailsCart(orderId);
        }

        public int AddToCart(ProductDetailCart productDetailCart)
        {
            return ProductDetailDao.AddProductDetailCart(productDetailCart);
        }
        public int Update(ProductDetail productDetail)
        {
            return ProductDetailDao.Update(productDetail);
        }
        public int Add(int orderId)
        {
            return ProductDetailDao.AddProductDetail(orderId);
        }

        public void EditCart(ProductDetailCart productDetailCart)
        {
            ProductDetailDao.EditProductDetailCart(productDetailCart);
        }
        public bool UpdateOrder(ProductDetailCart productDetailCart)
        {
            return ProductDetailDao.UpdateOrder(productDetailCart);
        }
        public int Delete(int id, int deletedBy)
        {
            return ProductDetailDao.DeleteProductDetailCart(id, deletedBy);
        }

        public List<ProductDetail> GetProductCodes(int locationId)
        {
            return ProductDetailDao.GetProductCodes(locationId);
        }
    }
}