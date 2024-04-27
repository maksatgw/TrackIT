using TrackIT.DTO.Dtos.CategoryDtos;

namespace TrackIT.UI.ViewModels
{
    public class CategoryViewModel
    {
        public List<CategoryGetDto>? Categories { get; set; }
        public CategoryAddDto? CategoryAdd { get; set; }
        public CategoryUpdateDto? CategoryUpdate { get; set; }
    }
}
