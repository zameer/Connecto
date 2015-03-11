using System;
using System.Collections.Generic;
using System.Linq;
using Connecto.BusinessObjects;
using Connecto.Common.Enumeration;
using Connecto.DataObjects.EntityFramework.ModelMapper;

namespace Connecto.DataObjects.EntityFramework.Implementation
{
    /// <summary>
    /// Entity Framework implementation of the IProductDao interface.
    /// </summary>
    public class EntityProductDao : IProductDao
    {
        // get all products
        public List<Product> GetProducts()
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var products = context.Products.Where(e => e.Status == RecordStatus.Active).ToList();
                return products.Select(Mapper.Map).ToList();
            }
        }

        
        // get product by id
        public Product GetProduct(int productId)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = context.Products.FirstOrDefault(e => e.ProductId == productId);
                return entity == null ? null : Mapper.Map(entity);
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
        public bool EditProduct(Product product)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = context.Products.FirstOrDefault(s => s.ProductId == product.ProductId);
                entity.ProductTypeId = product.ProductTypeId;
                entity.VendorId = product.VendorId;
                entity.Name = product.Name;
                entity.StockInHand = product.StockInHand;
                entity.Quantity = product.Quantity;
                entity.Reorderlevel = product.Reorderlevel;
                entity.EditedBy = product.EditedBy;
                entity.EditedOn = product.EditedOn;
                return context.SaveChanges() > 0;
            }
        }
        public int DeleteProduct(int id, int deletedBy)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = context.Products.FirstOrDefault(s => s.ProductId == id);
                entity.Status = RecordStatus.Deleted;
                entity.EditedOn = DateTime.Now;
                entity.EditedBy = deletedBy;
                return context.SaveChanges();
            }
        }

    }
}
