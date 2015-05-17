using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Connecto.App.Models
{
    public class ReportCriteriaViewModel
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }

        public string ReportType { get; set; }
        public string Page { get; set; }
        public List<string> ReportTypes {
            get { return new List<string> { "PDF", "Excel", "Word", "Image" }; }
        }
    }
}
