using System.Collections.Generic;
using Connecto.BusinessObjects;

namespace Connecto.DataObjects
{
    public interface IProductDetailDao
    {
        List<int> GetInvoices();
        List<ProductDetail> GetProductDetails(int invoiceId);
        List<ProductDetailCart> GetProductDetailsCart(int invoiceId);
        int AddProductDetailCart(ProductDetailCart productDetailCart);
        bool EditProductDetailCart(ProductDetailCart product);
        int DeleteProductDetailCart(int id, int deletedBy);
    }
}
