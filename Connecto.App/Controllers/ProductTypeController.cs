using Connecto.Repositories;
using System.Web.Mvc;

namespace Connecto.App.Controllers
{
    public class ProductTypeController : Controller
    {
        private readonly ProductTypeRepository _productType = ConnectoFactory.ProductTypeRepository;
        private readonly MeasureRepository _measure = ConnectoFactory.MeasureRepository;
        
    }
}
