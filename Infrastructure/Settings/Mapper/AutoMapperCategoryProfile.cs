using AutoMapper;
using DAL.Entities;
using Infrastructure.Models.Category;

namespace Infrastructure.Settings.Mapper
{
    public class AutoMapperCategoryProfile : Profile
    {
        public AutoMapperCategoryProfile()
        {
            CreateMap<CategoryVM, Category>();
            CreateMap<Category, CategoryVM>();
            CreateMap<CategoryProduct, CategoryVM>()
                .ForMember(
                c => c.Id,
                opt => opt.MapFrom(cp => cp.CategoryId))
                .ForMember(
                c => c.Name,
                opt => opt.MapFrom(cp => cp.Category.Name));
                
        }
    }
}
