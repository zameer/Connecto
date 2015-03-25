using System.Collections.Generic;
using Connecto.App.Models;
using Connecto.BusinessObjects;
using Connecto.Common.Enumeration;
using Connecto.Repositories;
using System;
using System.Web.Mvc;
using Connecto.App.ModelValidator;

namespace Connecto.App.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ProductDetailRepository _repo = ConnectoFactory.ProductDetailRepository;
        public JsonResult GetInvoices()
        {
            var items = _repo.GetInvoices();
            return Json(items, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Get(int invoiceId)
        {
            var items = _repo.GetAll(invoiceId);
            return Json(items, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCart(int id)
        {
            var item = _repo.GetCart(id);
            return Json(item, JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Transaction/Create
        [HttpPost]
        public JsonResult Create(ProductDetailCart item)
        {
            var errors = new ProductDetailValidator(item, _repo).Validate();
            if (errors.Count > 0) return Json(new ConnectoValidation { Status = "Failure", Exceptions = errors }, JsonRequestBehavior.AllowGet);

            item.LocationId = 1;
            item.ProductDetailGuid = Guid.NewGuid();
            item.CreatedBy = User.UserId();
            item.CreatedOn = DateTime.Now;
            item.DateReceived = DateTime.Now;
            item.Status = RecordStatus.Active;
            _repo.AddToCart(item);
            return Json(new { Status = "Success", Message = "Cart Item Added." }, JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Transaction/Edit/5
        [HttpPost]
        public ActionResult Edit(ProductDetailCart item)
        {
            item.EditedBy = User.UserId();
            item.EditedOn = DateTime.Now;
            _repo.EditCart(item);
            return Json(new { Status = "Success", Message = "Cart Item Updated." }, JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Transaction/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            _repo.Delete(id, User.UserId());
            return Json(new { Status = "Success", Message = "Person Successfully Deleted." }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Complete(int id)
        {
            _repo.Add(id);
            return Json(new { Status = "Success", Message = "Invoice Successfully Added." }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index()
        {
            return View();
        }
        //
        // GET: /Transaction/
        public ActionResult CartIn()
        {
            return View();
        }
    }
}
