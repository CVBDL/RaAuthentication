using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jose;
namespace RAAuthentication.JWTAuthentication
{
    public class JWTConfig
    {
        private const string _defaultSecretKey = @"ralibrary";
        private const string RALIBRARY_JWT_KEY = "RA_JWT_KEY";

        //Encrypt algorithm
        private JwsAlgorithm _jwsAlgorithm = JwsAlgorithm.HS256;
        public JwsAlgorithm jwsAlgorithm
        {
            get { return _jwsAlgorithm; }
            set { _jwsAlgorithm = value; }
        }

        //Secret key
        private string _secretKey = string.Empty;
        public string SecretKey
        {
            get { return _secretKey; }
            set { _secretKey = value; }
        }

        public JWTConfig()
        {
            //Set secret key to value from environment table or default value. 
            string varKey = Environment.GetEnvironmentVariable(RALIBRARY_JWT_KEY);
            SecretKey = string.IsNullOrEmpty(varKey) ? _defaultSecretKey : varKey;
        }
    }
}
