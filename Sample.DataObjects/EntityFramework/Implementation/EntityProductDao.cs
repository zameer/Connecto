using System.Collections.Generic;
using System.Linq;
using Connecto.BusinessObjects;
using Connecto.DataObjects.EntityFramework.ModelMapper;

namespace Connecto.DataObjects.EntityFramework.Implementation
{
    /// <summary>
    /// Entity Framework implementation of the IProductDao interface.
    /// </summary>
    public class EntityProductDao : IProductDao
    {
        public List<Product> GetProducts()
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var products = context.Products.ToList();
                return products.Select(Mapper.Map).ToList();
            }
        }

        public int AddProduct(Product product)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = Mapper.Map(product);
                context.Products.Add(entity);
                context.SaveChanges();
                return entity.ProductId;
            }
        }
    }
}
