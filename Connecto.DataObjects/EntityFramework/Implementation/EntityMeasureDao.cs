using System.Collections.Generic;
using System.Linq;
using Connecto.BusinessObjects;
using Connecto.DataObjects.EntityFramework.ModelMapper;

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
                var measures = context.Measures.ToList();
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

        public int DeleteMeasure(int id = 0)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = context.Measures.FirstOrDefault(s => s.MeasureId == id);
                context.Measures.Remove(entity);
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
    }
}
