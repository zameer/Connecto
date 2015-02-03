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
    public class ProductTypeController : Controller
    {
        private readonly ProductTypeRepository _productType = ConnectoFactory.ProductTypeRepository;
        private readonly MeasureRepository _measure = ConnectoFactory.MeasureRepository;
        
    }
}
