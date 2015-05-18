using System;

namespace Connecto.BusinessObjects
{
    public class ReportSetting
    {
        public Guid ReportGuid { get; set; }
        public string ReportName { get; set; }
        public string ReportTitle { get; set; }
        public string CommandText { get; set; }
        public string ReportPath { get; set; }
        public string Parameters { get; set; }
    }
}
