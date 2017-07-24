using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAAuthentication.JWTAuthentication
{
    public class JWTUnknowPayloadExpections : Exception
    {
        public JWTUnknowPayloadExpections(string message) : base(message) { }
        public JWTUnknowPayloadExpections(string message, Exception innerException) : base(message, innerException) { }
    }
}
