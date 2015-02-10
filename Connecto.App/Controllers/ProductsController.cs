using Connecto.BusinessObjects;
using Connecto.Common.Enumeration;
using Connecto.Repositories;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Connecto.App.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductRepository _product = ConnectoFactory.ProductRepository;
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
            var products = new[] { new Product { ProductId = 1, Name = "Cement" }, new Product { ProductId = 2, Name = "Tile" } };
            var product = products.FirstOrDefault(e => e.ProductId == id);
            return Json(product, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProducts()
        {
            var products = new[] { new Product { ProductId = 1, Name = "Cement" }, new Product { ProductId = 2, Name = "Tile" } };
            return Json(products, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Create(Product product)
        {

            product.ProductGuid = Guid.NewGuid();
            product.CreatedBy = 1;
            product.CreatedOn = DateTime.Now;
            product.Status = RecordStatus.Active;
            return Json(1, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Vendors()
        {
            var vendors = new[] { new Vendor { VendorId = 1, Name = "Sanstha" }, new Vendor { VendorId = 2, Name = "Holcim" } };

            return Json(vendors, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ProductTypes()
        {
            var productTypes = new[] { new ProductType { ProductTypeId = 1, Type = "Cement" }, new ProductType { ProductTypeId = 1, Type = "Tile" } };
            return Json(productTypes, JsonRequestBehavior.AllowGet);
        }
       
    }
}
