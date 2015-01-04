using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Connecto.BusinessObjects;
using Connecto.DataObjects;

namespace Connecto.Repositories
{
    public class CurrencyRepository
    {
        private static readonly ICurrencyDao CurrencyDao = DataAccess.CurrencyDao;

        /// <summary>
        /// Get List of Currency
        /// </summary>
        /// <returns>IList of Currency</returns>
        public IList<Currency> GetAll()
        {
            return CurrencyDao.GetCurrencys();
        }

        /// <summary>
        /// Get a specific Currency
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Return Currency ID</returns>
        public Currency GetCurrencyById(int id)
        {
            return CurrencyDao.GetCurrencyById(id);
        }

        /// <summary>
        /// Removes specific Currency
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>No of Currency Deleted</returns>
        public int Delete(int id = 0)
        {
            return CurrencyDao.DeleteCurrency(id);
        }

        /// <summary>
        /// Create new Currency
        /// </summary>
        /// <param name="vendor">Create Currency object</param>
        public void Add(Currency currency)
        {
            CurrencyDao.AddCurrency(currency);
        }
    }
}