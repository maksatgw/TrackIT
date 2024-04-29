using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackIT.DTO.Dtos.UserDtos
{
    public class UserUpdateDto
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Email Gereklidir")]
        [EmailAddress(ErrorMessage = "Lütfen Geçerli Bir Mail Adresi Girin")]
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        [Required(ErrorMessage = "Soyisim gereklidir")]
        public string Surname { get; set; }
        public string RoleName { get; set; }
    }
}
