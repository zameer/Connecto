using System;

namespace Connecto.BusinessObjects
{
    public class Measure : Connecto
    {
        public int MeasureId { get; set; }
        public Guid MeasureGuid { get; set; }
        public string Lower { get; set; }
        public int Volume { get; set; }
        public string Actual { get; set; }
    }
}
