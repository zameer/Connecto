using System;

namespace Connecto.BusinessObjects
{
    public class Supplier : Connecto
    {
        public int SupplierId { get; set; }
        public Guid SupplierGuid { get; set; }
        public string Name { get; set; }
    }
}
