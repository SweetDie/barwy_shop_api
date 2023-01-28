using AutoMapper;
using DAL.Entities;
using Infrastructure.Models.Product;

namespace Infrastructure.Settings.Mapper
{
    public class AutoMapperProductProfile : Profile
    {
        public AutoMapperProductProfile()
        {
            CreateMap<Product, ProductVM>();
            CreateMap<ProductVM, Product>();
            CreateMap<ProductCreateVM, Product>().ForMember(p => p.CategoryProduct, c => c.Ignore());
        }
    }
}
