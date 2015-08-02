using System.Collections.Generic;
using System.Linq;
using Connecto.BusinessObjects;
using Connecto.DataObjects.EntityFramework.ModelMapper;
using System;
using Connecto.Common.Enumeration;

namespace Connecto.DataObjects.EntityFramework.Implementation
{
    /// <summary>
    /// Entity Framework implementation of the IReturnReasonDao interface.
    /// </summary>
    public class EntityReturnReasonDao : IReturnReasonDao
    {
        public Tuple<IList<ReturnReason>, int> GetReturnReasonsSearch(FilterCriteria filter)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                List<ReturnReason> items;
                var count = context.ReturnReasons.Count();
                if (!string.IsNullOrEmpty(filter.sSearch))
                {
                    count = context.ReturnReasons.Count(e => e.Name.ToLower().Contains(filter.sSearch) );
                    items = context.ReturnReasons.Where(e => e.Name.ToLower().Contains(filter.sSearch) )
                        .OrderBy(e => e.ReturnReasonId).Skip(filter.iDisplayStart).Take(filter.iDisplayLength).Select(Mapper.Map).ToList();
                }
                else
                {
                    items = context.ReturnReasons.OrderBy(e => e.ReturnReasonId).Skip(filter.iDisplayStart).Take(filter.iDisplayLength).Select(Mapper.Map).ToList();
                }
                return new Tuple<IList<ReturnReason>, int>(items, count);
            }
        }
        public IList<ReturnReason> GetReturnReasons()
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var returnReasons = context.ReturnReasons.ToList();
                return returnReasons.Select(Mapper.Map).ToList();
            }
        }

        // get ReturnReason by id
        public ReturnReason GetReturnReasonById(int returnReasonId)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = context.ReturnReasons.FirstOrDefault(e => e.ReturnReasonId == returnReasonId);
                return entity == null ? null : Mapper.Map(entity);
            }
        }

        public int DeleteReturnReason(int id, int deletedBy)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = context.ReturnReasons.FirstOrDefault(s => s.ReturnReasonId == id);
                context.ReturnReasons.Remove(entity);
                return context.SaveChanges();
            }
        } 

        public int AddReturnReason(ReturnReason returnReason)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = Mapper.Map(returnReason);
                context.ReturnReasons.Add(entity);
                context.SaveChanges();
                return entity.ReturnReasonId;
            }
        }

        public bool EditReturnReason(ReturnReason returnReason)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = context.ReturnReasons.FirstOrDefault(s => s.ReturnReasonId == returnReason.ReturnReasonId);
                entity.Name = returnReason.Name;
                entity.Description = returnReason.Description;
                entity.EditedBy = returnReason.EditedBy;
                entity.EditedOn = DateTime.Now;
                return context.SaveChanges() > 0;
            }
        }
        public bool IsExist(ReturnReason returnReason)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                if (returnReason.ReturnReasonId > 0)
                    return context.ReturnReasons.Any(e => e.ReturnReasonId != returnReason.ReturnReasonId && e.Name.ToLower() == returnReason.Name.ToLower());
                return context.ReturnReasons.Any(e => e.Name.ToLower() == returnReason.Name.ToLower());
            }
        }

        public bool IsUsed(int id)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                return context.ProductReturns.Any(s => s.ReturnReasonId == id && s.Status == RecordStatus.Active);
            }
        }

    }
}
