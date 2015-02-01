using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Connecto.BusinessObjects;
using Connecto.DataObjects;

namespace Connecto.Repositories
{
    public class MeasureRepository
    {
        private static readonly IMeasureDao MeasureDao = DataAccess.MeasureDao;

        /// <summary>
        /// Get List of Measure
        /// </summary>
        /// <returns>IList of Measure</returns>
        public IList<Measure> GetAll()
        {
            return MeasureDao.GetMeasures();
        }

        /// <summary>
        /// Get a specific Measure
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Return Measure ID</returns>
        public Measure GetMeasureById(int id)
        {
            return MeasureDao.GetMeasureById(id);
        }

        /// <summary>
        /// Removes specific Measure
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>No of Measure Deleted</returns>
        public int Delete(int id, int deletedBy)
        {
            return MeasureDao.DeleteMeasure(id, deletedBy);
        }

        /// <summary>
        /// Create new Measure
        /// </summary>
        /// <param name="vendor">Create Measure object</param>
        public void Add(Measure measure)
        {
            MeasureDao.AddMeasure(measure);
        }
        public void Edit(Measure measure)
        {
            MeasureDao.EditMeasure(measure);
        }
    }
}