using System;

namespace Connecto.BusinessObjects
{
    public class ProductBase : Connecto
    {
        public int Quantity { get; set; }
        public int QuantityActual { get; set; }
        public int QuantityLower { get; set; }
    }
}
