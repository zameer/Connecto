using System.Collections.Generic;
using System.Linq;
using Connecto.BusinessObjects;
using Connecto.DataObjects.EntityFramework.ModelMapper;
using System;
using Connecto.Common.Enumeration;

namespace Connecto.DataObjects.EntityFramework.Implementation
{
    /// <summary>
    /// Entity Framework implementation of the IProductReturnDao interface.
    /// </summary>
    public class EntityProductReturnDao : IProductReturnDao
    {

        // get ProductReturn by id
        public ProductReturn GetProductReturnById(int productReturnId)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = context.ProductReturns.FirstOrDefault(e => e.ProductReturnId == productReturnId);
                return entity == null ? null : Mapper.Map(entity);
            }
        }

        public int DeleteProductReturn(int id, int deletedBy)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = context.ProductReturns.FirstOrDefault(s => s.ProductReturnId == id);
                context.ProductReturns.Remove(entity);
                return context.SaveChanges();
            }
        } 

        public int AddProductReturn(ProductReturn productReturn)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = Mapper.Map(productReturn);
                context.ProductReturns.Add(entity);
                context.SaveChanges();
                return entity.ProductReturnId;
            }
        }

        public bool EditProductReturn(ProductReturn productReturn)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = context.ProductReturns.FirstOrDefault(s => s.ProductReturnId == productReturn.ProductReturnId);
                entity.EditedBy = productReturn.EditedBy;
                entity.DateReturned = DateTime.Now;
                return context.SaveChanges() > 0;
            }
        }

        public bool IsUsed(int id)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                return context.ProductReturns.Any(s => s.ProductReturnId == id && s.Status == RecordStatus.Active);
            }
        }
    }
}
