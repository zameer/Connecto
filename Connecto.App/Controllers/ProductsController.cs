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
    public class ProductsController : Controller
    {
        private readonly ProductRepository _repo = ConnectoFactory.ProductRepository;
        private readonly VendorRepository _repoVendor = ConnectoFactory.VendorRepository;
        private readonly ProductTypeRepository _repoProductType = ConnectoFactory.ProductTypeRepository;
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
        public ActionResult Details()
        {
            return View();
        }
        public ActionResult Edit()
        {
            return View();
        }
        public JsonResult Get(int id)
        {
            var product = _repo.Get(id);
            return Json(product, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProducts()
        {
            var products = _repo.GetAll();
            return Json(products, JsonRequestBehavior.AllowGet);
        }
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

        [HttpGet]
        public JsonResult Vendors()
        {
            var vendors = _repoVendor.GetAll();
            return Json(vendors, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ProductTypes()
        {
            var productTypes = _repoProductType.GetAll();
            return Json(productTypes, JsonRequestBehavior.AllowGet);
        }
       
    }
}
