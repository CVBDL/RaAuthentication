using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jose;

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

        public string GetToken(string email)
        {
            string token = string.Empty;
            JWTPayload payload = new JWTPayload();
            payload.email = email;

            try
            {
                token = JWT.Encode(payload.jwtPayloadDTO, Encoding.ASCII.GetBytes(_jwtConfig.SecretKey), _jwtConfig.jwsAlgorithm);
            }
            catch
            {

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
                // check expiration time
                string payloadJson = JWT.Decode(token, Encoding.ASCII.GetBytes(_jwtConfig.SecretKey), _jwtConfig.jwsAlgorithm);
                JWTPayload jwtPayload = new JWTPayload(payloadJson);
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
