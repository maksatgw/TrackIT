using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackIT.DTO.Dtos.LocationDtos
{
    public class LocationAddDto
    {
        [Required(ErrorMessage = "Lokasyon İsmi Boş Geçilemez")]
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
