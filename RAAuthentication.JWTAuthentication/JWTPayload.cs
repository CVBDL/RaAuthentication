using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace RAAuthentication.JWTAuthentication
{
    public class JWTPayloadDTO
    {
        public string email;
        public long exp;
    }

    public partial class JWTPayload
    {
        //Default expire time. 
        // 3600 seconds from now. 
        private const double _defaultTimeExpSeconds = 3600;
        private JWTPayloadDTO _jwtPayloadDTO;

        public JWTPayloadDTO jwtPayloadDTO
        {
            get { return _jwtPayloadDTO; }
        }

        public string email
        {
            get { return _jwtPayloadDTO.email; }
            set { _jwtPayloadDTO.email = value; }
        }

        //Constructor.
        public JWTPayload()
        {
            _jwtPayloadDTO = new JWTPayloadDTO();
            _jwtPayloadDTO.exp = DateTime.Now.AddSeconds(_defaultTimeExpSeconds).Ticks;
        }

        //Constructor. Create new payload via json string.
        public JWTPayload(string jsonString)
        {
            try
            {
                var _obj = JsonConvert.DeserializeObject<JWTPayloadDTO>(jsonString);
                _jwtPayloadDTO = _obj as JWTPayloadDTO;
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
            return (_jwtPayloadDTO.exp > now ? false : true);
        }
    }
}
