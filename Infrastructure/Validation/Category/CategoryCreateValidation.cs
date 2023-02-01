using FluentValidation;
using Infrastructure.Models.Category;

namespace Infrastructure.Validation.Category ;

    public class CategoryCreateValidation : AbstractValidator<CategoryCreateVM>
    {
        public CategoryCreateValidation()
        {
            RuleFor(c => c.Name).NotEmpty();
        }
    }