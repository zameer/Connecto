using System.Collections.Generic;
using Connecto.App.Models;
using Connecto.App.ModelValidator;
using Connecto.BusinessObjects;
using Connecto.Common.Enumeration;
using Connecto.Repositories;
using System;
using System.Web.Mvc;

namespace Connecto.App.Controllers
{
    public class ReturnReasonController : Controller
    {
        private readonly ReturnReasonRepository _repo = ConnectoFactory.ReturnReasonRepository;
        public JsonResult GetSearch(FilterCriteria criteria)
        {
            var items = _repo.GetAllSearch(criteria);
            return Json(new { recordsTotal = items.Item2, recordsFiltered = items.Item2, data = items.Item1 }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Get()
        {
            var items = _repo.GetAll();
            return Json(items, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetItem(int id)
        {
            var item = _repo.GetReturnReasonById(id);
            return Json(item, JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /ReturnReason/Create
        [HttpPost]
        public JsonResult Create(ReturnReason item)
        {
            var errors = new ReturnReasonValidator(item, _repo).Validate();
            if (errors.Count > 0) return Json(new ConnectoValidation { Status = "Failure", Exceptions = errors }, JsonRequestBehavior.AllowGet);

            item.LocationId = 1;
            item.ReturnReasonGuid = Guid.NewGuid();
            item.CreatedBy = User.UserId();
            item.CreatedOn = DateTime.Now;
            item.Status = RecordStatus.Active;
            _repo.Add(item);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /ReturnReason/Edit/5
        [HttpPost]
        public ActionResult Edit(ReturnReason item)
        {
            var errors = new ReturnReasonValidator(item, _repo).Validate();
            if (errors.Count > 0) return Json(new ConnectoValidation { Status = "Failure", Exceptions = errors }, JsonRequestBehavior.AllowGet);
            
            item.EditedBy = User.UserId();
            item.EditedOn = DateTime.Now;
            _repo.Edit(item);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /ReturnReason/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var errors = new ReturnReasonValidator(_repo).Validate(id);
            if (errors.Count > 0) return Json(new ConnectoValidation { Status = "Failure", Exceptions = errors }, JsonRequestBehavior.AllowGet);

            _repo.Delete(id, User.UserId());
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /ReturnReason/
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
    }
}
