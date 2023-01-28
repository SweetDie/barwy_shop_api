using FluentValidation;
using Infrastructure.Models.Product;

namespace Infrastructure.Validation.Product
{
    public class ProductCreateValidation : AbstractValidator<ProductCreateVM>
    {
        public ProductCreateValidation()
        {
            RuleFor(r => r.Name).NotEmpty();
            RuleFor(r => r.Price).GreaterThan(0);
            RuleFor(r => r.Article).NotEmpty();
            RuleFor(r => r.Categories).Must(c => c.Any());
        }
    }
}
