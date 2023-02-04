using AutoMapper;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using Infrastructure.Models.Category;
using Infrastructure.Services.Interfaces;
using Infrastructure.Validation.Category;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Classes
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse> CreateAsync(CategoryCreateVM model)
        {
            var validator = new CategoryCreateValidation();
            var validationResult = validator.Validate(model);
            if (!validationResult.IsValid)
            {
                return new ServiceResponse
                {
                    IsSuccess = false,
                    Message = "Ім'я вказано невірно",
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage)
                };
            }

            if (await _categoryRepository.IsExistAsync(model.Name))
            {
                return new ServiceResponse
                {
                    IsSuccess = false,
                    Message = "Категорія з таким іменем вже існує",
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage)
                };
            }

            var category = _mapper.Map<Category>(model);
            var resultCreate = await _categoryRepository.CreateAsync(category);
            if (!resultCreate)
            {
                return new ServiceResponse
                {
                    IsSuccess = false,
                    Message = "Помилка при створенні категорії"
                };
            }
            
            return new ServiceResponse
            {
                IsSuccess = true,
                Message = "Категорію успішно створено"
            };
        }

        public async Task<ServiceResponse> GetAllAsync()
        {
            var categories = await _categoryRepository.Categories.ToListAsync();
            var categoriesVM = _mapper.Map<List<CategoryVM>>(categories);
            return new ServiceResponse
            {
                IsSuccess = true,
                Message = "Categories loaded",
                Payload = categoriesVM
            };
        }

        public async Task<ServiceResponse> DeleteAsync(Guid id)
        {
            var result = await _categoryRepository.DeleteAsync(id);
            if (!result)
            {
                return new ServiceResponse
                {
                    IsSuccess = false,
                    Message = "Не вдалося видалити"
                };
            }
            return new ServiceResponse
            {
                IsSuccess = true,
                Message = "Категорію видалено"
            };
        }

        public async Task<ServiceResponse> RestoreAsync(Guid id)
        {
            var result = await _categoryRepository.RestoreAsync(id);
            if(!result)
            {
                return new ServiceResponse
                {
                    IsSuccess = false,
                    Message = "Не вдалося відновити"
                };
            }
            return new ServiceResponse
            {
                IsSuccess = true,
                Message = "Категорію відновлено"
            };
        }

        public async Task<ServiceResponse> UpdateAsync(CategoryUpdateVM model)
        {
            var validator = new CategoryUpdateValidation();
            var validationResult = await validator.ValidateAsync(model);
            if(!validationResult.IsValid)
            {
                return new ServiceResponse
                {
                    IsSuccess = false,
                    Message = "Некоректні дані",
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage)
                };
            }
            var newCategory = _mapper.Map<Category>(model);
            var result = await _categoryRepository.UpdateAsync(newCategory);

            if (!result)
            {
                return new ServiceResponse
                {
                    IsSuccess = false,
                    Message = "Не вдалося оновити категорію"
                };
            }

            return new ServiceResponse
            {
                IsSuccess = true,
                Message = "Категорію успішно оновлено"
            };
        }
    }
}
