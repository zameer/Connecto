using System;
using System.IO;
using System.Web.Mvc;
using Connecto.App.BusinessIntelligence;
using Connecto.App.Models;
using Connecto.App.Utilities;
using Connecto.Repositories;

namespace Connecto.App.Controllers
{
    public class BusinessIntelligenceController : BaseController
    {
        private readonly CompanyRepository _repo = ConnectoFactory.CompanyRepository;
        private const string ReportType = "PDF";
        
        public ActionResult Load(Guid id)
        {
            var item = _repo.GetReportSetting(id);
            if (item == null) return View(new ReportCriteriaViewModel());

            return View(new ReportCriteriaViewModel { ReportType = ReportType, CommandText = item.CommandText, ReportName = item.ReportPath, 
                RenderControls = Reporto.GetRenderControls(item.Parameters), ReportTitle = item.ReportTitle});
        }

        [HttpPost]
        public ActionResult Download(ReportCriteriaViewModel vm)
        {
            vm.ReportPath = Path.Combine(Server.MapPath("~/BusinessIntelligence/"), vm.ReportName); ;
            var info = new PrintoDeviceInfo { OutputFormat = vm.ReportType, SizeUnit = "in", PageWidth = 8.5, PageHeight = 11, MarginTop = 0.5, MarginLeft = 1, MarginRight = 1, MarginBottom = 0.5 };
            var lr = Reporto.Execute(vm);
            if (lr == null) return View(vm.Page);

            if (vm.ReportType.Equals("EMF"))
            {
                Printo.Printer(lr, info.Xml, Location.PrinterName);
                return View(vm.Page);
            }

            var file = Printo.File(lr, info.Xml, vm.ReportType);
            return File(file.Item1, file.Item2);
        }
        public ActionResult Transactions()
        {
            return View();
        }
        public ActionResult HumanResource()
        {
            return View();
        }
        public ActionResult CommonSettings()
        {
            return View();
        }
    }
}