using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using NToastNotify;
using NuGet.ContentModel;
using TrackIT.Business.Abstract;
using TrackIT.Business.Model;
using TrackIT.DTO.Dtos.CategoryDtos;
using TrackIT.DTO.Dtos.ProductDtos;
using TrackIT.DTO.Dtos.ProductRegisterDtos;
using TrackIT.DTO.Dtos.UserDtos;
using TrackIT.Entity.Model;
using TrackIT.UI.Extensions;
using TrackIT.UI.ViewModels;

namespace TrackIT.UI.Controllers
{
    public class ProductRegisterController : Controller
    {
        private readonly IProductRegisterService _productRegisterService;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IProductRegisterHistoryService _productRegisterHistoryService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IToastNotification _toastNotification;
        private readonly IMapper _mapper;

        public ProductRegisterController(IProductRegisterService productRegisterService, IToastNotification toastNotification, IMapper mapper, ICategoryService categoryService, UserManager<AppUser> userManager, IProductService productService, IProductRegisterHistoryService productRegisterHistoryService)
        {
            _productRegisterService = productRegisterService;
            _toastNotification = toastNotification;
            _mapper = mapper;
            _categoryService = categoryService;
            _userManager = userManager;
            _productService = productService;
            _productRegisterHistoryService = productRegisterHistoryService;
        }

        public IActionResult Index(string? searchQuery, int? categoryId, string? userId)
        {
            var categories = _mapper.Map<List<CategoryGetDto>>(_categoryService.TGet());
            var productRegisters = _mapper.Map<List<ProductRegisterGetDto>>(_productRegisterService.TGetWithIncluded());
            var users = _mapper.Map<List<UserGetDto>>(_userManager.Users
                .Where(x => x.isActive == true)
                .ToList());
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

        public IActionResult Update()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangeRegister(ProductRegisterViewModel model)
        {
            var register = _mapper.Map<ProductRegistiration>(model.ProductRegisterUpdate);
            var passModel = new ProductRegisterUpdateDto
            {
                AppUserId = model.ProductRegisterUpdate.AppUserId,
                ProductId = model.ProductRegisterUpdate.ProductId,
                RegistirationDate = DateTime.Now
            };
            var regHistory = _mapper.Map<ProductRegistirationHistory>(passModel);
            register.RegistirationDate = DateTime.Now;

            if (model.ProductRegisterUpdate.FilePath != null)
            {

                var newAssetName = await _productRegisterService.TSaveFile(model.ProductRegisterUpdate.FilePath, "productregistration");
                register.FilePath = newAssetName;
                regHistory.FilePath = newAssetName;
            }

            _productRegisterService.TUpdate(register);
            _productRegisterHistoryService.TInsert(regHistory);
            _toastNotification.AddSuccessToastMessage("Zimmet Aktarıldı", new ToastrOptions { Title = "Başarılı" });
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult New()
        {
            var users = _mapper.Map<List<UserGetDto>>(_userManager.Users.Where(x => x.isActive == true).ToList());
            var products = _mapper.Map<List<ProductGetDto>>(_productService.TGetAvailableToRegistrate());
            var categories = _mapper.Map<List<CategoryGetDto>>(_categoryService.TGet());
            var productRegisterViewModel = new ProductRegisterViewModel
            {
                ProductsGet = products,
                Users = users,
                Categories = categories
            };
            return View(productRegisterViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> New(ProductRegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                var register = _mapper.Map<ProductRegistiration>(model.ProductRegisterAdd);
                var regHistory = _mapper.Map<ProductRegistirationHistory>(model.ProductRegisterAdd);

                if (model.ProductRegisterAdd.FilePath != null && model.ProductRegisterAdd.FilePath.Length > 0)
                {
                    var newAssetName = await _productRegisterService.TSaveFile(model.ProductRegisterAdd.FilePath, "productregistration");
                    register.FilePath = newAssetName;
                    regHistory.FilePath = newAssetName;
                }

                _productRegisterService.TInsert(register);
                _productRegisterHistoryService.TInsert(regHistory);
                _toastNotification.AddSuccessToastMessageWithCustomTitle("Zimmet Eklendi");
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                _toastNotification.AddErrorToastMessageWithCustomTitle($"Zimmet eklenemedi {ex}");
                return RedirectToAction("Index");
            }

        }

        public IActionResult Remove(int id, string requestFrom)
        {
            var value = _productRegisterService.TGet(id);
            _productRegisterService.TDelete(value);
            if (requestFrom == "user")
            {
                _toastNotification.AddSuccessToastMessageWithCustomTitle("Zimmet Kaldırıldı");
                return RedirectToAction("Index", "User");
            }
            _toastNotification.AddSuccessToastMessage("Zimmet Kaldırıldı");
            return RedirectToAction("Index");
        }

    }
}
