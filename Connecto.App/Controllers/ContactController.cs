using System.Collections.Generic;
using Connecto.App.Models;
using Connecto.BusinessObjects;
using Connecto.Common.Enumeration;
using Connecto.Repositories;
using System;
using System.Web.Mvc;

namespace Connecto.App.Controllers
{
    public class ContactController : Controller
    {
        private readonly ContactRepository _repo = ConnectoFactory.ContactRepository;

        public JsonResult Get(int id)
        {
            var items = _repo.GetAll();
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
            item.CreatedBy = User.UserId();
            item.CreatedOn = DateTime.Now;
            item.Status = RecordStatus.Active;
            _repo.Add(item);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        //
        // POST: /Person/Edit/5
        [HttpPost]
        public ActionResult Edit(Contact item)
        {
            item.EditedBy = User.UserId();
            item.EditedOn = DateTime.Now;
            _repo.Edit(item);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
