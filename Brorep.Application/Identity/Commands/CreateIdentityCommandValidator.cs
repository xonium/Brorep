using FluentValidation;

namespace Brorep.Application.Identity.Commands
{
    public class CreateIdentityCommandValidator : AbstractValidator<CreateIdentityCommand>
    {
        public CreateIdentityCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(5).Equal(x => x.ConfirmPassword)
                .WithMessage("Password must match");
        }
    }
}
