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
    public class EmployeeController : Controller
    {
        private readonly EmployeeRepository _employee = ConnectoFactory.EmployeeRepository;
        private readonly ContactRepository _contact = ConnectoFactory.ContactRepository;
        //
        // GET: /Employee/

        public ActionResult Index()
        {
            var employee = _employee.GetAll();
            return View(employee);
        }

        //
        // GET: /Employee/Details/5

        public ActionResult Details(int id)
        {
            var employee = _employee.GetEmployeeById(id);
            return View(employee);
        }

        public ActionResult Contacts(List<Contact> contacts)
        {
            return PartialView("_Contacts", contacts);
        }

        //
        // GET: /Employee/Create

        public ActionResult Create()
        {
            return View(new Employee());
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
        // POST: /Employee/Create

        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            try
            {
                employee.Person.PersonGuid = Guid.NewGuid();
                employee.Person.Status = RecordStatus.Active;
                employee.Person.LocationId = 1;
                employee.Person.CreatedBy = 1;
                employee.Person.CreatedOn = DateTime.Now;
                _employee.Add(employee);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Employee/Edit/5

        public ActionResult Edit(int id)
        {
            var employee = _employee.GetEmployeeById(id);
            return View(employee);
        }

        //
        // POST: /Employee/Edit/5

        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            try
            {
                employee.Person.EditedBy = 1;
                employee.Person.EditedOn = DateTime.Now;
                _employee.Edit(employee);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Employee/Delete/5

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
