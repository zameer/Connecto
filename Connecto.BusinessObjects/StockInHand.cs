using System;

namespace Connecto.BusinessObjects
{
    public class StockInHand 
    {
        public int Quantity { get; set; }
        public int QuantityActual { get; set; }
        public int QuantityLower { get; set; }
    }
}
