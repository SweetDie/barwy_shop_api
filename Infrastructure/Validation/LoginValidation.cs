using FluentValidation;
using Infrastructure.Models.Account;

namespace Infrastructure.Validation
{
    public class LoginValidation : AbstractValidator<LoginVM>
    {
        public LoginValidation()
        {
            RuleFor(r => r.Email).NotEmpty().EmailAddress();
            RuleFor(r => r.Password).NotEmpty().MinimumLength(6);
        }
    }
}
