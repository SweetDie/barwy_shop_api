using AutoMapper;
using DAL.Entities;
using Infrastructure.Models.Product;

namespace Infrastructure.Settings.Mapper
{
    public class AutoMapperProductProfile : Profile
    {
        public AutoMapperProductProfile()
        {
            CreateMap<Product, ProductVm>()
                .ForMember(
                dest => dest.Categories,
                opt => opt.MapFrom(src => src.CategoryProduct))
                .ForMember(
                    dest => dest.Image,
                    opt => opt.MapFrom(src => src.Image.FullName));
            CreateMap<ProductCreateVm, Product>()
                .ForMember(dest => dest.Image,
                    opt => opt.Ignore());
            CreateMap<ProductUpdateVm, Product>();
        }
    }
}
