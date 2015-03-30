using System;
using System.Collections.Generic;

namespace Connecto.BusinessObjects
{
    public class SalesDetail : ProductBase
    {
        public int SalesDetailId { get; set; }
        public Guid SalesDetailGuid { get; set; }
        public string ProductCode { get; set; }
        public int OrderId { get; set; }
        public int ProductDetailId { get; set; }
        public string StockAs { get; set; }
        public bool SellingLower { get; set; }
        public string StockSummary { get; set; }
        public string Measure { get; set; }
        public int Volume { get; set; }
        public List<string> Measures { get; set; }
        public int CustomerId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public int StockInHand { get; set; }
        public DateTime DateSold { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
