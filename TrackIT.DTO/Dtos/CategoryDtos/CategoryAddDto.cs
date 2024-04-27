using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackIT.DTO.Dtos.CategoryDtos
{
    public class CategoryAddDto
    {
        [Required(ErrorMessage ="İsim Gereklidir.")]
        public string Name { get; set; }
    }
}
