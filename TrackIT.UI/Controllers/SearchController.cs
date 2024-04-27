using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TrackIT.Business.Abstract;
using TrackIT.Business.Model;
using TrackIT.DTO.Dtos.CategoryDtos;
using TrackIT.DTO.Dtos.ProductDtos;
using TrackIT.DTO.Dtos.ProductRegisterDtos;
using TrackIT.DTO.Dtos.UserDtos;
using TrackIT.UI.ViewModels;

namespace TrackIT.UI.Controllers
{
    public class SearchController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductRegisterService _productRegisterService;
        private readonly ICategoryService _categoryService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public SearchController(IProductService productService, IMapper mapper, IProductRegisterService productRegisterService, ICategoryService categoryService, UserManager<AppUser> userManager)
        {
            _productService = productService;
            _mapper = mapper;
            _productRegisterService = productRegisterService;
            _categoryService = categoryService;
            _userManager = userManager;
        }


        public IActionResult Index(string query)
        {
            if (!string.IsNullOrEmpty(query))
            {
                var products = _mapper.Map<List<ProductGetDto>>(_productService.TGetWithIncludedSearch(query));
                var productsRegisters = _mapper.Map<List<ProductRegisterGetDto>>(_productRegisterService.TGetWithIncludedSearch(query));
                var categories = _mapper.Map<List<CategoryGetDto>>(_categoryService.TGetWithIncludedSearch(query));
                var users = _mapper.Map<List<UserGetDto>>(_userManager.Users.Where(x=>x.Email.Contains(query)));
                var searchViewModel = new SearchViewModel()
                {
                    Products = products,
                    ProductRegistrations = productsRegisters,
                    Categories = categories,
                    Users = users,
                };
                return View(searchViewModel);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
