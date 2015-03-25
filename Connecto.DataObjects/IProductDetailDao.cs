using System.Collections.Generic;
using Connecto.BusinessObjects;

namespace Connecto.DataObjects
{
    public interface IProductDetailDao
    {
        List<int> GetInvoices();
        List<ProductDetail> GetProductDetails(int orderId);
        List<ProductDetailCart> GetProductDetailsCart(int orderId);
        int AddProductDetailCart(ProductDetailCart productDetailCart);
        bool EditProductDetailCart(ProductDetailCart product);
        int DeleteProductDetailCart(int id, int deletedBy);
        int AddProductDetail(int orderId);
    }
}
