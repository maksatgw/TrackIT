using TrackIT.DTO.Dtos.UserDtos;
using TrackIT.DTO.Dtos.UserRoleDtos;

namespace TrackIT.UI.ViewModels
{
    public class UserViewModel
    {
        public List<UserGetDto>? AppUsers { get; set; }
        public List<UserRoleGetDto>? AppRoles { get; set; }
        public UserAddDto UserAdd { get; set; }
        public int? TotalPage { get; set; }
        public int? CurrentPage { get; set; }
    }
}
