using AutoMapper;
using DAL.Entities;
using Infrastructure.Models.Category;

namespace Infrastructure.Settings.Mapper
{
    public class AutoMapperCategoryProfile : Profile
    {
        public AutoMapperCategoryProfile()
        {
            CreateMap<CategoryVM, Category>()
                .ForMember(dest => dest.NormalizedName,
                    opt => opt.MapFrom(src => src.Name.ToUpper()));
            CreateMap<Category, CategoryVM>();
            CreateMap<CategoryProduct, CategoryVM>()
                .ForMember(
                    dest => dest.Id,
                opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(
                    dest => dest.Name,
                opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<CategoryCreateVM, Category>()
                .ForMember(
                    dest => dest.NormalizedName,
                    opt => opt.MapFrom((src => src.Name.ToUpper())))
                .ForMember(
                    dest => dest.DateCreated,
                    opt => opt.MapFrom(src => DateTime.Now.ToUniversalTime()));
        }
    }
}
