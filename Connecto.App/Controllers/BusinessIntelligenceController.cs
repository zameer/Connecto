using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Connecto.App.BusinessIntelligence.Dataset;
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
            var path = Path.Combine(Server.MapPath("~/BusinessIntelligence/Transaction"), "ProductDetails.rdlc");
            if (!System.IO.File.Exists(path))
                return View("Report");

            var data = new ProductDetailsAdapter().GetData().ToList();
            var rd = new ReportDataSource("Dataset", data);
            var lr = new LocalReport { ReportPath = path };
            lr.DataSources.Add(rd);

            var deviceInfo =
            "<DeviceInfo>" +
            "  <OutputFormat>" + id + "</OutputFormat>" +
            "  <PageWidth>8.5in</PageWidth>" +
            "  <PageHeight>11in</PageHeight>" +
            "  <MarginTop>0.5in</MarginTop>" +
            "  <MarginLeft>1in</MarginLeft>" +
            "  <MarginRight>1in</MarginRight>" +
            "  <MarginBottom>0.5in</MarginBottom>" +
            "</DeviceInfo>";

            if (id.Equals("EMF"))
            {
                Printo.Printer(lr, deviceInfo);
                return View("Report");
            }

            var info = Printo.File(lr, deviceInfo, id);
            return File(info.Item1, info.Item2);
        }
       
    }
}