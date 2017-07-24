using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RAAuthentication.Models;

namespace RAAuthentication.Controllers
{
    [RoutePrefix("api/tokens")]
    public class TokensController : ApiController
    {
        [Route("validate")]
        [HttpPost]
        public IHttpActionResult CheckToken(AuthorizationDTO authorization)
        {
            string testToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE0ODMyMDAwMDAwMDAsImVtYWlsIjoicGF0cmljay56aG9uZ0BleGFtcGxlLmNvbSJ9.jIBK2wO6qtoAdT4v5bGaPP_ytZfIMqW_4Ofh9UTLqj4";

            if (authorization.AccessToken == testToken)
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
