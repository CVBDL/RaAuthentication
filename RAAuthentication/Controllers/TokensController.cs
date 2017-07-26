using System.Net;
using System.Web.Http;
using RAAuthentication.Models;
using RAAuthentication.JWTAuthentication;
using System.Web.Http.Cors;

namespace RAAuthentication.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/token")]
    public class TokensController : ApiController
    {
        [Route("validate")]
        [HttpPost]
        public IHttpActionResult CheckToken(AuthorizationDTO authorization)
        {

            if (JWTAuthenticate.Instance().IsValid(authorization.IdToken))
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
