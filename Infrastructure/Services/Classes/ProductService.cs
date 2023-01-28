﻿using AutoMapper;
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

        public Task<ServiceResponse> AddCategoryToProductAsync(string category)
        {
            throw new NotImplementedException();
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
            var categories = new List<Category>();

            foreach (var item in model.Categories)
            {
                var category = await _categoryRepository.Categories.FirstOrDefaultAsync(c => c.NormalizedName == item.ToUpper());
                await _productRepository.CreateProduct(newProduct, category);
            }

            return new ServiceResponse
            {
                IsSuccess = true,
                Message = "Товар створено"
            };
        }

        public Task<ServiceResponse> DeleteProductAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
