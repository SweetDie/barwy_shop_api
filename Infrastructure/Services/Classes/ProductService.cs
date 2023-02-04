using AutoMapper;
using DAL;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using Infrastructure.Constants;
using Infrastructure.Models.Category;
using Infrastructure.Models.Product;
using Infrastructure.Services.Interfaces;
using Infrastructure.Validation.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Classes
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly AppEFContext _context;

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository, IMapper mapper, AppEFContext context)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse> CreateAsync(ProductCreateVM model)
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
            var image = await UploadImageAsync(model.Image);
            newProduct.Image = image;

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
                Message = "Товар успішно створено",
                Payload = newProduct
            };
        }

        public async Task<ServiceResponse> GetAllAsync()
        {
            var products = await _productRepository.Products.ToListAsync();
            var productsVM = _mapper.Map<List<ProductVM>>(products);
            return new ServiceResponse
            {
                IsSuccess = true,
                Message = "Products loaded",
                Payload = productsVM
            };
        }

        public async Task<ServiceResponse> RestoreAsync(Guid id)
        {
            var result = await _productRepository.RestoreAsync(id);
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
                Message = "Товар відновлено"
            };
        }

        public async Task<ServiceResponse> DeleteAsync(Guid id)
        {
            var result = await _productRepository.DeleteAsync(id);
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
                Message = "Товар видалено"
            };
        }

        public async Task<ServiceResponse> UpdateAsync(ProductUpdateVM model)
        {
            var validator = new ProductUpdateValidation();
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
            var newProduct = _mapper.Map<Product>(model);
            var result = await _productRepository.UpdateAsync(newProduct);

            if (!result)
            {
                return new ServiceResponse
                {
                    IsSuccess = false,
                    Message = "Не вдалося оновити товар"
                };
            }

            return new ServiceResponse
            {
                IsSuccess = true,
                Message = "Товар успішно оновлено"
            };
        }

        public async Task<ServiceResponse> GetAllByCategoryAsync(string categoryName)
        {
            var categoryProducts = _context.CategoryProduct.Where(x => x.Category.Name.Contains(categoryName)).AsQueryable();
            var products = await categoryProducts.Select(x => new ProductVM
            {
                Id = x.ProductId.ToString(),
                Name = x.Product.Name,
                Article = x.Product.Article,
                Image = x.Product.Image,
                Size = x.Product.Size,
                Price = x.Product.Price,
                Categories = x.Product.CategoryProduct.Select(x => new CategoryVM
                {
                    Id = x.CategoryId.ToString(),
                    Name = x.Category.Name
                }).ToList()
            }).ToListAsync();

            return new ServiceResponse 
            {
                IsSuccess = true,
                Message = "Products loaded",
                Payload = products
            };
        }

        public async Task<string> UploadImageAsync(IFormFile image)
        {
            try
            {
                if (image == null)
                {
                    return DefaultImages.NotAvailable;
                }

                var fileExp = Path.GetExtension(image.FileName);
                var dir = Path.Combine(Directory.GetCurrentDirectory(), "Images");
                string fileName = Path.GetRandomFileName() + fileExp;

                using (var stream = File.Create(Path.Combine(dir, fileName)))
                {
                    await image.CopyToAsync(stream);
                }

                return fileName;
            }
            catch (Exception)
            {

                return DefaultImages.NotAvailable;
            }
        }
    }
}
