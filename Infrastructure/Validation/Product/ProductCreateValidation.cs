using FluentValidation;
using Infrastructure.Models.Product;

namespace Infrastructure.Validation.Product
{
    public class ProductCreateValidation : AbstractValidator<ProductCreateVm>
    {
        public ProductCreateValidation()
        {
            RuleFor(r => r.Name).NotEmpty().NotNull();
            RuleFor(r => r.Price).GreaterThanOrEqualTo(0);
            RuleFor(r => r.Article).NotEmpty().NotNull();
        }
    }
}
