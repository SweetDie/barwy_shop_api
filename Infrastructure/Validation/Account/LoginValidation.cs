using FluentValidation;
using Infrastructure.Models.Account;

namespace Infrastructure.Validation.Account
{
    public class LoginValidation : AbstractValidator<LoginVm>
    {
        public LoginValidation()
        {
            RuleFor(r => r.Email).NotEmpty().EmailAddress();
            RuleFor(r => r.Password).NotEmpty().MinimumLength(6);
        }
    }
}
