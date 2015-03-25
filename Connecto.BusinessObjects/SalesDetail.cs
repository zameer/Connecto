using System;
using System.Collections.Generic;

namespace Connecto.BusinessObjects
{
    public class SalesDetail : Connecto
    {
        public int SalesDetailId { get; set; }
        public Guid SalesDetailGuid { get; set; }
        public string ProductCode { get; set; }
        public int OrderId { get; set; }
        public int ProductDetailId { get; set; }
        public string Measure { get; set; }
        public List<string> Measures { get; set; }
        public int CustomerId { get; set; }
        public double UnitPrice { get; set; }
        public double SellingPrice { get; set; }
        public int Quantity { get; set; }
        public int StockInHand { get; set; }
        public DateTime DateSold { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
