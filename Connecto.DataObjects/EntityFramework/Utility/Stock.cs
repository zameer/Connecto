using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Connecto.BusinessObjects;

namespace Connecto.DataObjects.EntityFramework.Utility
{
    public static class Stock
    {
        public static void SyncStock(EntityProductDetail item, int quantity, int quantityActual, int quantityLower)
        {
            var volume = item.Product.ProductType.Measure.Volume;
            var containsQty = item.Product.ContainsQty;
            var stock = new ProductBase { Quantity = item.Quantity, QuantityActual = item.QuantityActual, QuantityLower = item.QuantityLower };
            var sold = new ProductBase { Quantity = quantity, QuantityActual = quantityActual, QuantityLower = quantityLower };
            var synced = SyncStock(volume, (int)containsQty, stock, sold);
            item.Quantity = synced.Quantity;
            item.QuantityActual = synced.QuantityActual;
            item.QuantityLower = synced.QuantityLower;

            stock = new ProductBase { Quantity = item.Product.Quantity, QuantityActual = item.Product.QuantityActual, QuantityLower = item.Product.QuantityLower };
            synced = SyncStock(volume, (int)containsQty, stock, sold);
            var qty = item.Product.Quantity;
            item.Product.Quantity = synced.Quantity;
            item.Product.QuantityActual = synced.QuantityActual;
            item.Product.QuantityLower = synced.QuantityLower;
            item.Product.StockInHand -= (qty - synced.Quantity);
        }

        public static ProductBase SyncStock(int volume, int containsQty, ProductBase stock, ProductBase sold)
        {
            var actByLwr = sold.QuantityLower / volume;
            var soldLower = actByLwr > 0 ? (actByLwr * volume) - sold.QuantityLower : stock.QuantityLower - sold.QuantityLower;

            var nxt = soldLower < 0 ? new ProductBase { QuantityLower = volume + soldLower, QuantityActual = stock.QuantityActual - (actByLwr + 1) }
                : new ProductBase { QuantityLower = soldLower, QuantityActual = stock.QuantityActual };
            nxt.Quantity = stock.Quantity;

            var diffQty = sold.QuantityActual - nxt.QuantityActual;
            if (diffQty > 0)
            {
                nxt.QuantityActual = containsQty - diffQty;
                nxt.Quantity = stock.Quantity - 1;
            }
            else nxt.QuantityActual -= sold.QuantityActual;

            nxt.Quantity -= sold.Quantity;
            return nxt;
        }
    }
}
