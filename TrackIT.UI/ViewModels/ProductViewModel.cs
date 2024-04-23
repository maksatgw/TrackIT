using TrackIT.DTO.Dtos.CategoryDtos;
using TrackIT.DTO.Dtos.ProductDtos;

namespace TrackIT.UI.ViewModels
{
    public class ProductViewModel
    {
        public List<ProductGetDto>? Products { get; set; }
        public List<CategoryGetDto>? Categories { get; set; }
        public ProductAddDto ProductAdd { get; set; }

    }
}
