using FluentValidation;
using Infrastructure.Models.Category;

namespace Infrastructure.Validation.Category ;

    public class CategoryUpdateValidation : AbstractValidator<CategoryUpdateVM>
    {
        public CategoryUpdateValidation()
        {
            RuleFor(c => c.Name).NotEmpty().NotNull();
        }
    }