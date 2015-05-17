using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Connecto.App.BusinessIntelligence;
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
            return View(new ReportCriteriaViewModel { Page = "ProductDetails", ReportType = ReportType, CommandText = "[Product].[GetProductDetail]", ReportName = "ProductDetails.rdlc" });
        }
        public ActionResult SalesDetails()
        {
            return View(new ReportCriteriaViewModel { Page = "SalesDetails", ReportType = ReportType, CommandText = "[Product].[GetSalesDetailsByOrderId]", ReportName = "SalesDetailsByOrderId.rdlc" });
        }
        public ActionResult ProductData()
        {
            return View(new ReportCriteriaViewModel { Page = "ProductData", ReportType = ReportType });
        }

        [HttpPost]
        public ActionResult Download(ReportCriteriaViewModel vm)
        {
            vm.ReportPath = Path.Combine(Server.MapPath("~/BusinessIntelligence/Transaction"), vm.ReportName); ;
            var info = new PrintoDeviceInfo { OutputFormat = vm.ReportType, SizeUnit = "in", PageWidth = 8.5, PageHeight = 11, MarginTop = 0.5, MarginLeft = 1, MarginRight = 1, MarginBottom = 0.5 };
            var lr = vm.Page.Equals("ProductDetails") ? Reporto.Execute(vm) :
                vm.Page.Equals("ProductData") ? GetProductData(ref info) : Reporto.Execute(vm);
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