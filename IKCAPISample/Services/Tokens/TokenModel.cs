using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IKCAPISample.Services.Tokens
{
    public class TokenModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpireDate { get; set; } // refreshToken Expire Time
        // Access Token Expire Timeden daha büyük tutulmalıdır. // 20 dk

    }
}
