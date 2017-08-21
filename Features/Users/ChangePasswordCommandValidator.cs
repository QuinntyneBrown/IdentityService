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

        }
    }
}