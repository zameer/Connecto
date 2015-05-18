using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connecto.DataObjects.EntityFramework
{
    [Table("ReportSetting", Schema = "Connecto")]
    public class EntityReportSetting
    {
        [Key]
        public Guid ReportGuid { get; set; }
        public string ReportName { get; set; }
        public string ReportTitle { get; set; }
        public string CommandText { get; set; }
        public string ReportPath { get; set; }
        public string Parameters { get; set; }
    }
}
