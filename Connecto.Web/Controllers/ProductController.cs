using Connecto.BusinessObjects;
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

    }
}
