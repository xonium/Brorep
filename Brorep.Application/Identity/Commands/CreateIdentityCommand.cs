using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Brorep.Application.Identity.Commands
{
    public class CreateIdentityCommand : IRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        
        public class Handler : IRequestHandler<CreateIdentityCommand, Unit>
        {
            private readonly IMediator _mediator;
            private UserManager<IdentityUser> _userManager;

            public Handler(
                UserManager<IdentityUser> userManager,
                IMediator mediator)
            {
                _mediator = mediator;
                _userManager = userManager;
            }

            public async Task<Unit> Handle(CreateIdentityCommand request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                {
                    user = new IdentityUser { UserName = request.Email, Email = request.Email };
                    var balle = await _userManager.IsEmailConfirmedAsync(user);

                    var result = await _userManager.CreateAsync(user, request.Password);
                    if(!result.Succeeded)
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
}
