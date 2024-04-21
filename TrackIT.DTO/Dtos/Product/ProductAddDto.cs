using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackIT.DTO.Dtos.Product
{
    public class ProductAddDto
    {
        public string Name { get; set; }
        public string Serial { get; set; }
        public string Description { get; set; }
        public string AppUserId { get; set; }
        public int CategoryId { get; set; }
    }
}
