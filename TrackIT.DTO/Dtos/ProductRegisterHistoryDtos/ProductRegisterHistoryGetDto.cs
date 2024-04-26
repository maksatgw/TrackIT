using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackIT.Business.Model;
using TrackIT.Entity.Model;

namespace TrackIT.DTO.Dtos.ProductRegisterHistoryDtos
{
    public class ProductRegisterHistoryGetDto
    {
        public int ProductRegistirationHistoryId { get; set; }
        public int ProductId { get; set; }
        public string AppUserId { get; set; }
        public string FilePath { get; set; }
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        public Product Product { get; set; }
        public AppUser AppUser { get; set; }
    }
}
