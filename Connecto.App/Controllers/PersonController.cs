﻿using System.Collections.Generic;
using Connecto.App.Models;
using Connecto.BusinessObjects;
using Connecto.Common.Enumeration;
using Connecto.Repositories;
using System;
using System.Web.Mvc;

namespace Connecto.App.Controllers
{
    public class PersonController : Controller
    {
        private readonly PersonRepository _repo = ConnectoFactory.PersonRepository;
        public JsonResult Get()
        {
            var items = _repo.GetAll();
            return Json(items, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetItem(int id)
        {
            var item = _repo.GetVendorById(id);
            return Json(item, JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Person/Create
        [HttpPost]
        public JsonResult Create(Person item)
        {
            var errors = new List<ConnectoException>();
            if (string.IsNullOrEmpty(item.FirstName)) errors.Add(new ConnectoException { Message = "Please provide Name" });
            if (errors.Count > 0) return Json(new ConnectoValidation { Status = "Failure", Exceptions = errors }, JsonRequestBehavior.AllowGet);

            item.LocationId = 1;
            item.PersonGuid = Guid.NewGuid();
            item.CreatedBy = User.UserId();
            item.CreatedOn = DateTime.Now;
            item.Status = RecordStatus.Active;
            _repo.Add(item);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Person/Edit/5
        [HttpPost]
        public ActionResult Edit(Person item)
        {
            item.EditedBy = User.UserId();
            item.EditedOn = DateTime.Now;
            _repo.Edit(item);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Person/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            _repo.Delete(id, User.UserId());
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Person/
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
        public ActionResult Contacts()
        {
            return View();
        }
    }
}