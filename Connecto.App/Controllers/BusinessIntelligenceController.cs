using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Connecto.App.BusinessIntelligence.Dataset;
using Connecto.App.BusinessIntelligence.Dataset.TransactionsTableAdapters;
using Connecto.App.Models;
using Connecto.App.Utilities;
using Microsoft.Ajax.Utilities;
using Microsoft.Reporting.WebForms;

namespace Connecto.App.Controllers
{
    public class BusinessIntelligenceController : Controller
    {
        private const string ReportType = "PDF";
        public ActionResult ProductDetails()
        {
            return View(new ReportCriteriaViewModel { Page = "ProductDetails", ReportType = ReportType });
        }
        public ActionResult SalesDetails()
        {
            return View(new ReportCriteriaViewModel { Page = "SalesDetails", ReportType = ReportType });
        }
        public ActionResult ProductData()
        {
            return View(new ReportCriteriaViewModel { Page = "ProductData", ReportType = ReportType });
        }

        [HttpPost]
        public ActionResult Download(ReportCriteriaViewModel vm)
        {
            var info = new PrintoDeviceInfo { OutputFormat = vm.ReportType, SizeUnit = "in", PageWidth = 8.5, PageHeight = 11, MarginTop = 0.5, MarginLeft = 1, MarginRight = 1, MarginBottom = 0.5 };
            var lr = vm.Page.Equals("ProductDetails") ? GetProductDetails() :
                vm.Page.Equals("ProductData") ? GetProductData(ref info) : GetSalesDetails(ref info, vm.Id);
            if (lr == null)
                return View(vm.Page);

            if (vm.ReportType.Equals("EMF"))
            {
                Printo.Printer(lr, info.Xml);
                return View(vm.Page);
            }

            var file = Printo.File(lr, info.Xml, vm.ReportType);
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
        private LocalReport GetSalesDetails(ref PrintoDeviceInfo info, int orderId)
        {
            var path = Path.Combine(Server.MapPath("~/BusinessIntelligence/Transaction"), "SalesDetailsByOrderId.rdlc");
            if (!System.IO.File.Exists(path)) return null;

            info = new PrintoDeviceInfo { OutputFormat = "EMF", SizeUnit = "in", PageWidth = 4.4, PageHeight = 0, MarginTop = 0, MarginLeft = 0, MarginRight = 0, MarginBottom = 0 };
            var data = new SalesDetailsAdapter().GetData(orderId).ToList();
            var rd = new ReportDataSource("Dataset", data);
            var lr = new LocalReport { ReportPath = path };
            lr.SetParameters(new[] { new ReportParameter("OrderId", orderId.ToString()) });
            lr.DataSources.Add(rd);
            return lr;
        }

        private LocalReport GetProductData(ref PrintoDeviceInfo info)
        {
            var path = Path.Combine(Server.MapPath("~/BusinessIntelligence/Transaction"), "ProductData.rdlc");
            if (!System.IO.File.Exists(path)) return null;

            var data = new ProductDataAdapter().GetData().ToList();
            var rd = new ReportDataSource("Dataset", data);
            var lr = new LocalReport { ReportPath = path };
            lr.DataSources.Add(rd);
            return lr;
        }
    }
}