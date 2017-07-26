using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAAuthentication.JWTAuthentication
{
    class DetailedJwtPayload : BasicJwtPayload
    {
        public string email { get; set; }
        public string name { get; set; }

        public DetailedJwtPayload(string userName, string email, string name) : base(userName)
        {
            this.email = email;
            this.name = name;
        }
    }
}
