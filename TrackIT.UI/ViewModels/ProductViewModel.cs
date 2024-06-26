﻿using TrackIT.DTO.Dtos.CategoryDtos;
using TrackIT.DTO.Dtos.LocationDtos;
using TrackIT.DTO.Dtos.ProductAssetDtos;
using TrackIT.DTO.Dtos.ProductDtos;

namespace TrackIT.UI.ViewModels
{
    public class ProductViewModel
    {
        public List<ProductGetDto>? Products { get; set; }
        public List<CategoryGetDto>? Categories { get; set; }
        public List<LocationGetDto>? Locations { get; set; }
        public ProductAddDto? ProductAdd { get; set; }
        public ProductUpdateDto? ProductUpdate { get; set; }
        public List<ProductAssetGetDto>? ProductAssets { get; set; }
        public int? CurrentPage { get; set; }
        public int? TotalPage { get; set; }
    }
}
