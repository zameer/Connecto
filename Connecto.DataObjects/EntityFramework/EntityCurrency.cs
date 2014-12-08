using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connecto.DataObjects.EntityFramework
{
    [Table("Currency", Schema = "Connecto")]
    public class EntityCurrency : EntityConnecto
    {
        [Key]
        public int CurrencyId { get; set; }
        public Guid CurrencyGuid { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
    }
}
