using AutoMapper;
using TrackIT.Business.Model;
using TrackIT.DTO.Dtos.CategoryDtos;
using TrackIT.DTO.Dtos.ProductDtos;
using TrackIT.DTO.Dtos.UserDtos;
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
            CreateMap<Product, ProductAddDto>();
            CreateMap<ProductUpdateDto, Product>();
            CreateMap<Product, ProductUpdateDto>();

            CreateMap<CategoryGetDto, Category>();
            CreateMap<Category, CategoryGetDto>();

            CreateMap<UserGetDto, AppUser>();
            CreateMap<AppUser, UserGetDto>();
            CreateMap<AppUser, UserAddDto>();
            CreateMap<UserAddDto, AppUser>();
            
        }
    }
}
