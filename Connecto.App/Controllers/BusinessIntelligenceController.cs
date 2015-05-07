using System.IO;
using System.Linq;
using System.Web.Mvc;
using Connecto.App.BusinessIntelligence.Dataset.TransactionsTableAdapters;
using Connecto.App.Utilities;
using Microsoft.Reporting.WebForms;

namespace Connecto.App.Controllers
{
    public class BusinessIntelligenceController : Controller
    {
        public ActionResult Report()
        {
            return View();
        }
        public ActionResult Download(string id)
        {
            //var path = Path.Combine(Server.MapPath("~/BusinessIntelligence/Transaction"), "ProductDetails.rdlc");
            var path = Path.Combine(Server.MapPath("~/BusinessIntelligence/Transaction"), "SalesDetailsByOrderId.rdlc");
            if (!System.IO.File.Exists(path))
                return View("Report");

            var data = new SalesDetailsAdapter().GetData().ToList();//new ProductDetailsAdapter().GetData().ToList();
            var rd = new ReportDataSource("Dataset", data);
            var lr = new LocalReport { ReportPath = path };
            lr.DataSources.Add(rd);

            //var info = new PrintoDeviceInfo { OutputFormat = id, SizeUnit = "in", PageWidth = 8.5, PageHeight = 11, MarginTop = 0.5, MarginLeft = 1, MarginRight = 1, MarginBottom = 0.5};
            var info = new PrintoDeviceInfo { OutputFormat = "EMF", SizeUnit = "in", PageWidth = 4.4, PageHeight = 0, MarginTop = 0, MarginLeft = 0, MarginRight = 0, MarginBottom = 0 };
            if (id.Equals("EMF"))
            {
                Printo.Printer(lr, info.Xml);
                return View("Report");
            }

            var file = Printo.File(lr, info.Xml, id);
            return File(file.Item1, file.Item2);
        }
       
    }
}