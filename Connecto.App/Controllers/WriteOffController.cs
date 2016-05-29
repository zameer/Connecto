using System.IO;
using System.Linq;
using Connecto.App.BusinessIntelligence.Dataset.TransactionsTableAdapters;
using Connecto.App.Models;
using Connecto.App.Utilities;
using Connecto.BusinessObjects;
using Connecto.Repositories;
using System;
using System.Web.Mvc;
using Connecto.App.ModelValidator;
using Microsoft.Reporting.WebForms;

namespace Connecto.App.Controllers
{
    public class WriteOffController : BaseController
    {
        private readonly WriteoffDetailRepository _repo = ConnectoFactory.WriteoffDetailRepository;

        public JsonResult GetWriteoffDetailList(FilterCriteria criteria)
        {
            var items = _repo.GetDetailList(criteria);
            return Json(new { recordsTotal = items.Item2, recordsFiltered = items.Item2, data = items.Item1 }, JsonRequestBehavior.AllowGet);
        }



        public JsonResult GetWriteoffs()
        {
            var items = _repo.GetWriteoffs(false);
            return Json(items, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Get(int writeoffId)
        {
            var items = _repo.GetAll(writeoffId);
            return Json(items, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetWriteoffDetail(string productCode)
        {
            var item = _repo.GetWriteoffDetail(productCode);
            return Json(item, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCart(int id)
        {
            var item = _repo.GetCart(id);
            return Json(item, JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Transaction/Create
        [HttpPost]
        public JsonResult Create(WriteoffDetailCart item)
        {
            var errors = new WriteoffDetailValidator(item, _repo).Validate();
            if (errors.Count > 0) return Json(new ConnectoValidation { Status = "Failure", Exceptions = errors }, JsonRequestBehavior.AllowGet);

            item.LocationId = Location.LocationId;
            item.CreatedBy = Location.UserId;
            item.DateWriteoff = item.DateWriteoff.Year == 1 ? DateTime.Now : item.DateWriteoff;
            var WriteoffId = _repo.AddToCart(item);
            return Json(new { WriteoffId = WriteoffId, Status = "Success", Message = "Writeoff Cart Item Added." }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EditHeader(WriteoffDetailCart item)
        {
            _repo.UpdateWriteoff(item);
            return Json(new { Status = "Success", Message = "Writeoff Cart Item successfully Added." }, JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Transaction/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            _repo.Delete(id, Location.UserId);
            return Json(new { Status = "Success", Message = "Writeoff Cart Item Successfully Deleted." }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteWriteoffDetail(int id)
        {
            _repo.DeleteWriteoffDetail(id, Location.UserId);
            return Json(new { Status = "Success", Message = "Writeoff Detail Successfully Deleted." }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Complete(int id)
        {
            _repo.Add(id);
            return Json(new { Status = "Success", Message = "Writeoff Successfully updated." }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Print(int id)
        {
            var path = Path.Combine(Server.MapPath("~/BusinessIntelligence/Transaction"), "SalesInvoice.rdlc");
            if (!System.IO.File.Exists(path))
                return Json(new { Status = "Failure", Message = "Report not found." }, JsonRequestBehavior.AllowGet);

            var data = new InvoiceByIdAdapter().GetData(id).ToList();
            var rd = new ReportDataSource("Dataset", data);

            var lr = new LocalReport { ReportPath = path };

            var rptParams = new[]
            {
                new ReportParameter("CompanyName", Location.CompanyName),
                new ReportParameter("LocationName", Location.LocationName),
                new ReportParameter("Address", Location.Address),
                new ReportParameter("UserName", Location.DisplayName),
                new ReportParameter("Contact", Location.Contact),
                new ReportParameter("WriteoffId", id.ToString("")),
                new ReportParameter("PrintDate", DateTime.Now.ToString("g")),
            };
            lr.SetParameters(rptParams);

            lr.DataSources.Add(rd);

            var info = new PrintoDeviceInfo { OutputFormat = "EMF", SizeUnit = "mm", PageWidth = 76, PageHeight = 100, MarginTop = 0.5, MarginLeft = 0, MarginRight = 0, MarginBottom = 0.5 };
            Printo.Printer(lr, info.Xml, Location.PrinterName);
            _repo.Add(id);
            return Json(new { Status = "Success", Message = "Writeoff Successfully updated." }, JsonRequestBehavior.AllowGet);
        }


        // GET: /Writeoff/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Checkout()
        {
            return View();
        }
        public ActionResult List()
        {
            return View();
        }
        public ActionResult Edit()
        {
            return View();
        }
        public ActionResult WriteoffDetails()
        {
            return View();
        }
    }
}
