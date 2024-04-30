using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TrackIT.Business.Abstract;
using TrackIT.Business.Model;
using TrackIT.UI.Models;

namespace TrackIT.UI.Controllers
{
    public class HomeController : Controller
    {

        private readonly IProductService _productService;
        private readonly IProductRegisterService _registerService;
        private readonly ICategoryService _categoryService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public HomeController(IProductService productService, IProductRegisterService registerService, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, ICategoryService categoryService)
        {
            _productService = productService;
            _registerService = registerService;
            _signInManager = signInManager;
            _userManager = userManager;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            ViewBag.ProductCount = _productService.TGet().Count;
            ViewBag.RegisterCount = _registerService.TGet().Count;
            ViewBag.UserCount = _userManager.Users.Where(x => x.isActive == true).Count();
            ViewBag.CategoryCount = _categoryService.TGet().Count;
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}