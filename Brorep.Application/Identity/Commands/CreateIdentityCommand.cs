using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Brorep.Application.Identity.Commands
{
    public class CreateIdentityCommand : IRequest
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class CreateIdentityCommandHandler : IRequestHandler<CreateIdentityCommand, Unit>
    {
        private readonly IMediator _mediator;
        private UserManager<IdentityUser> _userManager;

        public CreateIdentityCommandHandler(
            UserManager<IdentityUser> userManager,
            IMediator mediator)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        public async Task<Unit> Handle(CreateIdentityCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            var email = await _userManager.FindByEmailAsync(request.Email);
            if (user == null && email == null)
            {
                user = new IdentityUser
                {
                    UserName = request.Username,
                    Email = request.Email
                };

                var result = await _userManager.CreateAsync(user, request.Password);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                    }
                }
            }

            return Unit.Value;
        }
    }
}
