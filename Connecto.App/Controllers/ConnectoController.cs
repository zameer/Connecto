using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Connecto.BusinessObjects;

namespace Connecto.App.Controllers
{
    public class ConnectoController : ApiController
    {
        // GET api/connecto
        public IEnumerable<Vendor> Get()
        {
            var vendors = new[] { new Vendor { VendorId = 1, Name = "Sanstha" }, new Vendor { VendorId = 2, Name = "Holcim" } };
            return vendors;
        }

        // GET api/connecto/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/connecto
        public void Post([FromBody]string value)
        {
        }

        // PUT api/connecto/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/connecto/5
        public void Delete(int id)
        {
        }
    }
}
