using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackIT.DTO.Dtos.UserDtos
{
    public class UserAddDto
    {
        [Required(ErrorMessage ="Email Gereklidir")]
        [EmailAddress(ErrorMessage = "Email Formatı Yanlış")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Kullanıcı adı gereklidir")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "İsim gereklidir")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Soyisim gereklidir")]
        public string Surname { get; set; }
        public string RoleName { get; set; }
        [Required(ErrorMessage = "Şifre Gereklidir")]
        public string Password { get; set; }

    }
}
