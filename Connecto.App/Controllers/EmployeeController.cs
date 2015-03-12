using Connecto.App.Models;
using Connecto.BusinessObjects;
using Connecto.Common.Enumeration;
using Connecto.Repositories;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Connecto.App.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeRepository _repo = ConnectoFactory.EmployeeRepository;
        //
        // GET: /Employee/

        public JsonResult Get()
        {
            var items = _repo.GetAll();
            return Json(items, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPeople()
        {
            var items = _repo.GetPeople();
            return Json(items, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Create(int id)
        {
            var errors = new List<ConnectoException>();
            if (id <= 0) errors.Add(new ConnectoException { Message = "Please provide none" });
            if (errors.Count > 0) return Json(new ConnectoValidation { Status = "Failure", Exceptions = errors }, JsonRequestBehavior.AllowGet);

            _repo.Add(new Employee { PersonId = id, LocationId = 1, CreatedBy = User.UserId(), CreatedOn = DateTime.Now, Status = RecordStatus.Active });
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Supplier/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        {
            return View();
        }
        public ActionResult People()
        {
            return View();
        }
        //
        // POST: /Person/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            _repo.Delete(id, User.UserId());
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
