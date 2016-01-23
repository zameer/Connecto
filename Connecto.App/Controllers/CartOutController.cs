using System.IO;
using System.Linq;
using Connecto.App.BusinessIntelligence.Dataset.TransactionsTableAdapters;
using Connecto.App.Utilities;
using Connecto.BusinessObjects;
using Connecto.Repositories;
using System;
using System.Web.Mvc;
using Connecto.App.ModelValidator;
using Microsoft.Reporting.WebForms;

namespace Connecto.App.Controllers
{
    public class CartOutController : BaseController
    {
        private readonly SalesDetailRepository _repo = ConnectoFactory.SalesDetailRepository;
        public JsonResult GetInvoices()
        {
            var items = _repo.GetInvoices(false);
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
            var item = _repo.GetCart(id);
            return Json(item, JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Transaction/Create
        [HttpPost]
        public JsonResult Create(SalesDetailCart item)
        {
            var errors = new SalesDetailValidator(item, _repo).Validate();
            if (errors.Count > 0) return Json(new ConnectoValidation { Status = "Failure", Exceptions = errors }, JsonRequestBehavior.AllowGet);

            item.LocationId = Location.LocationId;
            item.CreatedBy = Location.UserId;
            item.DateSold = item.DateSold.Year == 1 ? DateTime.Now : item.DateSold;
            var invoiceId = _repo.AddToCart(item);
            return Json(new { InvoiceId = invoiceId, Status = "Success", Message = "Cart Item Added." }, JsonRequestBehavior.AllowGet);
        }
       
        [HttpPost]
        public JsonResult EditHeader(SalesDetailCart item)
        {
            _repo.UpdateInvoice(item);
            return Json(new { Status = "Success", Message = "Order successfully updated." }, JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Transaction/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            _repo.Delete(id, Location.UserId);
            return Json(new { Status = "Success", Message = "Cart Item Successfully Deleted." }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Complete(int id, decimal fluctuation)
        {
            _repo.Add(id, fluctuation);
            return Json(new {Status = "Success", Message = "Invoice Successfully Added."}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Print(int id, decimal fluctuation, decimal paid, decimal balance)
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
                new ReportParameter("OrderId", id.ToString("")),
                new ReportParameter("PrintDate", DateTime.Now.ToString("g")),
                new ReportParameter("Paid", paid.ToString("")),
                new ReportParameter("Balance", balance.ToString(""))
            };
            lr.SetParameters(rptParams);
            
            lr.DataSources.Add(rd);

            var info = new PrintoDeviceInfo { OutputFormat = "EMF", SizeUnit = "mm", PageWidth = 76, PageHeight = 100, MarginTop = 0.5, MarginLeft = 0, MarginRight = 0, MarginBottom = 0.5 };
            Printo.Printer(lr, info.Xml, Location.PrinterName);
            _repo.Add(id, fluctuation);
            return Json(new { Status = "Success", Message = "Invoice Successfully Added." }, JsonRequestBehavior.AllowGet);
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
