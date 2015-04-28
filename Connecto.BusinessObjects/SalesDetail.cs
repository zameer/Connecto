using System;
using System.Collections.Generic;
using Connecto.Common.Enumeration;

namespace Connecto.BusinessObjects
{
    public class SalesDetail : ProductBase
    {
        public int SalesDetailId { get; set; }
        public Guid SalesDetailGuid { get; set; }
        public string ProductCode { get; set; }
        public int OrderId { get; set; }
        public int ProductDetailId { get; set; }
        public int? CustomerId { get; set; }
        public string StockAs { get; set; }
        public bool SellingLower { get; set; }
        public int Volume { get; set; }
        public decimal ContainsQty { get; set; }
        public Measure Measure { get; set; }
        public StockInHand StockInHand { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public bool SellingMargin { get; set; }
        public decimal MarginAmount { get; set; }
        public decimal SellingPriceActual { get; set; }
        public DateTime DateSold { get; set; }
        public DiscountBy DiscountBy { get; set; }
        public double DiscountAs { get; set; }
        public decimal Discount { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
