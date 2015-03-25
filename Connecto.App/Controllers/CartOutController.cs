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
    public class CartOutController : Controller
    {
        private readonly SalesDetailRepository _repo = ConnectoFactory.SalesDetailRepository;
        public JsonResult GetOrders()
        {
            var items = _repo.GetOrders();
            return Json(items, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Get(int orderId)
        {
            var items = _repo.GetAll(orderId);
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

            item.LocationId = 1;
            item.SalesDetailGuid = Guid.NewGuid();
            item.CreatedBy = User.UserId();
            item.CreatedOn = DateTime.Now;
            item.DateSold = DateTime.Now;
            item.Status = RecordStatus.Active;
            _repo.AddToCart(item);
            return Json(new { Status = "Success", Message = "Cart Item Added." }, JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Transaction/Edit/5
        [HttpPost]
        public ActionResult Edit(SalesDetailCart item)
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
            //_repo.Add(id);
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
