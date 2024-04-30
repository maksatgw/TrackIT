using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackIT.Entity.Model;

namespace TrackIT.DTO.Dtos.LocationDtos
{
    public class LocationGetDto
    {
        public int LocationId { get; set; }
        public string Name { get; set; }
        public string Locations { get; set; }
        public List<Product> Products { get; set; }
    }
}
