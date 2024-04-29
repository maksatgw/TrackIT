using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TrackIT.Business.Model;
using TrackIT.UI.ViewModels;

namespace TrackIT.UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public LoginController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel request, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            returnUrl = returnUrl ?? Url.Action("Index", "Home");
            var hasUser = await _userManager.FindByEmailAsync(request.Email);
            if (hasUser.isActive == false)
            {
                ModelState.AddModelError(string.Empty, "Kullanıcı Aktif Değil");
                return View();
            }
            if (hasUser == null)
            {
                ModelState.AddModelError(string.Empty, "Kullanıcı adı veya şifre yanlış");
                return View();
            }
           
            var result = await _signInManager.PasswordSignInAsync(hasUser, request.Password, false, false);
            if (result.Succeeded)
            {
                return Redirect(returnUrl);
            }
            ModelState.AddModelError(string.Empty, "Kullanıcı adı veya şifre yanlış");
            return View();
        }
    }
}
