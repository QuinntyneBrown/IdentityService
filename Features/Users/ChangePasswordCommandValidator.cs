using FluentValidation.Results;
using FluentValidation;

namespace IdentityService.Features.Users
{
    public interface IChangePasswordCommandValidator {
        ValidationResult Validate(ChangePasswordCommand.Request instance);
    }

    public class ChangePasswordCommandValidator: AbstractValidator<ChangePasswordCommand.Request>, IChangePasswordCommandValidator
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required");

            RuleFor(x => x.Password)
                .MinimumLength(6)
                .WithMessage("Password must be at least 6 characters long");

            RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password)
                .WithMessage("Confimation does not match");
        }
    }
}