using Connecto.App.ModelValidator;
using Connecto.BusinessObjects;
using Connecto.Common.Enumeration;
using Connecto.Repositories;
using System;
using System.Web.Mvc;

namespace Connecto.App.Controllers
{
    public class VendorController : BaseController
    {
        private readonly VendorRepository _repo = ConnectoFactory.VendorRepository;
        public JsonResult GetSearch(FilterCriteria criteria)
        {
            var items = _repo.GetAllSearch(criteria);
            return Json(new { recordsTotal = items.Item2, recordsFiltered = items.Item2, data = items.Item1 }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Get()
        {
            var items = _repo.GetAll();
            return Json(items, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetItem(int id)
        {
            var item = _repo.GetVendorById(id);
            return Json(item, JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Vendor/Create
        [HttpPost]
        public JsonResult Create(Vendor item)
        {
            var errors = new VendorValidator(item, _repo).Validate();
            if (errors.Count > 0) return Json(new ConnectoValidation { Status = "Failure", Exceptions = errors }, JsonRequestBehavior.AllowGet);

            item.LocationId = 1;
            item.VendorGuid = Guid.NewGuid();
            item.CreatedBy = Location.UserId;
            item.CreatedOn = DateTime.Now;
            item.Status = RecordStatus.Active;
            _repo.Add(item);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Vendor/Edit/5
        [HttpPost]
        public ActionResult Edit(Vendor item)
        {
            var errors = new VendorValidator(item, _repo).Validate();
            if (errors.Count > 0) return Json(new ConnectoValidation { Status = "Failure", Exceptions = errors }, JsonRequestBehavior.AllowGet);

            item.EditedBy = Location.UserId;
            item.EditedOn = DateTime.Now;
            _repo.Edit(item);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Vendor/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var errors = new VendorValidator(_repo).Validate(id);
            if (errors.Count > 0) return Json(new ConnectoValidation { Status = "Failure", Exceptions = errors }, JsonRequestBehavior.AllowGet);

            _repo.Delete(id, Location.UserId);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Vendor/
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
    }
}
