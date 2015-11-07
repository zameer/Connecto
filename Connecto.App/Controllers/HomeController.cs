using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Connecto.App.Models;
using Connecto.BusinessObjects;

namespace Connecto.App.Controllers
{
    public class HomeController : BaseController
    {
        public JsonResult GetStarter()
        {
            var starter = new Starter
            {
                EmployeeId = User.UserId(),
                Todate = string.Format("{0:g}", DateTime.Now)
            };
            return Json(starter, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}