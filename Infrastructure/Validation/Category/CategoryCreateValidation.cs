using FluentValidation;
using Infrastructure.Models.Category;

namespace Infrastructure.Validation.Category ;

    public class CategoryCreateValidation : AbstractValidator<CategoryCreateVm>
    {
        public CategoryCreateValidation()
        {
            RuleFor(c => c.Name).NotEmpty().NotNull();
        }
    }