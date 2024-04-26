using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackIT.DTO.Dtos.ProductRegisterDtos
{
    public class ProductRegisterUpdateDto
    {
        public int ProductRegistirationId { get; set; }
        public int ProductId { get; set; }
        public string AppUserId { get; set; }
        public DateTime RegistirationDate { get; set; } = DateTime.Now;
        [Required(ErrorMessage ="Lütfen Belge Yükleyiniz.")]
        public IFormFile? FilePath { get; set; }
    }
}
