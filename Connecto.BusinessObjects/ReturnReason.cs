using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connecto.BusinessObjects
{
    public class ReturnReason : Connecto
    {
        public int ReturnReasonId { get; set; }
        public Guid ReturnReasonGuid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
