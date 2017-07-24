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
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        /// <summary>
        /// Get an access token.
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpPost]
        public AuthorizationDTO GetAccessToken(CredentialDTO credential)
        {
            string userName = credential.UserName;
            string password = credential.Password;

            return new AuthorizationDTO
            {
                AccessToken = JWTAuthenticate.Instance().GetToken(userName)
            };
        }

        /// <summary>
        /// Get user details.
        /// </summary>
        /// <returns></returns>
        [Route("details")]
        [HttpPost]
        public UserDetailsDTO GetUserDetails(CredentialDTO credential)
        {
            string userName = credential.UserName;
            string password = credential.Password;

            return new UserDetailsDTO
            {
                DisplayName = "Patrick Zhong",
                EmailAddress = "patrick.zhong@example.com",
                EmployeeId = "A0123456789",
                Name = "Patrick"
            };
        }
    }
}
