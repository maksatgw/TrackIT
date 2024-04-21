using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TrackIT.Business.Abstract;
using TrackIT.DTO.Dtos.Product;

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

        public IActionResult Index()
        {
            var values = _mapper.Map<List<ProductGetDto>>(_service.TGet());
            return View(values);
        }
    }
}
