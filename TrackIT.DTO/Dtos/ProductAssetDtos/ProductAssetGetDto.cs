using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackIT.DTO.Dtos.ProductAssetDtos
{
    public class ProductAssetGetDto
    {
        public int ProductAssetId { get; set; }
        public int ProductId { get; set; }
        public string AssetUrl { get; set; }
    }
}
