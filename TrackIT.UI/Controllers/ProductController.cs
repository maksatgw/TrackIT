using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TrackIT.Business.Abstract;
using TrackIT.DTO.Dtos.Product;
using TrackIT.UI.ViewModels;

namespace TrackIT.UI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _service;
        private readonly IMapper _mapper;

        public ProductController(IProductService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public IActionResult Index(string searchQuery, string filterByCategory, string filterByDate)
        {
            if (!string.IsNullOrEmpty(searchQuery))
            {
                var searchvalues = _mapper.Map<List<ProductGetDto>>(_service.TGetWithIncludedSearch(searchQuery));
                return View(searchvalues);
            }
            var data = _mapper.Map<List<ProductGetDto>>(_service.TGetWithIncluded());
            var values = new ProductViewModel()
            {
                Products = data,
            };
            return View(values);


        }
    }
}
