using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace RAAuthentication.JWTAuthentication
{
    public partial class JWTPayload
    {
        //Default expire time. 
        // 3600 seconds from now. 
        private const double _defaultTimeExpSeconds = 3600;

        //User log in name 
        private string _userName;
        public string username
        {
            get { return _userName; }
            set { _userName = value; }
        }

        //Token expire time from now
        private long _exp = 0;
        public long exp
        {
            get { return _exp; }
        }

        //Constructor.
        public JWTPayload()
        {
            _exp = DateTime.Now.AddSeconds(_defaultTimeExpSeconds).Ticks;
        }

        //Constructor. Create new payload via json string.
        public JWTPayload(string jsonString)
        {
            try
            {
                var _obj = JsonConvert.DeserializeObject<JWTPayload>(jsonString);
                username = _obj.username;
                _exp = _obj._exp;
            }
            catch
            {
                throw new JWTUnknowPayloadExpections("Unknow payload json string");
            }
        }

        //Token is expired or not.
        // True if expire time is lower than now, otherwise is false.
        public bool IsExpired()
        {
            long now = DateTime.Now.Ticks;
            return (_exp > now ? false : true);
        }

        //Convert this to JSON string.
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
