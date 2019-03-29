using FluentValidation;

namespace Brorep.Application.Identity.Commands
{
    public class CreateTokenFromIdentityValidator : AbstractValidator<CreateTokenFromIdentityCommand>
    {
        public CreateTokenFromIdentityValidator()
        {
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
