using RAAuthentication.JWTAuthentication;
using RAAuthentication.Models;
using System.Net;
using System.Web.Http;
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
            try
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
            catch
            {
                return InternalServerError();
            }
        }
    }
}
