using Connecto.BusinessObjects;
using Connecto.Common.Enumeration;
using Connecto.Repositories;
using System;
using System.Web.Mvc;

namespace Connecto.App.Controllers
{
    public class VendorController : Controller
    {
        private readonly VendorRepository _vendor = ConnectoFactory.VendorRepository;
        //
        // GET: /Vendor/

        public ActionResult Index()
        {
            var vendor = _vendor.GetAll();
            return View(vendor);
        }

        //
        // GET: /Vendor/Details/5

        public ActionResult Details(int id)
        {
            var vendor = _vendor.GetVendorById(id);
            return View(vendor);
        }

        //
        // GET: /Vendor/Create

        public ActionResult Create()
        {
            return View(new Vendor());
        }

        //
        // POST: /Vendor/Create

        [HttpPost]
        public ActionResult Create(Vendor vendor)
        {
            try
            {
                vendor.VendorId = 1;
                vendor.VendorGuid = Guid.NewGuid();
                vendor.CreatedBy = 1;
                vendor.CreatedOn = DateTime.Now;
                vendor.Status = RecordStatus.Active;
                _vendor.Add(vendor);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Vendor/Edit/5

        public ActionResult Edit(int id)
        {
            var vendor = _vendor.GetVendorById(id);
            return View(vendor);
        }

        //
        // POST: /Vendor/Edit/5

        [HttpPost]
        public ActionResult Edit(Vendor vendor)
        {
            try
            {
                vendor.EditedBy = 1;
                vendor.EditedOn = DateTime.Now;
                _vendor.Edit(vendor);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Vendor/Delete/5

        public ActionResult Delete(int id)
        {
            var vendor = _vendor.GetVendorById(id);
            return View(vendor);
        }

        //
        // POST: /Vendor/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _vendor.Delete(id, 3);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
