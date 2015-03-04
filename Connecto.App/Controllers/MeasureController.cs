using Connecto.App.Models;
using Connecto.BusinessObjects;
using Connecto.Common.Enumeration;
using Connecto.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Connecto.App.Controllers
{
    public class MeasureController : Controller
    {
        private readonly MeasureRepository _repo = ConnectoFactory.MeasureRepository;
        //
        // GET: /Measure/

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
        public JsonResult Get()
        {
            var items = _repo.GetAll();
            return Json(items, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetItem(int id)
        {
            var item = _repo.GetMeasureById(id);
            return Json(item, JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Measure/Create

        [HttpPost]
        public JsonResult Create(Measure item)
        {
            var errors = new List<ConnectoException>();
            if (string.IsNullOrEmpty(item.Lower)) errors.Add(new ConnectoException { Message = "Please provide lower" });
            if (item.Volume == 0) errors.Add(new ConnectoException { Message = "Please provide Volume" });
            if (string.IsNullOrEmpty(item.Actual)) errors.Add(new ConnectoException { Message = "Please provide Actual" });
            if (errors.Count > 0) return Json(new ConnectoValidation{ Status = "Failure", Exceptions = errors}, JsonRequestBehavior.AllowGet);


            item.LocationId = 1;
            item.MeasureGuid = Guid.NewGuid();
            item.CreatedBy = User.UserId();
            item.CreatedOn = DateTime.Now;
            item.Status = RecordStatus.Active;
            _repo.Add(item);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Measure/Edit/5

        [HttpPost]
        public ActionResult Edit(Measure item)
        {
            item.EditedBy = User.UserId();
            item.EditedOn = DateTime.Now;
            _repo.Edit(item);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Measure/Delete/5

        [HttpPost]
        public ActionResult Delete(int id)
        {
            _repo.Delete(id, User.UserId());
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
