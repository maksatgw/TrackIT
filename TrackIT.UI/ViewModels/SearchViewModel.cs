using TrackIT.DTO.Dtos.CategoryDtos;
using TrackIT.DTO.Dtos.ProductDtos;
using TrackIT.DTO.Dtos.ProductRegisterDtos;
using TrackIT.DTO.Dtos.UserDtos;

namespace TrackIT.UI.ViewModels
{
    public class SearchViewModel
    {
        public List<ProductGetDto>? Products { get; set; }
        public List<CategoryGetDto>? Categories { get; set; }
        public List<ProductRegisterGetDto>? ProductRegistrations { get; set; }
        public List<UserGetDto>? Users { get; set; }
    }
}
