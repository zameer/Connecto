using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connecto.BusinessObjects
{
    public class Currency
    {
        
        public int CurrencyId { get; set; }
        public Guid CurrencyGuid { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
    }
}
