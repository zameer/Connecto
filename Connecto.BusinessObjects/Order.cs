using System;
using System.Collections.Generic;
using Connecto.Common.Enumeration;

namespace Connecto.BusinessObjects
{
    public class Order : Connecto
    {
        public int OrderId { get; set; }
        public Guid OrderGuid { get; set; }
        public OrderType OrderType { get; set; }
        public int? CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderDateDisplay
        {
            get { return string.Format("{0:g}", OrderDate); }
        }
        public decimal Fluctuation { get; set; }
        public string ReferenceCode { get; set; }
        public Customer Customer { get; set; }
    }
}
