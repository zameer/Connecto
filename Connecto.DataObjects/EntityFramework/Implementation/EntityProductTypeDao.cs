using System.Collections.Generic;
using System.Linq;
using Connecto.BusinessObjects;
using Connecto.DataObjects.EntityFramework.ModelMapper;
using System;

namespace Connecto.DataObjects.EntityFramework.Implementation
{
    /// <summary>
    /// Entity Framework implementation of the IProductTypeDao interface.
    /// </summary>
    public class EntityProductTypeDao : IProductTypeDao
    {
        // get all productTypes
        public List<ProductType> GetProductTypes()
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var productTypes = context.ProductTypes.ToList();
                return productTypes.Select(Mapper.Map).ToList();
            }
        }

        
        // get productType by id
        public ProductType GetProductType(int productId)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = context.ProductTypes.FirstOrDefault(e => e.ProductTypeId == productId);
                return entity == null ? null : Mapper.Map(entity);
            }
        }
        
        public int AddProductType(ProductType productType)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = Mapper.Map(productType);
                context.ProductTypes.Add(entity);
                context.SaveChanges();
                return entity.ProductTypeId;
            }
        }

        public int DeleteProductType(int id = 0)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = context.ProductTypes.FirstOrDefault(s => s.ProductTypeId == id);
                context.ProductTypes.Remove(entity);
                return context.SaveChanges();
            }
        }

        public bool EditProductType(ProductType productType)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = context.ProductTypes.FirstOrDefault(s => s.ProductTypeId == productType.ProductTypeId);
                entity.Type = productType.Type;
                entity.StockAs = productType.StockAs;
                entity.LocationId = productType.LocationId;
                entity.CreatedBy = productType.CreatedBy;
                entity.CreatedOn = productType.CreatedOn;
                entity.EditedBy = productType.EditedBy;
                entity.EditedOn = DateTime.Now;
                return context.SaveChanges() > 0;
            }
        }

    }
}
