using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connecto.BusinessObjects
{
    public class CustomerReturn : Connecto
    {

        public int CustomerReturnId { get; set; }
        public Guid CustomerReturnGuid { get; set; }
        public DateTime DateReturned { get; set; }
        public int? SalesDetailId { get; set; }
        public int? ReturnReasonId { get; set; }
    }
}
