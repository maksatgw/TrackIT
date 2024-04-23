using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TrackIT.Business.Abstract;
using TrackIT.DTO.Dtos.CategoryDtos;
using TrackIT.DTO.Dtos.ProductDtos;
using TrackIT.Entity.Model;
using TrackIT.UI.ViewModels;

namespace TrackIT.UI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        //Filtre nesnelerini query string üzerinden alıyoruz.
        public IActionResult Index(string searchQuery, int filterByCategory)
        {

            var categoryDtos = _mapper.Map<List<CategoryGetDto>>(_categoryService.TGet());
            var productViewModel = new ProductViewModel()
            {
                Categories = categoryDtos,
            };
            if (!string.IsNullOrEmpty(searchQuery))
            {
                var productSearchResult = _productService.TGetWithIncludedSearch(searchQuery);
                var productDtos = _mapper.Map<List<ProductGetDto>>(productSearchResult);
                productViewModel.Products = productDtos;
            }
            else if (filterByCategory != 0)
            {
                var productFiterByCategory = _productService.TGetByCategory(filterByCategory);
                var productDtos = _mapper.Map<List<ProductGetDto>>(productFiterByCategory);
                productViewModel.Products = productDtos;
            }
            else
            {
                var productDtos = _mapper.Map<List<ProductGetDto>>(_productService.TGetWithIncluded());
                productViewModel.Products = productDtos;
            }
            _mapper.Map<List<CategoryGetDto>>(_categoryService.TGet());
            return View(productViewModel);
        }


        [HttpGet]
        public IActionResult New()
        {
            var categoryDtos = _mapper.Map<List<CategoryGetDto>>(_categoryService.TGet());
            var productViewModel = new ProductViewModel()
            {
                Categories = categoryDtos,
            };
            return View(productViewModel);
        }
        [HttpPost]
        public IActionResult New(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _mapper.Map<List<CategoryGetDto>>(_categoryService.TGet());
                return View(model);
            }

            var value = _mapper.Map<Product>(model.ProductAdd);
            _productService.TInsert(value);
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int productId)
        {
            var value = _productService.TGet(productId);
            _productService.TDelete(value);
            return RedirectToAction("Index");
        }
        
    }
}
