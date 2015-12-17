using System.Collections.Generic;
using Connecto.BusinessObjects;
using Connecto.Common.Enumeration;
using Connecto.Repositories;
using System;
using System.Web.Mvc;

namespace Connecto.App.Controllers
{
    public class ContactController : BaseController
    {
        private readonly ContactRepository _repo = ConnectoFactory.ContactRepository;

        public JsonResult Get(int id)
        {
            var items = _repo.GetAll(id);
            return Json(items, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetItem(int id)
        {
            var item = _repo.GetContact(id);
            return Json(item, JsonRequestBehavior.AllowGet);
        }
        //
        // POST: /Contact/Create
        [HttpPost]
        public JsonResult Create(Contact item)
        {
            var errors = new List<ConnectoException>();
            if (string.IsNullOrEmpty(item.LandNumber)) errors.Add(new ConnectoException { Message = "Please provide Land Number" });
            if (errors.Count > 0) return Json(new ConnectoValidation { Status = "Failure", Exceptions = errors }, JsonRequestBehavior.AllowGet);

            item.LocationId = 1;
            item.ContactGuid = Guid.NewGuid();
            item.CreatedBy = Location.UserId;
            item.CreatedOn = DateTime.Now;
            item.Status = RecordStatus.Active;
            _repo.Add(item);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        //
        // POST: /Contact/Edit/5
        [HttpPost]
        public ActionResult Edit(Contact item)
        {
            item.EditedBy = Location.UserId;
            item.EditedOn = DateTime.Now;
            _repo.Edit(item);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        //
        // POST: /Contact/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            _repo.Delete(id, Location.UserId);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
