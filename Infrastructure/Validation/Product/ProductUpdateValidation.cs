using FluentValidation;
using Infrastructure.Models.Product;

namespace Infrastructure.Validation.Product
{
    public class ProductUpdateValidation : AbstractValidator<ProductUpdateVm>
    {
        public ProductUpdateValidation()
        {
            RuleFor(r => r.Name).NotEmpty().NotNull();
            RuleFor(r => r.Price).GreaterThan(0);
            RuleFor(r => r.Article).NotEmpty().NotNull();
        }
    }  
}
