using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RAAuthentication.Models;
using RAAuthentication.JWTAuthentication;

namespace RAAuthentication.Controllers
{
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
