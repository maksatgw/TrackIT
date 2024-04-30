using TrackIT.Business.Model;
using TrackIT.DTO.Dtos.CategoryDtos;
using TrackIT.DTO.Dtos.ProductDtos;
using TrackIT.DTO.Dtos.ProductRegisterDtos;
using TrackIT.DTO.Dtos.UserDtos;
using TrackIT.Entity.Model;

namespace TrackIT.UI.ViewModels
{
    public class ProductRegisterViewModel
    {
        public List<ProductRegisterGetDto>? ProductRegisters { get; set; }
        public List<CategoryGetDto>? Categories { get; set; }
        public List<UserGetDto>? Users { get; set; }
        public ProductRegisterUpdateDto? ProductRegisterUpdate { get; set; }
        public ProductRegisterAddDto? ProductRegisterAdd { get; set; }
        public UserGetDto? AppUser { get; set; }
        public List<ProductGetDto>? ProductsGet { get; set; }
    }
}
