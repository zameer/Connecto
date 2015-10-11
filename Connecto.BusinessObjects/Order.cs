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
        public string ReferenceCode { get; set; }
    }
}
