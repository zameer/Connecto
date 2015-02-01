using Connecto.BusinessObjects;
using Connecto.Common.Enumeration;
using Connecto.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Connecto.Web.Controllers
{
    public class MeasureController : Controller
    {
        private readonly MeasureRepository _measure = ConnectoFactory.MeasureRepository;
        //
        // GET: /Measure/

        public ActionResult Index()
        {
            var measures = _measure.GetAll();
            return View(measures);
        }

        //
        // GET: /Measure/Details/5

        public ActionResult Details(int id)
        {
            var measure = _measure.GetMeasureById(id);
            return View(measure);
        }

        //
        // GET: /Measure/Create

        public ActionResult Create()
        {
            return View(new Measure());
        }

        //
        // POST: /Measure/Create

        [HttpPost]
        public ActionResult Create(Measure measure)
        {
            try
            {
                // TODO: Add insert logic here
                measure.CreatedBy = 1;
                measure.CreatedOn = DateTime.Now;
                measure.Status = RecordStatus.Active;
                _measure.Add(measure);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Measure/Edit/5

        public ActionResult Edit(int id)
        {
            var measure = _measure.GetMeasureById(id);
            return View(measure);
        }

        //
        // POST: /Measure/Edit/5

        [HttpPost]
        public ActionResult Edit(Measure measure)
        {
            try
            {
                // TODO: Add update logic here
                //supplier.EditedBy = 1;
                //supplier.EditedOn = DateTime.Now;
                //_measure.Edit(measure);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Measure/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Measure/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
