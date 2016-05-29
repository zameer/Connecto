using System;

namespace Connecto.BusinessObjects
{
    public class Writeoff : Connecto
    {
        public int WriteoffId { get; set; }
        public Guid WriteoffGuid { get; set; }
        public int EmployeeId { get; set; }
        public DateTime WriteoffDate { get; set; }
        public string WriteoffDateDisplay
        {
            get { return string.Format("{0:g}", WriteoffDate); }
        }
        public Employee Employee { get; set; }
    }
}
