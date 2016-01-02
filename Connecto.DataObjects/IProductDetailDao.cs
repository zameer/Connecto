using System.Collections.Generic;
using Connecto.BusinessObjects;

namespace Connecto.DataObjects
{
    public interface IProductDetailDao
    {
        List<Order> GetOrders(bool fromCart);
        List<ProductDetail> GetProductDetails(int orderId);
        List<ProductDetailCart> GetProductDetailsCart(int orderId);
        int AddProductDetailCart(ProductDetailCart productDetailCart);
        int Update(ProductDetail productDetail);
        bool EditProductDetailCart(ProductDetailCart product);
        bool UpdateOrder(ProductDetailCart productDetailCart);
        int DeleteProductDetailCart(int id, int deletedBy);
        int AddProductDetail(int orderId);
        List<ProductDetail> GetProductCodes(int locationId);
    }
}
