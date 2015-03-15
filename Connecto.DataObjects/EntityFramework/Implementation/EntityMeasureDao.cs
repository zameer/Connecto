using System.Collections.Generic;
using System.Linq;
using Connecto.BusinessObjects;
using Connecto.DataObjects.EntityFramework.ModelMapper;
using Connecto.Common.Enumeration;
using System;

namespace Connecto.DataObjects.EntityFramework.Implementation
{
    /// <summary>
    /// Entity Framework implementation of the IMeasureDao interface.
    /// </summary>
    public class EntityMeasureDao : IMeasureDao
    {
        public IList<Measure> GetMeasures()
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var measures = context.Measures.Where(e => e.Status == RecordStatus.Active).ToList();
                return measures.Select(Mapper.Map).ToList();
            }
        }

        // get Measure by id
        public Measure GetMeasureById(int measureId)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = context.Measures.FirstOrDefault(e => e.MeasureId == measureId);
                return entity == null ? null : Mapper.Map(entity);
            }
        }

        public int DeleteMeasure(int id, int deletedBy)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = context.Measures.FirstOrDefault(s => s.MeasureId == id);
                entity.Status = RecordStatus.Deleted;
                entity.EditedOn = DateTime.Now;
                entity.EditedBy = deletedBy;
                return context.SaveChanges();
            }
        }

        public int AddMeasure(Measure measure)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = Mapper.Map(measure);
                context.Measures.Add(entity);
                context.SaveChanges();
                return entity.MeasureId;
            }
        }
        public bool EditMeasure(Measure measure)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = context.Measures.FirstOrDefault(s => s.MeasureId == measure.MeasureId);
                entity.Lower = measure.Lower;
                entity.Actual = measure.Actual;
                entity.Volume = measure.Volume;
                entity.EditedBy = measure.EditedBy;
                entity.EditedOn = measure.EditedOn;
                return context.SaveChanges() > 0;
            }
        }
        public bool IsExist(Measure measure)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                if (measure.MeasureId > 0)
                    return context.Measures.Any(e => e.MeasureId != measure.MeasureId && e.Volume == measure.Volume && e.Lower.ToLower() == measure.Lower.ToLower() && e.Actual.ToLower() == measure.Actual.ToLower());
                return context.Measures.Any(e => e.Volume == measure.Volume && e.Lower.ToLower() == measure.Lower.ToLower() && e.Actual.ToLower() == measure.Actual.ToLower());
            }
        }
        public bool IsUsed(int id)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                return context.ProductTypes.Any(s => s.MeasureId == id && s.Status == RecordStatus.Active);
            }
        }
    }
}
