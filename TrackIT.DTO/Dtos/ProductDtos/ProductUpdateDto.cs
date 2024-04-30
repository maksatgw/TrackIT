using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackIT.DTO.Dtos.ProductDtos
{
    public class ProductUpdateDto
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Ürün ismi Boş Geçilemez")]
        public string Name { get; set; }
        public string? Serial { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public int LocationId { get; set; }
    }
}
