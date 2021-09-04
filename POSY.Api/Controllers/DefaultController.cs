using System.Collections.Generic;
using System.Web.Http;

namespace POSY.Api.Controllers
{
    public class DefaultController : ApiController
    {
        //
        // GET: api/default
        [AllowAnonymous]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}
