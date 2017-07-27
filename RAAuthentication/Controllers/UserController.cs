using RAAuthentication.JWTAuthentication;
using RAAuthentication.Models;
using RAAuthenticationLib;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace RAAuthentication.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        private const string DOMAIN_NAME = "ra-int";

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

            try
            {
                bool isValidUser = await Authentication.CheckAuthenticateAsync(userName, password, DOMAIN_NAME);
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
                    UserDetail user = await Authentication.GetUserEmailFromADAsync(userName, password, DOMAIN_NAME);
                    authorization = new AuthorizationDTO
                    {
                        IdToken = JWTAuthenticate.Instance().GetDetailedToken(userName, user.EmailAddress, user.Name)
                    };
                }

                return Ok(authorization);
            }
            catch
            {
                return InternalServerError();
            }
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

            try
            {
                bool isValidUser = await Authentication.CheckAuthenticateAsync(userName, password, DOMAIN_NAME);
                if (!isValidUser)
                {
                    return Unauthorized();
                }

                UserDetail userDetails = await Authentication.GetUserEmailFromADAsync(userName, password, DOMAIN_NAME);

                return Ok(userDetails);
            }
            catch
            {
                return InternalServerError();
            }
        }
    }
}
