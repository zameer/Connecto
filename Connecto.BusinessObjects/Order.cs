using System;

namespace Connecto.BusinessObjects
{
    public class Order : Connecto
    {
        public int OrderId { get; set; }
        public Guid OrderGuid { get; set; }
        public int? SupplierId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderDateDisplay
        {
            get { return string.Format("{0:g}", OrderDate); }
        }
        public decimal Fluctuation { get; set; }
        public string ReferenceCode { get; set; }
        public Supplier Supplier { get; set; }
        public Employee Employee { get; set; }
    }
}
