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

        public async Task<List<CategoryVM>> GetAllAsync()
        {
            var categories = await _categoryRepository.Categories.ToListAsync();
            var categoriesVM = _mapper.Map<List<CategoryVM>>(categories);
            return categoriesVM;
        }

        public Task<ServiceResponse> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse> RestoreAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse> UpdateAsync(CategoryUpdateVM model)
        {
            throw new NotImplementedException();
        }
    }
}
