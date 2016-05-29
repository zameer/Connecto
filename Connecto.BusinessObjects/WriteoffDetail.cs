using System;
using System.Collections.Generic;
using Connecto.Common.Enumeration;

namespace Connecto.BusinessObjects
{
    public class WriteoffDetail : ProductBase
    {
        public int WriteoffDetailId { get; set; }
        public Guid WriteoffDetailGuid { get; set; }
        public int WriteoffId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int OrderId { get; set; }
        public int ProductDetailId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Price { get; set; }
        public decimal NetPrice { get; set; }
        public DateTime DateWriteoff { get; set; }
        public String DisplayDate { get; set; }
        public string StockAs { get; set; }
        public string ReceivedInfo { get; set; }
        public Measure Measure { get; set; }
        public int Volume { get; set; }
        public string Actual { get; set; }
        public string Lower { get; set; }
        public StockInHand StockInHand { get; set; }
        public StockInHand RowStockInHand { get; set; }
        public bool SellingLower { get; set; }
        public decimal ContainsQty { get; set; }
        public int ItemCount { get; set; }
        public decimal GrossPrice { get; set; }
    }
}
