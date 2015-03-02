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
        private readonly MeasureRepository _measure = ConnectoFactory.MeasureRepository;
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
        public ActionResult Details()
        {
            return View();
        }
        public ActionResult Edit()
        {
            return View();
        }
        public JsonResult Get()
        {
            var measures = _measure.GetAll();
            return Json(measures, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetItem(int id)
        {
            var measure = _measure.GetMeasureById(id);
            return Json(measure, JsonRequestBehavior.AllowGet);
        }
        //
        // GET: /Measure/Details/5

        public ActionResult Details(int id)
        {
            var measure = _measure.GetMeasureById(id);
            return View(measure);
        }

        //
        // POST: /Measure/Create

        [HttpPost]
        public JsonResult Create(Measure measure)
        {
            measure.LocationId = 1;
            measure.MeasureGuid = Guid.NewGuid();
            measure.CreatedBy = User.UserId();
            measure.CreatedOn = DateTime.Now;
            measure.Status = RecordStatus.Active;
            _measure.Add(measure);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Measure/Edit/5

        [HttpPost]
        public ActionResult Edit(Measure measure)
        {
            measure.EditedBy = User.UserId();
            measure.EditedOn = DateTime.Now;
            _measure.Edit(measure);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Measure/Delete/5

        public ActionResult Delete(int id)
        {
            var measure = _measure.GetMeasureById(id);
            return View(measure);
        }

        //
        // POST: /Measure/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _measure.Delete(id, 3);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
