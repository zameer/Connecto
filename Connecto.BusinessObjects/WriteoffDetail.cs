using System;
using System.Collections.Generic;
using Connecto.Common.Enumeration;

namespace Connecto.BusinessObjects
{
    public class WriteoffDetail : ProductBase
    {
        public int WriteoffDetailId { get; set; }
        public Guid WriteoffDetailGuid { get; set; }
        public string ProductCode { get; set; }
        public int OrderId { get; set; }
        public int ProductDetailId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Price { get; set; }
        public decimal NetPrice { get; set; }
        public DateTime DateWriteoff { get; set; }

    }
}
