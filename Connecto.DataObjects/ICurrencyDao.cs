using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Connecto.BusinessObjects;

namespace Connecto.DataObjects
{
    /// <summary>
    /// Defines methods to access currency
    /// </summary>
    /// <remarks>
    /// This is a database independent interface, implementations are database specific
    /// </remarks>
    public interface ICurrencyDao
    {
        /// <summary>
        /// Gets List of Currency
        /// </summary>
        /// <returns>List of Currency</returns>
        IList<Currency> GetCurrencys();

        /// <summary>
        /// Get specific Currency
        /// </summary>
        /// <param name="id">Unique Currency identifier</param>
        /// <returns>Specific Currency Details</returns>
        Currency GetCurrencyById(int id);

        /// <summary>
        /// Remove specific Currency
        /// </summary>
        /// <param name="id">Unique Currency identifier</param>
        /// <returns>No of Currency Deleted</returns>
        int DeleteCurrency(int id = 0);

        

        /// <summary>
        /// Add specific Currency
        /// </summary>
        /// <param name="id">Unique Currency identifier</param>
        /// <returns>Currency ID</returns>
        int AddCurrency(Currency currency);
    }
}
