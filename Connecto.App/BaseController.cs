using System.Web.Mvc;
using Connecto.App.Models;

namespace Connecto.App
{
    [Authorize]
    public class BaseController : Controller
    {
        public LocationInfo Location
        {
            get
            {
                return User.LocationInfo();
            }
        }
    }
}