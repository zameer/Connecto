using System.Collections.Generic;
using System.Linq;
using Connecto.BusinessObjects;
using Connecto.DataObjects.EntityFramework.ModelMapper;
using System;
using Connecto.Common.Enumeration;

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
                var productTypes = context.ProductTypes.Where(e => e.Status == RecordStatus.Active).ToList();
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

        public int DeleteProductType(int id, int deletedBy)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = context.ProductTypes.FirstOrDefault(s => s.ProductTypeId == id);
                entity.Status = RecordStatus.Deleted;
                entity.EditedOn = DateTime.Now;
                entity.EditedBy = deletedBy;
                return context.SaveChanges();
            }
        } 


        public bool EditProductType(ProductType productType)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = context.ProductTypes.FirstOrDefault(s => s.ProductTypeId == productType.ProductTypeId);
                entity.MeasureId = productType.MeasureId;
                entity.Type = productType.Type;
                entity.StockAs = productType.StockAs;
                entity.LocationId = productType.LocationId;
                entity.EditedBy = productType.EditedBy;
                entity.EditedOn = productType.EditedOn;
                return context.SaveChanges() > 0;
            }
        }
        public bool IsExist(ProductType productType)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                if (productType.ProductTypeId > 0)
                    return context.ProductTypes.Any(e => e.ProductTypeId != productType.ProductTypeId && e.MeasureId == productType.MeasureId && e.Type.ToLower() == productType.Type.ToLower());
                return context.ProductTypes.Any(e => e.MeasureId == productType.MeasureId && e.Type.ToLower() == productType.Type.ToLower());
            }
        }

        public bool IsUsed(int id)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                return context.Products.Any(s => s.ProductTypeId == id && s.Status == RecordStatus.Active);
            }
        }
    }
}
