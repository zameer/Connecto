using System;
using Connecto.Common.Enumeration;

namespace Connecto.BusinessObjects
{
    public class WriteoffDetailCart : ProductBase
    {
        public int WriteoffDetailId { get; set; }
        public Guid WriteoffDetailGuid { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int ProductDetailId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Price { get; set; }
        public decimal NetPrice { get; set; }
        public int EmployeeId { get; set; }
        public int WriteoffId { get; set; }
        public DateTime DateWriteoff { get; set; }
    }
}
