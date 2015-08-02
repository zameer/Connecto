using Connecto.App.Models;
using Connecto.BusinessObjects;
using Connecto.Common.Enumeration;
using Connecto.Repositories;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Connecto.App.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly ProductRepository _repo = ConnectoFactory.ProductRepository;
        //
        // GET: /Product/
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
        public JsonResult GetItem(int id)
        {
            var product = _repo.Get(id);
            return Json(product, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Get()
        {
            var products = _repo.GetAll();
            return Json(products, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSearch(FilterCriteria criteria)
        {
            var items = _repo.GetAllSearch(criteria);
            return Json(new { recordsTotal = items.Item2, recordsFiltered = items.Item2, data = items.Item1 }, JsonRequestBehavior.AllowGet);
        }
        //
        // POST: /Product/Create
        [HttpPost]
        public JsonResult Create(Product item)
        {
            item.ProductGuid = Guid.NewGuid();
            item.CreatedBy = User.UserId();
            item.CreatedOn = DateTime.Now;
            item.Status = RecordStatus.Active;
            _repo.Add(item);
            return Json(1, JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Product/Edit/5
        [HttpPost]
        public ActionResult Edit(Product item)
        {
            item.EditedBy = User.UserId();
            item.EditedOn = DateTime.Now;
            _repo.Edit(item);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            _repo.Delete(id, User.UserId());
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
