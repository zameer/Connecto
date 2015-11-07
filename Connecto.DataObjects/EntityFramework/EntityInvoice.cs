using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connecto.DataObjects.EntityFramework
{
    [Table("Invoice", Schema = "Product")]
    public class EntityInvoice : EntityConnecto
    {
        [Key]
        public int InvoiceId { get; set; }
        public Guid InvoiceGuid { get; set; }
        public int? CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal Fluctuation { get; set; }
        public string ReferenceCode { get; set; }
        public virtual EntityCustomer Customer { get; set; }
        public virtual EntityEmployee Employee { get; set; }
    }
}
