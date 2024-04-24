using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackIT.Business.Model;

namespace TrackIT.DTO.Dtos.ProductRegisterDtos
{
    public class ProductRegisterGetDto
    {
        public int ProductRegistirationId { get; set; }
        public int ProductId { get; set; }
        public string AppUserId { get; set; }
        public DateTime RegistirationDate { get; set; }

        [ForeignKey("AppUserId")]
        public AppUser AppUser { get; set; }
    }
}
