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
        }
    }
}
