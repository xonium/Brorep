using Brorep.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Brorep.Application.Settings;
using Brorep.Application.Identity.Models;
using Brorep.Domain.Entities;

namespace Brorep.Application.Identity.Commands
{
    public class CreateTokenFromIdentityCommand : IRequest<TokenDto>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class CreateTokenFromIdentityCommandHandler : IRequestHandler<CreateTokenFromIdentityCommand, TokenDto>
    {
        private readonly ISettings _settings;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public CreateTokenFromIdentityCommandHandler(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ISettings settings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _settings = settings;
        }

        public async Task<TokenDto> Handle(CreateTokenFromIdentityCommand request, CancellationToken cancellationToken)
        {
            var identityUser = await _userManager.FindByNameAsync(request.Username);
            if (identityUser == null)
            {
                throw new AuthenticationException();
            }
            
            var signInResult = await _signInManager.CheckPasswordSignInAsync(identityUser, request.Password, false);
            if (!signInResult.Succeeded) {
                throw new AuthenticationException();
            };

            await _signInManager.SignInAsync(identityUser, false);

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_settings.Secret);
            var expires = DateTime.UtcNow.AddDays(7); //todo
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, identityUser.UserName)
                }),
                Expires = expires,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new TokenDto()
            {
                Token = tokenHandler.WriteToken(token),
                Expires = expires
            };
        }
    }
}
