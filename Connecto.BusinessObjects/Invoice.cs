using System;
using System.Collections.Generic;
using Connecto.Common.Enumeration;

namespace Connecto.BusinessObjects
{
    public class Invoice : Connecto
    {
        public int InvoiceId { get; set; }
        public Guid InvoiceGuid { get; set; }
        public InvoiceType InvoiceType { get; set; }
    }
}
