using RAAuthentication.Models;
using System.Web.Http;
using System.Web.Http.Cors;

namespace RAAuthentication.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api")]
    public class RootEndpointController : ApiController
    {
        [Route("")]
        [HttpGet]
        public IHttpActionResult ListEndpointCategories()
        {
            return Ok(new EndpointCategoriesDTO());
        }
    }
}
