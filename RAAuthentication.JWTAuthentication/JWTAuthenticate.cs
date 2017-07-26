using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jose;
using Newtonsoft.Json;

namespace RAAuthentication.JWTAuthentication
{
    public class JWTAuthenticate
    {
        private static JWTAuthenticate instance;

        public static JWTAuthenticate Instance()
        {
            if (instance == null)
            {
                instance = new JWTAuthenticate();
            }
            return instance;
        }

        private JWTConfig _jwtConfig;

        private JWTAuthenticate()
        {
            _jwtConfig = new JWTConfig();
        }

        public string GetBasicToken(string userName)
        {
            string token = null;
            try
            {
                BasicJwtPayload payload = new BasicJwtPayload(userName);
                token = JWT.Encode(payload, Encoding.ASCII.GetBytes(_jwtConfig.SecretKey), _jwtConfig.jwsAlgorithm);
            }
            catch
            {
                return null;
            }

            return token;
        }

        public string GetDetailedToken(string userName, string email, string name)
        {
            string token = null;
            try
            {
                DetailedJwtPayload payload = new DetailedJwtPayload(userName, email, name);
                token = JWT.Encode(payload, Encoding.ASCII.GetBytes(_jwtConfig.SecretKey), _jwtConfig.jwsAlgorithm);
            }
            catch
            {
                return null;
            }

            return token;
        }

        public bool IsValid(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return false;
            }

            string[] tokenParts = token.Split('.');
            if (tokenParts.Length != 3)
            {
                return false;
            }

            try
            {
                string payloadJson = JWT.Decode(token, Encoding.ASCII.GetBytes(_jwtConfig.SecretKey), _jwtConfig.jwsAlgorithm);
                DetailedJwtPayload jwtPayload = JsonConvert.DeserializeObject<DetailedJwtPayload>(payloadJson);

                // check token issuer
                if (!string.Equals(jwtPayload.iss, BasicJwtPayload.DEFAULT_ISS, StringComparison.Ordinal))
                {
                    return false;
                }

                // check expiration time
                if (jwtPayload.IsExpired())
                {
                    return false;
                }

                // check signature
                string validJwt = JWT.Encode(payloadJson, Encoding.ASCII.GetBytes(_jwtConfig.SecretKey), _jwtConfig.jwsAlgorithm);
                if (!string.Equals(validJwt, token, StringComparison.Ordinal))
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
