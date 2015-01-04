using System.Collections.Generic;
using System.Linq;
using Connecto.BusinessObjects;
using Connecto.DataObjects.EntityFramework.ModelMapper;

namespace Connecto.DataObjects.EntityFramework.Implementation
{
    /// <summary>
    /// Entity Framework implementation of the ICurrencyDao interface.
    /// </summary>
    public class EntityCurrencyDao : ICurrencyDao
    {
        public IList<Currency> GetCurrencys()
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var currencys = context.Currencys.ToList();
                return currencys.Select(Mapper.Map).ToList();
            }
        }

        // get Currency by id
        public Currency GetCurrencyById(int currencyId)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = context.Currencys.FirstOrDefault(e => e.CurrencyId == currencyId);
                return entity == null ? null : Mapper.Map(entity);
            }
        }

        public int DeleteCurrency(int id = 0)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = context.Currencys.FirstOrDefault(s => s.CurrencyId == id);
                context.Currencys.Remove(entity);
                return context.SaveChanges();
            }
        }

        public int AddCurrency(Currency currency)
        {
            using (var context = DataObjectFactory.CreateContext())
            {
                var entity = Mapper.Map(currency);
                context.Currencys.Add(entity);
                context.SaveChanges();
                return entity.CurrencyId;
            }
        }
    }
}
