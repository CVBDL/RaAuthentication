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
            bool isValid = false;
            string payload = string.Empty;

            try
            {
                payload = JWT.Decode(token, Encoding.ASCII.GetBytes(_jwtConfig.SecretKey), _jwtConfig.jwsAlgorithm);
                JWTPayload jwtPayload = new JWTPayload(payload);
                isValid = !jwtPayload.IsExpired();
            }
            catch
            {

            }

            return isValid;
        }
    }
}
