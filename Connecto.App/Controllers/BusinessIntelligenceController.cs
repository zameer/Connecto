using System;
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
        public ActionResult ProductDetails()
        {
            return View();
        }
        public ActionResult SalesDetails()
        {
            return View();
        }
        public ActionResult Download(string id, string page)
        {
            var info = new PrintoDeviceInfo { OutputFormat = id, SizeUnit = "in", PageWidth = 8.5, PageHeight = 11, MarginTop = 0.5, MarginLeft = 1, MarginRight = 1, MarginBottom = 0.5 };
            var lr = page.Equals("ProductDetails") ? GetProductDetails() : GetSalesDetails(ref info);
            if(lr == null)
                return View(page);

            if (id.Equals("EMF"))
            {
                Printo.Printer(lr, info.Xml);
                return View(page);
            }

            var file = Printo.File(lr, info.Xml, id);
            return File(file.Item1, file.Item2);
        }

        private LocalReport GetProductDetails()
        {
            var path = Path.Combine(Server.MapPath("~/BusinessIntelligence/Transaction"), "ProductDetails.rdlc");
            if (!System.IO.File.Exists(path)) return null;

            var data = new ProductDetailsAdapter().GetData().ToList();
            var rd = new ReportDataSource("Dataset", data);
            var lr = new LocalReport { ReportPath = path };
            lr.DataSources.Add(rd);
            return lr;
        }
        private LocalReport GetSalesDetails(ref PrintoDeviceInfo info)
        {
            var path = Path.Combine(Server.MapPath("~/BusinessIntelligence/Transaction"), "SalesDetailsByOrderId.rdlc");
            if (!System.IO.File.Exists(path)) return null;

            info = new PrintoDeviceInfo { OutputFormat = "EMF", SizeUnit = "in", PageWidth = 4.4, PageHeight = 0, MarginTop = 0, MarginLeft = 0, MarginRight = 0, MarginBottom = 0 };
            var data = new SalesDetailsAdapter().GetData().ToList();
            var rd = new ReportDataSource("Dataset", data);
            var lr = new LocalReport { ReportPath = path };
            lr.DataSources.Add(rd);
            return lr;
        }
    }
}