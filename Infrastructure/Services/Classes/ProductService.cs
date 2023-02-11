using AutoMapper;
using DAL;
using DAL.Entities;
using DAL.Entities.Image;
using DAL.Repositories.Interfaces;
using Infrastructure.Constants;
using Infrastructure.Models.Category;
using Infrastructure.Models.Product;
using Infrastructure.Services.Interfaces;
using Infrastructure.Validation.Product;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Classes
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        private readonly AppEFContext _context;

        public ProductService(IProductRepository productRepository, IMapper mapper, AppEFContext context, IFileService fileService)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _context = context;
            _fileService = fileService;
        }

        public async Task<ServiceResponse> CreateAsync(ProductCreateVm model)
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

            if (model.Image == null || model.Image.FileName == "blob")
            {
                const string fileName = ImagesConstants.ProductDefaultImage;
                var path = Path.Combine(ImagesConstants.ImagesFolder, ImagesConstants.ProductImageFolder);
                newProduct.Image = new ProductImageEntity
                {
                    FileName = fileName,
                    Path = path,
                    FullName = Path.Combine(path, fileName)
                };
            }
            else
            {
                newProduct.Image = await _fileService.UploadImageAsync<ProductImageEntity>(model.Image, ImagesConstants.ProductImageFolder);
            }

            var resultCreate = await _productRepository.CreateAsync(newProduct);

            if(!resultCreate)
             {
                 return new ServiceResponse
                 {
                     IsSuccess = false,
                     Message = "Помилка при створенні товару"
                 };
             }

            bool resultAddCategory;

            if (model.Categories.Any())
            {
                resultAddCategory = await _productRepository.AddToCategoryAsync(newProduct, model.Categories);
            }
            else
            {
                resultAddCategory = await _productRepository.AddToCategoryAsync(newProduct, "Uncategorized");
            }

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

        public async Task<ServiceResponse> GetAllAsync()
        {
            var products = await _productRepository.Products.ToListAsync();
            var productsVm = _mapper.Map<List<ProductVm>>(products);
            return new ServiceResponse
            {
                IsSuccess = true,
                Message = "Products loaded",
                Payload = productsVm
            };
        }

        public async Task<ServiceResponse> DeleteAsync(Guid id)
        {
            var product = await _productRepository.Products.FirstOrDefaultAsync(p => p.Id == id);
            _fileService.DeleteFile(product.Image.FullName);

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

        public async Task<ServiceResponse> UpdateAsync(ProductUpdateVm model)
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
            var products = await categoryProducts.Select(x => new ProductVm
            {
                Id = x.ProductId.ToString(),
                Name = x.Product.Name,
                Article = x.Product.Article,
                Image = x.Product.Image.FullName,
                Size = x.Product.Size,
                Price = x.Product.Price,
                Categories = x.Product.CategoryProduct.Select(categoryProduct => new CategoryVm
                {
                    Id = categoryProduct.CategoryId.ToString(),
                    Name = categoryProduct.Category.Name
                }).ToList()
            }).ToListAsync();

            return new ServiceResponse 
            {
                IsSuccess = true,
                Message = "Products loaded",
                Payload = products
            };
        }
    }
}
