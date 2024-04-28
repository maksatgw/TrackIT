using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackIT.DTO.Dtos.UserRoleDtos
{
    public class UserRoleAddDto
    {
        [Required(ErrorMessage = "Rol Ekle")]
        public string Name { get; set; }
    }
}
