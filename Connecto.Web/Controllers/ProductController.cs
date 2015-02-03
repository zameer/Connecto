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
    public class ProductController : Controller
    {
        private readonly ProductRepository _product = ConnectoFactory.ProductRepository;
        //
        // GET: /Product/

        public ActionResult Index()
        {
            //var productId = _product.Add(new Product {  });
            //var product = _product.Get(productId);
            return View();
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
