using System;
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
        public IList<int> GetInvoices()
        {
            return ProductDetailDao.GetInvoices();
        }
        public IList<ProductDetail> GetAll(int orderId)
        {
            return ProductDetailDao.GetProductDetails(orderId);
        }

        public IList<ProductDetailCart> GetCart(int orderId)
        {
            return ProductDetailDao.GetProductDetailsCart(orderId);
        }

        public void AddToCart(ProductDetailCart productDetailCart)
        {
            ProductDetailDao.AddProductDetailCart(productDetailCart);
        }
        public int Add(int orderId)
        {
            return ProductDetailDao.AddProductDetail(orderId);
        }

        public void EditCart(ProductDetailCart productDetailCart)
        {
            ProductDetailDao.EditProductDetailCart(productDetailCart);
        }

        public int Delete(int id, int deletedBy)
        {
            return ProductDetailDao.DeleteProductDetailCart(id, deletedBy);
        }
    }
}