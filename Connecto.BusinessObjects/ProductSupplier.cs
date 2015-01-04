using System;

namespace Connecto.BusinessObjects
{
    public class ProductSupplier : Connecto
    {
        public int ProductSupplierId { get; set; }
        public Guid ProductSupplierGuid { get; set; }
        public int ProductId { get; set; }
        public int SupplierId { get; set; }

    }
}
