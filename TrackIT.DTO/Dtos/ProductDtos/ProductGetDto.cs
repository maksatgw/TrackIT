using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackIT.Business.Model;
using TrackIT.Entity.Model;

namespace TrackIT.DTO.Dtos.ProductDtos
{
    public class ProductGetDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Serial { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public bool isActive { get; set; } = true;
        public DateTime DateAdded { get; set; }
        public Entity.Model.Category Category { get; set; }
        public List<ProductRegistirationHistory> ProductRegistirationHistories { get; set; }
    }
}
