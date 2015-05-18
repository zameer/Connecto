using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Connecto.App.Models
{
    public class ReportCriteriaViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public string ReportPath { get; set; }
        public string ReportName { get; set; }
        public string ReportType { get; set; }
        public string Page { get; set; }

        public string CommandText { get; set; }
        public List<string> ReportTypes {
            get { return new List<string> { "PDF", "Excel", "Word", "Image" }; }
        }

        public string[] RenderControls { get; set; }
        public string ReportTitle { get; set; }
    }
}
