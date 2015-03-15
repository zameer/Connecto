using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Connecto.BusinessObjects;

namespace Connecto.DataObjects
{
    /// <summary>
    /// Defines methods to access Measure
    /// </summary>
    /// <remarks>
    /// This is a database independent interface, implementations are database specific
    /// </remarks>
    public interface IMeasureDao
    {
        /// <summary>
        /// Gets List of Measure
        /// </summary>
        /// <returns>List of Measures</returns>
        IList<Measure> GetMeasures();

        /// <summary>
        /// Get specific Measure
        /// </summary>
        /// <param name="id">Unique Measure identifier</param>
        /// <returns>Specific Measure Details</returns>
        Measure GetMeasureById(int id);

        /// <summary>
        /// Remove specific Measure
        /// </summary>
        /// <param name="id">Unique Measure identifier</param>
        /// <returns>No of Measures Deleted</returns>
        int DeleteMeasure(int id, int deletedBy);

        /// <summary>
        /// Add specific Measure
        /// </summary>
        /// <param name="id">Unique Measure identifier</param>
        /// <returns>Measure ID</returns>
        int AddMeasure(Measure measure);

        bool EditMeasure(Measure measure);

        bool IsExist(Measure measure);

        bool IsUsed(int id);
    }
}
