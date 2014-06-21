using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebServicesForExamples.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public object Get()
        {
            return new {Enabled = true};
        }
    }
}
    