using System.Collections.Generic;
using System.IO;
using System.Linq;
using Connecto.App.BusinessIntelligence.Dataset.TransactionsTableAdapters;
using Connecto.App.Models;
using Connecto.App.Utilities;
using Connecto.BusinessObjects;
using Connecto.Common.Enumeration;
using Connecto.Repositories;
using System;
using System.Web.Mvc;
using Connecto.App.ModelValidator;
using Microsoft.Reporting.WebForms;

namespace Connecto.App.Controllers
{
    public class CustomerReturnController : Controller
    {
        private readonly CustomerReturnRepository _repo = ConnectoFactory.CustomerReturnRepository;
       

        public JsonResult GetSalesDetailByOrderId(int orderId)
        {
            var item = _repo.GetSalesDetailByOrderId(orderId);
            return Json(item, JsonRequestBehavior.AllowGet);
        }


        
        public ActionResult Index()
        {
            return View();
        }
        //
        // GET: /CartIn/
        public ActionResult Checkout()
        {
            return View();
        }
    }
}
