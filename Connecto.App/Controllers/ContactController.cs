using Connecto.BusinessObjects;
using Connecto.Common.Enumeration;
using Connecto.Repositories;
using System;
using System.Web.Mvc;

namespace Connecto.App.Controllers
{
    public class ContactController : Controller
    {
        private readonly ContactRepository _contact = ConnectoFactory.ContactRepository;
        //
        // GET: /Contact/

        public ActionResult Index()
        {
            var contact = _contact.GetAll();
            return View(contact);
        }

        //
        // GET: /Contact/Details/5

        public ActionResult Details(int id)
        {
            var contact = _contact.GetContact(id);
            return View(contact);
        }

        //
        // GET: /Contact/Create

        public ActionResult Create()
        {
            return View(new Contact());
        }

        //
        // POST: /Contact/Create

        [HttpPost]
        public ActionResult Create(Contact contact)
        {
            try
            {
                contact.PersonId = 1;
                contact.ContactGuid = Guid.NewGuid();
                contact.CreatedBy = 1;
                contact.CreatedOn = DateTime.Now;
                contact.Status = RecordStatus.Active;
                _contact.Add(contact);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Contact/Edit/5

        public ActionResult Edit(int id)
        {
            var contact = _contact.GetContact(id);
            return View(contact);
        }

        //
        // POST: /Contact/Edit/5

        [HttpPost]
        public ActionResult Edit(Contact contact)
        {
            try
            {
                contact.EditedBy = 1;
                contact.EditedOn = DateTime.Now;
                _contact.Edit(contact);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Contact/Delete/5

        public ActionResult Delete(int id)
        {
            var contact = _contact.GetContact(id);
            return View(contact);
        }

        //
        // POST: /Contact/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _contact.Delete(id, 3);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
