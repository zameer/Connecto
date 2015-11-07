using System;

namespace Connecto.BusinessObjects
{
    public class Invoice : Connecto
    {
        public int InvoiceId { get; set; }
        public Guid InvoiceGuid { get; set; }
        public int? CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string InvoiceDateDisplay
        {
            get { return string.Format("{0:g}", InvoiceDate); }
        }
        public decimal Fluctuation { get; set; }
        public string ReferenceCode { get; set; }
        public Customer Customer { get; set; }
        public Employee Employee { get; set; }
    }
}
