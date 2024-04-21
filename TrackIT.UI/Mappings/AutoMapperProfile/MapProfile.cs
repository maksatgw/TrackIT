using AutoMapper;
using TrackIT.DTO.Dtos.Product;
using TrackIT.Entity.Model;

namespace TrackIT.UI.Mappings.AutoMapperProfile
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<ProductGetDto, Product>();
            CreateMap<Product, ProductGetDto>();
        }
    }
}
