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

        public IList<ProductDetail> GetAll(int invoiceId)
        {
            return ProductDetailDao.GetProductDetails(invoiceId);
        }

        public IList<ProductDetailCart> GetCart(int invoiceId)
        {
            return ProductDetailDao.GetProductDetailsCart(invoiceId);
        }

        public void AddToCart(ProductDetailCart productDetailCart)
        {
            ProductDetailDao.AddProductDetailCart(productDetailCart);
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