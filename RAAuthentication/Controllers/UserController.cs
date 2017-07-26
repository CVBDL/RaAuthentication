using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RAAuthentication.Models;
using RAAuthentication.JWTAuthentication;
using RAAuthenticationLib;
using System.Web.Http.Description;

namespace RAAuthentication.Controllers
{
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
        public IHttpActionResult GetAccessToken([FromBody] CredentialDTO credential)
        {
            string userName = credential.UserName;
            string password = credential.Password;

            bool isValidUser = Authentication.CheckAuthenticate(userName, password, "ra-int");
            if (!isValidUser)
            {
                return Unauthorized();
            }

            UserDetail userDetails = Authentication.GetUserEmailFromAD(userName, password, "ra-int");
            AuthorizationDTO authorization = new AuthorizationDTO
            {
                IdToken = JWTAuthenticate.Instance().GetToken(userDetails.EmailAddress)
            };

            return Ok(authorization);
        }

        /// <summary>
        /// Get user details.
        /// </summary>
        /// <returns></returns>
        [Route("details")]
        [HttpPost]
        [ResponseType(typeof(UserDetailsDTO))]
        public IHttpActionResult GetUserDetails(CredentialDTO credential)
        {
            string userName = credential.UserName;
            string password = credential.Password;

            bool isValidUser = Authentication.CheckAuthenticate(userName, password, "ra-int");
            if (!isValidUser)
            {
                return Unauthorized();
            }

            UserDetail userDetails = Authentication.GetUserEmailFromAD(userName, password, "ra-int");

            return Ok(userDetails);
        }
    }
}
