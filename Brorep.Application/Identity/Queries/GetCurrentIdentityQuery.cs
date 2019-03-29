using Brorep.Application.Identity.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Brorep.Application.Identity.Queries
{
    public class GetCurrentIdentityQuery : IRequest<UserDto>
    {
        public GetCurrentIdentityQuery(ClaimsPrincipal user)
        {
            User = user;
        }

        public ClaimsPrincipal User { get; }
    }

    public class GetCurrentIdentityQueryHandler : IRequestHandler<GetCurrentIdentityQuery, UserDto>
    {
        private UserManager<IdentityUser> _userManager;

        public GetCurrentIdentityQueryHandler(
            UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserDto> Handle(GetCurrentIdentityQuery request, CancellationToken cancellationToken)
        {
            if(request.User == null)
            {
                throw new Exception("todo");
            }

            var user = await _userManager.FindByNameAsync(request.User.Identity.Name);

            return new UserDto()
            {
                Email = user.Email,
                User = user.UserName
            };
        }
    }
}
