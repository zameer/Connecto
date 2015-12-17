using System.Collections.Generic;
using System.IO;
using System.Linq;
using Connecto.App.BusinessIntelligence.Dataset.TransactionsTableAdapters;
using Connecto.App.Models;
using Connecto.App.Utilities;
using Connecto.BusinessObjects;
using Connecto.Common.Enumeration;
using Connecto.Repositories;
using System;
using System.Web.Mvc;
using Connecto.App.ModelValidator;
using Microsoft.Reporting.WebForms;

namespace Connecto.App.Controllers
{
    public class CartReturnController : BaseController
    {
        private readonly SalesDetailRepository _repo = ConnectoFactory.SalesDetailRepository;
        public JsonResult GetInvoices()
        {
            var items = _repo.GetInvoices(true);
            return Json(items, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Get(int invoiceId)
        {
            var items = _repo.GetAll(invoiceId);
            return Json(items, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSalesDetail(string productCode)
        {
            var item = _repo.GetSalesDetail(productCode);
            return Json(item, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCart(int id)
        {
            var item = _repo.GetSoldCart(id);
            return Json(item, JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Transaction/Create
        [HttpPost]
        public JsonResult Create(SalesDetailCart item)
        {
            var errors = new SalesDetailValidator(item, _repo).Validate();
            if (errors.Count > 0) return Json(new ConnectoValidation { Status = "Failure", Exceptions = errors }, JsonRequestBehavior.AllowGet);

            item.LocationId = 1;
            item.SalesDetailGuid = Guid.NewGuid();
            item.CreatedBy = Location.UserId;
            item.CreatedOn = DateTime.Now;
            item.DateSold = DateTime.Now;
            item.Status = RecordStatus.Active;
            _repo.ReturnCart(item);
            return Json(new { item.InvoiceId, Status = "Success", Message = "Return Updated." }, JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Transaction/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            _repo.Delete(id, Location.UserId);
            return Json(new { Status = "Success", Message = "Person Successfully Deleted." }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Complete(int id, decimal fluctuation)
        {
            return Json(new {Status = "Success", Message = "Invoice Successfully Added."}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Print(int invoiceId)
        {
            var path = Path.Combine(Server.MapPath("~/BusinessIntelligence/Transaction"), "SalesDetailsByOrderId.rdlc");
            if (!System.IO.File.Exists(path))
                return Json(new { Status = "Failure", Message = "Report not found." }, JsonRequestBehavior.AllowGet);

            var data = new SalesDetailsAdapter().GetData(DateTime.Now).ToList();
            var rd = new ReportDataSource("Dataset", data);
            var lr = new LocalReport { ReportPath = path };
            lr.DataSources.Add(rd);

            var info = new PrintoDeviceInfo { OutputFormat = "EMF", SizeUnit = "in", PageWidth = 5.3, PageHeight = 3, MarginTop = 0.5, MarginLeft = 0, MarginRight = 0, MarginBottom = 0.5 };
            Printo.Printer(lr, info.Xml);
            return Json(new { Status = "Success", Message = "Invoice Successfully Printed." }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index()
        {
            return View();
        }
        //
        // GET: /CartIn/
        public ActionResult Checkout()
        {
            return View();
        }
    }
}
