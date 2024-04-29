using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackIT.Business.Model;
using TrackIT.Entity.Model;

namespace TrackIT.DTO.Dtos.ProductRegisterDtos
{
    public class ProductRegisterGetDto
    {
        public int ProductRegistirationId { get; set; }
        public int ProductId { get; set; }
        public string AppUserId { get; set; }
        public DateTime RegistirationDate { get; set; }
        public string FilePath { get; set; }
        public AppUser AppUser { get; set; }
        public Product Product { get; set; }
        public string QRData { get; set; }

    }
}
