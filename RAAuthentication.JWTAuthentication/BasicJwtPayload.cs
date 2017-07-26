using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAAuthentication.JWTAuthentication
{
    class BasicJwtPayload
    {
        public const string DEFAULT_ISS = "RAAuthentication";

        private const int ExpirationTimespanInSeconds = 60 * 60;

        public string iss { get; set; }
        public long iat { get; set; }
        public long exp { get; set; }
        public string aud { get; set; }

        public BasicJwtPayload(string userName)
        {
            iss = DEFAULT_ISS;
            iat = this.CalculateJwtNumericDateValue(DateTime.UtcNow);
            exp = iat + ExpirationTimespanInSeconds;
            aud = userName;
        }

        private long CalculateJwtNumericDateValue(DateTime dateTime)
        {
            double expInSeconds = dateTime
                .Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))
                .TotalSeconds;

            return Convert.ToInt64(expInSeconds);
        }

        public bool IsExpired()
        {
            try
            {
                return exp < this.CalculateJwtNumericDateValue(DateTime.UtcNow);
            }
            catch
            {
                return true;
            }
        }
    }
}
