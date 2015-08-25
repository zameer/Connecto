using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connecto.BusinessObjects
{
    public class ProductReturn : Connecto
    {

        public int ProductReturnId { get; set; }
        public Guid ProductReturnGuid { get; set; }
        public DateTime DateReturned { get; set; }
        public int? ProductDetailId { get; set; }
        public int? SalesDetailId { get; set; }
        public int? ReturnReasonId { get; set; }
    }
}
