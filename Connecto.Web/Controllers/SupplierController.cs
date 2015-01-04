using Connecto.BusinessObjects;
using Connecto.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Connecto.Web.Controllers
{
    public class SupplierController : Controller
    {
        private readonly SupplierRepository _supplier = ConnectoFactory.SupplierRepository;
        //
        // GET: /Supplier/

        public ActionResult Index()
        {
            var suppliers = _supplier.GetAll();
            return View(suppliers);
        }

        //
        // GET: /Supplier/Details/5

        public ActionResult Details(int id)
        {
            var supplier = _supplier.GetSupplierById(id);
            return View(supplier);
        }

        //
        // GET: /Supplier/Create

        public ActionResult Create()
        {
            return View(new Supplier());
        }

        //
        // POST: /Supplier/Create

        [HttpPost]
        public ActionResult Create(Supplier supplier)
        {
            try
            {
                // TODO: Add insert logic here
                supplier.SupplierGuid = Guid.NewGuid();
                supplier.LocationId = 1;
                supplier.Status = 1;
                supplier.CreatedBy = 1;
                supplier.CreatedOn = DateTime.Now;
                _supplier.Add(supplier);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Supplier/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Supplier/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Supplier/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Supplier/Delete/5

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
