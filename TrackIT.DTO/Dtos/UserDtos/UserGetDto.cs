using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackIT.DTO.Dtos.ProductRegisterDtos;
using TrackIT.Entity.Model;

namespace TrackIT.DTO.Dtos.UserDtos
{
    public class UserGetDto 
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        [Required(ErrorMessage = "Soyisim gereklidir")]
        public string Surname { get; set; }
        public string UserName { get; set; }
        public List<ProductRegisterGetDto> ProductRegistirations { get; set; }
        public int? UserProductCount { get; set; }
        public string Role { get; set; }
    }
}
