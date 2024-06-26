﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackIT.DTO.Dtos.ProductRegisterDtos
{
    public class ProductRegisterAddDto
    {
        public int ProductId { get; set; }
        public string AppUserId { get; set; }
        public DateTime RegistirationDate { get; set; } = DateTime.Now;
        public IFormFile? FilePath { get; set; }
    }
}
