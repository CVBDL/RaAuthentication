using System.Configuration;

namespace RAAuthentication.Models
{
    public class EndpointCategoriesDTO
    {
        private string rootEndpoint = ConfigurationManager.AppSettings.Get("RootEndpoint");

        public string UserAuthenticationUrl
        {
            get
            {
                return rootEndpoint + "/api/user{?scope}";
            }
        }

        public string UserDetailsUrl
        {
            get
            {
                return rootEndpoint + "/api/user/details";
            }
        }

        public string TokenValidationUrl
        {
            get
            {
                return rootEndpoint + "/api/token/validate";
            }
        }
    }
}
