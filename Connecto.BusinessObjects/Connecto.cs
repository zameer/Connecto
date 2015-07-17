using Connecto.Common.Enumeration;
using System;

namespace Connecto.BusinessObjects
{
    public class Connecto
    {
        public int LocationId { get; set; }
        public RecordStatus Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedOnText { get; set; }
        public int? EditedBy { get; set; }
        public DateTime? EditedOn { get; set; }
        public int Total { get; set; }
    }
}
