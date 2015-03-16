using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Connecto.Common.Enumeration;

namespace Connecto.DataObjects.EntityFramework
{
    [Table("Invoice", Schema = "Product")]
    public class EntityInvoice : EntityConnecto
    {
        [Key]
        public int InvoiceId { get; set; }
        public Guid InvoiceGuid { get; set; }
        public InvoiceType InvoiceType { get; set; }
        public int ProductDetailId { get; set; }
    }
}
