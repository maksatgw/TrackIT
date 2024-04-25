using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using TrackIT.Business.Abstract;
using TrackIT.Business.Model;
using TrackIT.DTO.Dtos.CategoryDtos;
using TrackIT.DTO.Dtos.ProductRegisterDtos;
using TrackIT.DTO.Dtos.UserDtos;
using TrackIT.Entity.Model;
using TrackIT.UI.ViewModels;

namespace TrackIT.UI.Controllers
{
    public class ProductRegisterController : Controller
    {
        private readonly IProductRegisterService _productRegisterService;
        private readonly ICategoryService _categoryService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IToastNotification _toastNotification;
        private readonly IMapper _mapper;

        public ProductRegisterController(IProductRegisterService productRegisterService, IToastNotification toastNotification, IMapper mapper, ICategoryService categoryService, UserManager<AppUser> userManager)
        {
            _productRegisterService = productRegisterService;
            _toastNotification = toastNotification;
            _mapper = mapper;
            _categoryService = categoryService;
            _userManager = userManager;
        }

        public IActionResult Index(string? searchQuery, int? categoryId, string? userId)
        {
            var productRegisters = _mapper.Map<List<ProductRegisterGetDto>>(_productRegisterService.TGetWithIncluded());
            var categories = _mapper.Map<List<CategoryGetDto>>(_categoryService.TGet());
            var users = _mapper.Map<List<UserGetDto>>(_userManager.Users.ToList());

            if (!string.IsNullOrEmpty(searchQuery))
            {
                productRegisters = _mapper.Map<List<ProductRegisterGetDto>>(_productRegisterService.TGetWithIncludedSearch(searchQuery: searchQuery));
            }
            if (categoryId != null)
            {
                productRegisters = _mapper.Map<List<ProductRegisterGetDto>>(_productRegisterService.TGetWithIncludedSearch(categoryId: categoryId));
            }
            if (!string.IsNullOrEmpty(userId))
            {
                productRegisters = _mapper.Map<List<ProductRegisterGetDto>>(_productRegisterService.TGetWithIncludedSearch(userId: userId));
            }
            var productRegisterViewModel = new ProductRegisterViewModel
            {
                ProductRegisters = productRegisters,
                Categories = categories,
                Users = users
            };
            return View(productRegisterViewModel);
        }

        [HttpPost]
        public IActionResult Update(ProductRegisterViewModel model)
        {
            var value = _mapper.Map<ProductRegistiration>(model.ProductRegisterUpdate);
            value.RegistirationDate = DateTime.Now;
            _productRegisterService.TUpdate(value);
            _toastNotification.AddSuccessToastMessage("Başarılı", new ToastrOptions { Title = "Başarılı" });
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult New()
        {
            return View();
        }
        [HttpPost]
        public IActionResult New(int id)
        {
            return View();
        }

        public IActionResult Remove(int id, string requestFrom)
        {
            var value = _productRegisterService.TGet(id);
            _productRegisterService.TDelete(value);
            if (requestFrom == "user")
            {
                _toastNotification.AddSuccessToastMessage("Zimmet Kaldırıldı", new ToastrOptions { Title = "Başarılı" });
                return RedirectToAction("Index", "User");
            }
            return RedirectToAction("Index");
        }
    }
}
