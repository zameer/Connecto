using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Connecto.App.Models;
using Connecto.BusinessObjects;
using Connecto.Common.Enumeration;
using Connecto.Repositories;

namespace Connecto.App.Controllers
{
    public class ProductTypeController : Controller
    {
        private readonly ProductTypeRepository _repo = ConnectoFactory.ProductTypeRepository;
        public JsonResult Get()
        {
            var items = _repo.GetAll();
            return Json(items, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetItem(int id)
        {
            var item = _repo.GetProductTypeById(id);
            return Json(item, JsonRequestBehavior.AllowGet);
        }


        //
        // GET: /ProductType/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
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

        //
        // POST: /ProductType/Create
        [HttpPost]
        public JsonResult Create(ProductType item)
        {
            var errors = new List<ConnectoException>();
            if (item.MeasureId.Equals(0)) errors.Add(new ConnectoException { Message = "Please select measure" });
            if (string.IsNullOrEmpty(item.Type)) errors.Add(new ConnectoException { Message = "Please provide Type" });
            if (string.IsNullOrEmpty(item.StockAs)) errors.Add(new ConnectoException { Message = "Please provide Stock As" });
            if (errors.Count > 0) return Json(new ConnectoValidation { Status = "Failure", Exceptions = errors }, JsonRequestBehavior.AllowGet);

            item.LocationId = 1;
            item.ProductTypeGuid = Guid.NewGuid();
            item.CreatedBy = User.UserId();
            item.CreatedOn = DateTime.Now;
            item.Status = RecordStatus.Active;
            _repo.Add(item);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /ProductType/Edit/5
        [HttpPost]
        public ActionResult Edit(ProductType item)
        {
            item.EditedBy = User.UserId();
            item.EditedOn = DateTime.Now;
            _repo.Edit(item);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /ProductType/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            //_productType.(id, User.UserId());
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
