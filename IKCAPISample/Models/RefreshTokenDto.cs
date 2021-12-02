using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IKCAPISample.Models
{
    public class RefreshTokenDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpireDate { get; set; }

    }
}
