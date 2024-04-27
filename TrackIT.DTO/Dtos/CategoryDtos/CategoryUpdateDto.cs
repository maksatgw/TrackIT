using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackIT.DTO.Dtos.CategoryDtos
{
    public class CategoryUpdateDto
    {
        [Required(ErrorMessage ="ID Gereklidir.")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Kategori İsmi Gereklidir.")]
        public string Name { get; set; }
    }
}
