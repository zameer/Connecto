using System;

namespace Connecto.BusinessObjects
{
    public class Product : Connecto
    {
        public int ProductId { get; set; }
        public Guid ProductGuid { get; set; }
        public int ProductTypeId { get; set; }
        public int VendorId { get; set; }
        public string Name { get; set; }
        public int StockInHand { get; set; }
        public int Quantity { get; set; }
        public int Reorderlevel { get; set; }
        public virtual ProductType ProductType { get; set; }
        public virtual Vendor Vendor { get; set; }
    }
}
