using FluentValidation;
using Infrastructure.Models.Account;

namespace Infrastructure.Validation.Account
{
    public class ExternalLoginValidation : AbstractValidator<ExternalLoginVM>
    {
        public ExternalLoginValidation()
        {
            RuleFor(r => r.Provider).NotEmpty();
            RuleFor(r => r.Token).NotEmpty();
        }
    }
}
