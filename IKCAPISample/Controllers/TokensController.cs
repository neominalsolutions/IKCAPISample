using IKCAPISample.Data.Models;
using IKCAPISample.Models;
using IKCAPISample.Services.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IKCAPISample.Controllers
{
    public class MyUserManager : UserManager<ApplicationUser>
    {
        public MyUserManager(IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators, IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<ApplicationUser>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }


        public override Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
        {
           

            return base.CheckPasswordAsync(user, password);
        }
    }


    [Route("api/[controller]")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        private readonly ITokenService tokenService;
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _sManager;

        public TokensController(ITokenService tokenService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser>  sManager)
        {
            this.tokenService = tokenService;
            _userManager = userManager;
            _sManager = sManager;
        }


        [HttpPost("user-create")]
        public async Task<ActionResult> CreateUser()
        {
            var user = new ApplicationUser();
            user.UserName = "mert";
            user.Email = "mert.alptekin@test.com";
           

            await _userManager.CreateAsync(user,"Test1234!");

            return Ok();
        }

        [HttpPost("create-token")]
        public async Task<ActionResult> CreateToken([FromBody] LoginDto loginDto)
        {

            //userManager.GetClaimsAsync();

            var user = await _userManager.FindByEmailAsync(loginDto.Email);

           await  _userManager.CheckPasswordAsync(user, loginDto.Password);

            // User


            //_sManager.extr(new ApplicationUser(), true);


            if (user != null)
            {
                //var claim = new Claim("Role", "Admin");
                //var claim1 = new Claim("Email", "test@test.com");

                //var claims = new List<Claim>();
                //claims.Add(claim);
                //claims.Add(claim1);

                var claims = await _userManager.GetClaimsAsync(user);
                claims.Add(new Claim("Email", user.Email));

                var token = this.tokenService.CreateAccessToken(claims);
                // refresh token kullancaksak user tablosundaki refreshtoken ve expiredate alanlarını güncellemem lazım.

                return Ok(token);
            }

            return Unauthorized();

        }


        [HttpPost("refresh")]
        public ActionResult RefreshToken([FromBody] RefreshTokenDto refreshTokenDto)
        {

           var principles =  tokenService.Validate(refreshTokenDto.AccessToken);

            if(principles == null)
            {
                return Unauthorized();
            }

            // daha sonra kullanıcın gönderdiği refresh token üzerinden expire date göre token kontrol etmem lazım.
            // user bilgileri repo içinden çekilip refresh token sahip mi ve expire süresi dolamış mı tekrar access token alabilir.


            var token = this.tokenService.CreateAccessToken(principles.Claims);

            return Ok(token);
        }
    }
}
