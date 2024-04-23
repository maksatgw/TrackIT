using AutoMapper;
using TrackIT.DTO.Dtos.CategoryDtos;
using TrackIT.DTO.Dtos.ProductDtos;
using TrackIT.Entity.Model;

namespace TrackIT.UI.Mappings.AutoMapperProfile
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<ProductGetDto, Product>();
            CreateMap<Product, ProductGetDto>();
            CreateMap<ProductAddDto, Product>();

            CreateMap<CategoryGetDto, Category>();
            CreateMap<Category, CategoryGetDto>();
            
        }
    }
}
