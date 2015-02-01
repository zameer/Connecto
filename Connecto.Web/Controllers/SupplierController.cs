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
    public class SupplierController : Controller
    {
        private readonly SupplierRepository _supplier = ConnectoFactory.SupplierRepository;
        private readonly ContactRepository _contact = ConnectoFactory.ContactRepository;
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

        public ActionResult Contacts(List<Contact> contacts)
        {
            return PartialView("_Contacts", contacts);
        }

        //
        // GET: /Supplier/Create

        public ActionResult Create()
        {
            return View(new Supplier());
        }
        public ActionResult CreateContact(int id)
        {
            return View(new Contact { PersonId = id });
        }

        [HttpPost]
        public ActionResult CreateContact(Contact contact)
        {
            contact.ContactGuid = Guid.NewGuid();
            contact.CreatedBy = 1;
            contact.CreatedOn = DateTime.Now;
            contact.Status = RecordStatus.Active;
            _contact.Add(contact);
            return RedirectToAction("Edit", new { id = contact.PersonId });
        }

        //
        // POST: /Supplier/Create

        [HttpPost]
        public ActionResult Create(Supplier supplier)
        {
            try
            {
                supplier.Person.PersonGuid = Guid.NewGuid();
                supplier.Person.Status = RecordStatus.Active;
                supplier.Person.LocationId = 1;
                supplier.Person.CreatedBy = 1;
                supplier.Person.CreatedOn = DateTime.Now;
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
                supplier.Person.EditedBy = 1;
                supplier.Person.EditedOn = DateTime.Now;
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
