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
    public class CustomerReturnController : BaseController
    {
        private readonly CustomerReturnRepository _repo = ConnectoFactory.CustomerReturnRepository;

        public JsonResult Get()
        {
            var items = _repo.Get();
            return Json(items, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSalesDetailByInvoiceId(int invoiceId)
        {
            var item = _repo.GetSalesDetailByInvoiceId(invoiceId);
            return Json(item, JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Transaction/Create
        [HttpPost]
        public JsonResult Create(ReturnProduct item)
        {
            //var errors = new SalesDetailValidator(item, _repo).Validate();
            //if (errors.Count > 0) return Json(new ConnectoValidation { Status = "Failure", Exceptions = errors }, JsonRequestBehavior.AllowGet);

            item.LocationId = 1;
            item.CustomerReturnGuid = Guid.NewGuid();
            item.CreatedBy = User.UserId();
            item.CreatedOn = DateTime.Now;
            item.DateReturned = DateTime.Now;
            item.Status = RecordStatus.Active;
            var orderId = _repo.ReturnProduct(item);
            return Json(new { OrderId = 1, Status = "Success", Message = "Cart Item Added." }, JsonRequestBehavior.AllowGet);
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
