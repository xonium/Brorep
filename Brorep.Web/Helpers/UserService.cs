using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Brorep.WebUI.Helpers
{
    public interface IUserService
    {
        string Authenticate(string username, string password);        
    }

    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private SignInManager<IdentityUser> _signInManager;
        private UserManager<IdentityUser> _userManager;

        public UserService(IOptions<AppSettings> appSettings, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _appSettings = appSettings.Value;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public string Authenticate(string username, string password)
        {
            var identityUser = _userManager.FindByNameAsync(username).GetAwaiter().GetResult();
            if (identityUser == null)
            {
                return null;
            }

            var signInResult = _signInManager.CheckPasswordSignInAsync(identityUser, password, false).GetAwaiter().GetResult();
            if (!signInResult.Succeeded) return null;
            
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, identityUser.Id)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);            
        }
    }
}
