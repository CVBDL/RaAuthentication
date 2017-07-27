using System;
using System.Web.Http;
using RAAuthentication.Models;
using RAAuthentication.JWTAuthentication;
using RAAuthenticationLib;
using System.Web.Http.Description;
using System.Web.Http.Cors;
using System.Threading.Tasks;

namespace RAAuthentication.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        /// <summary>
        /// Get an access token.
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpPost]
        [ResponseType(typeof(AuthorizationDTO))]
        public async Task<IHttpActionResult> GetAccessToken([FromBody] CredentialDTO credential, [FromUri] string scope = null)
        {
            string userName = credential.UserName;
            string password = credential.Password;

            bool isValidUser = await Authentication.CheckAuthenticateAsync(userName, password, "ra-int");
            if (!isValidUser)
            {
                return Unauthorized();
            }

            AuthorizationDTO authorization = null;

            if (string.Equals(scope, "none", StringComparison.OrdinalIgnoreCase))
            {
                authorization = new AuthorizationDTO
                {
                    IdToken = JWTAuthenticate.Instance().GetBasicToken(userName)
                };
            }
            else
            {
                UserDetail user = await Authentication.GetUserEmailFromADAsync(userName, password, "ra-int");
                authorization = new AuthorizationDTO
                {
                    IdToken = JWTAuthenticate.Instance().GetDetailedToken(userName, user.EmailAddress, user.Name)
                };
            }

            return Ok(authorization);
        }

        /// <summary>
        /// Get user details.
        /// </summary>
        /// <returns></returns>
        [Route("details")]
        [HttpPost]
        [ResponseType(typeof(UserDetailsDTO))]
        public async Task<IHttpActionResult> GetUserDetails(CredentialDTO credential)
        {
            string userName = credential.UserName;
            string password = credential.Password;

            bool isValidUser = await Authentication.CheckAuthenticateAsync(userName, password, "ra-int");
            if (!isValidUser)
            {
                return Unauthorized();
            }

            UserDetail userDetails = await Authentication.GetUserEmailFromADAsync(userName, password, "ra-int");

            return Ok(userDetails);
        }
    }
}
