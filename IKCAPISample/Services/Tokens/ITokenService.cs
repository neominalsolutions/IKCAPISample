using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IKCAPISample.Services.Tokens
{
    public interface ITokenService
    {

       TokenModel CreateAccessToken(IEnumerable<Claim> claims);
       string CreateRefreshToken();

        ClaimsPrincipal Validate(string token);


    }
}
