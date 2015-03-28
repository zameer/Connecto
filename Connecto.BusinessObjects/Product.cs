using System;

namespace Connecto.BusinessObjects
{
    public class Product : ProductBase
    {
        public int ProductId { get; set; }
        public Guid ProductGuid { get; set; }
        public int ProductTypeId { get; set; }
        public int VendorId { get; set; }
        public string Name { get; set; }
        public int StockInHand { get; set; }
        public decimal ContainsQty { get; set; }
        public int Reorderlevel { get; set; }
        public virtual ProductType ProductType { get; set; }
        public virtual Vendor Vendor { get; set; }
    }
}
