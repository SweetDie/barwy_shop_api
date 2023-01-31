using AutoMapper;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using Infrastructure.Models.Product;
using Infrastructure.Services.Interfaces;
using Infrastructure.Validation.Product;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Classes
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse> CreateProductAsync(ProductCreateVM model)
        {
            var validator = new ProductCreateValidation();
            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                return new ServiceResponse
                {
                    IsSuccess = false,
                    Message = "Некоректні дані",
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage)
                };
            }

            var newProduct = _mapper.Map<Product>(model);
            newProduct.DateCreated = DateTime.Now.ToUniversalTime();

            var resultCreate = await _productRepository.CreateAsync(newProduct);

            if(!resultCreate)
             {
                 return new ServiceResponse
                 {
                     IsSuccess = false,
                     Message = "Помилка при створенні товару"
                 };
             }

            var resultAddCategory = await _productRepository.AddToCategoryAsync(newProduct, model.Categories);

            if (!resultAddCategory)
            {
                return new ServiceResponse
                {
                    IsSuccess = false,
                    Message = "Помилка при створенні товару"
                };
            }
            
            return new ServiceResponse
            {
                IsSuccess = true,
                Message = "Товар успішно створено"
            };
        }

        public async Task<List<ProductVM>> GetAllProductsAsync()
        {
            var products = await _productRepository.Products.ToListAsync();
            var productsVM = _mapper.Map<List<ProductVM>>(products);
            return productsVM;
        }
    }
}
