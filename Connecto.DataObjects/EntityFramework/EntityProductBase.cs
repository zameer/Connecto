using Connecto.Common.Enumeration;
using System;

namespace Connecto.DataObjects.EntityFramework
{
    public class EntityProductBase : EntityConnecto
    {
        public int Quantity { get; set; }
        public int QuantityActual { get; set; }
        public int QuantityLower { get; set; }
        public DiscountType DiscountType { get; set; }
        public double DiscountRate { get; set; }
        public decimal DiscountAmount { get; set; }
    }
}
