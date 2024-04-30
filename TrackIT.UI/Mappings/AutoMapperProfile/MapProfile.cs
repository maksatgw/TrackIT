using AutoMapper;
using TrackIT.Business.Model;
using TrackIT.DTO.Dtos.CategoryDtos;
using TrackIT.DTO.Dtos.LocationDtos;
using TrackIT.DTO.Dtos.ProductAssetDtos;
using TrackIT.DTO.Dtos.ProductDtos;
using TrackIT.DTO.Dtos.ProductRegisterDtos;
using TrackIT.DTO.Dtos.ProductRegisterHistoryDtos;
using TrackIT.DTO.Dtos.UserDtos;
using TrackIT.DTO.Dtos.UserRoleDtos;
using TrackIT.Entity.Model;

namespace TrackIT.UI.Mappings.AutoMapperProfile
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            // Product ve ilgili DTO'lar için haritalamalar
            CreateMap<Product, ProductGetDto>();
            CreateMap<ProductGetDto, Product>();

            CreateMap<Product, ProductAddDto>();
            CreateMap<ProductAddDto, Product>();

            CreateMap<Product, ProductUpdateDto>();
            CreateMap<ProductUpdateDto, Product>();

            // Category ve ilgili DTO'lar için haritalamalar
            CreateMap<Category, CategoryGetDto>();
            CreateMap<CategoryGetDto, Category>();
            CreateMap<CategoryAddDto, Category>();
            CreateMap<Category, CategoryAddDto>();
            CreateMap<CategoryUpdateDto, Category>();
            CreateMap<Category, CategoryUpdateDto>();

            // AppUser ve ilgili DTO'lar için haritalamalar
            CreateMap<AppUser, UserGetDto>();
            CreateMap<UserGetDto, AppUser>();

            CreateMap<AppUser, UserAddDto>();
            CreateMap<UserAddDto, AppUser>();

            CreateMap<AppUser, UserUpdateDto>();
            CreateMap<UserUpdateDto, AppUser>();

            // ProductRegistration ve ilgili DTO'lar için haritalamalar
            CreateMap<ProductRegistiration, ProductRegisterGetDto>();
            CreateMap<ProductRegisterGetDto, ProductRegistiration>();

            CreateMap<ProductRegistiration, ProductRegisterUpdateDto>();
            CreateMap<ProductRegisterUpdateDto, ProductRegistiration>();

            CreateMap<ProductRegistiration, ProductRegisterAddDto>();
            CreateMap<ProductRegisterAddDto, ProductRegistiration>();

            // ProductRegistrationHistory ve ilgili DTO'lar için haritalamalar
            CreateMap<ProductRegistiration, ProductRegisterAddDto>();
            CreateMap<ProductRegisterAddDto, ProductRegistirationHistory>();

            CreateMap<ProductRegistirationHistory, ProductRegisterAddDto>();
            CreateMap<ProductRegisterAddDto, ProductRegistirationHistory>();

            CreateMap<ProductRegistiration, ProductRegisterGetDto>();
            CreateMap<ProductRegisterGetDto, ProductRegistirationHistory>();

            CreateMap<ProductRegistirationHistory, ProductRegisterGetDto>();

            CreateMap<ProductRegisterHistoryGetDto, ProductRegistirationHistory>();
            CreateMap<ProductRegistirationHistory, ProductRegisterHistoryGetDto>();

            CreateMap<ProductRegisterUpdateDto, ProductRegistirationHistory>();
            CreateMap<ProductRegistirationHistory, ProductRegisterUpdateDto>();

            // ProductAsset ve ilgili DTO'lar için haritalamalar
            CreateMap<ProductAsset, ProductAssetAddDto>();
            CreateMap<ProductAssetAddDto, ProductAsset>();
            CreateMap<ProductAssetGetDto, ProductAsset>();
            CreateMap<ProductAsset, ProductAssetGetDto>();

            //AppRole Map
            CreateMap<UserRoleGetDto, AppRole>();
            CreateMap<AppRole, UserRoleGetDto>();

            CreateMap<Location, LocationGetDto>();
            CreateMap<LocationGetDto, Location>();
            CreateMap<Location, LocationUpdateDto>();
            CreateMap<LocationUpdateDto, Location>();

            CreateMap<LocationAddDto, Location>();
            CreateMap<Location, LocationAddDto>();

        }
    }
}
