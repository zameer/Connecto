using Connecto.BusinessObjects;
using Connecto.Repositories;
using System;
using System.Web.Mvc;
using Connecto.App.ModelValidator;

namespace Connecto.App.Controllers
{
    public class CartInController : BaseController
    {
        private readonly ProductDetailRepository _repo = ConnectoFactory.ProductDetailRepository;
        public JsonResult GetOrders()
        {
            var items = _repo.GetOrders();
            return Json(items, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProductCodes()
        {
            var items = _repo.GetProductCodes(Location.LocationId);
           return Json(items, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Get(int orderId)
        {
            var items = _repo.GetAll(orderId);
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

            item.LocationId = Location.LocationId;
            item.CreatedBy = Location.UserId;
            item.DateReceived = item.DateReceived.Year == 1 ? DateTime.Now : item.DateReceived;
            var orderId = _repo.AddToCart(item);
            return Json(new { OrderId = orderId, Status = "Success", Message = "Cart Item Added." }, JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Transaction/Edit/5
        [HttpPost]
        public ActionResult Edit(ProductDetailCart item)
        {
            item.EditedBy = Location.UserId;
            item.EditedOn = DateTime.Now;
            _repo.EditCart(item);
            return Json(new { Status = "Success", Message = "Cart Item Updated." }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EditHeader(ProductDetailCart item)
        {
            _repo.UpdateOrder(item);
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
        // GET: /CartIn/
        public ActionResult Checkout()
        {
            return View();
        }
    }
}
