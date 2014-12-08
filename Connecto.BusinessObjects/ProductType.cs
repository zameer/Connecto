using System;

namespace Connecto.BusinessObjects
{
    public class ProductType : Connecto
    {
        public int ProductTypeId { get; set; }
        public Guid ProductTypeGuid { get; set; }
        public int MeasureId { get; set; }
        public string Type { get; set; }
        public string StockAs { get; set; }
        public virtual Measure Measure { get; set; }
    }
}
