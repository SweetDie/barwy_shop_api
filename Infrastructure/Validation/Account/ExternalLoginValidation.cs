using FluentValidation;
using Infrastructure.Models.Account;

namespace Infrastructure.Validation.Account
{
    public class ExternalLoginValidation : AbstractValidator<ExternalLoginVm>
    {
        public ExternalLoginValidation()
        {
            RuleFor(r => r.Provider).NotEmpty();
            RuleFor(r => r.Token).NotEmpty();
        }
    }
}
