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
                supplier.Person = new Person
                {
                    FirstName = "Ahamed",
                    LastName = "Zameer",
                    PersonGuid = Guid.NewGuid(),
                    LocationId = 1,
                    Status = 1,
                    CreatedBy = 1,
                    CreatedOn = DateTime.Now
                };
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
            var supplier = _supplier.GetSupplierById(id);
            return View(supplier);
        }

        //
        // POST: /Supplier/Edit/5

        [HttpPost]
        public ActionResult Edit(Supplier supplier)
        {
            try
            {
                // TODO: Add update logic here
                supplier.EditedBy = 1;
                supplier.EditedOn = DateTime.Now;
                _supplier.Edit(supplier);

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
