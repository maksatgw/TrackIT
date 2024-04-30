using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using TrackIT.Business.Model;
using TrackIT.UI.Extensions;
using TrackIT.UI.ViewModels;

namespace TrackIT.UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IToastNotification _toastNotification;

        public LoginController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IToastNotification toastNotification)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _toastNotification = toastNotification;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel request, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var hasUser = await _userManager.FindByEmailAsync(request.Email);
            if (hasUser == null)
            {
                _toastNotification.AddErrorToastMessageWithCustomTitle("Kullanıcı adı veya şifre yanlış");
                return View();
            }

            if (!hasUser.isActive)
            {
                _toastNotification.AddErrorToastMessageWithCustomTitle("Kullanıcı durumu aktif değil");
                return View();
            }

            returnUrl = returnUrl ?? Url.Action("Index", "Home");

            var result = await _signInManager.PasswordSignInAsync(hasUser, request.Password, false, false);
            if (result.Succeeded)
            {
                _toastNotification.AddSuccessToastMessageWithCustomTitle($"Hoş geldin {hasUser.Name}");
                var roles = await _userManager.GetRolesAsync(hasUser);
                if (roles.Contains("Kullanici"))
                {
                    return RedirectToAction("SignedUsersRegister", "ProductRegister", new { id = hasUser.Id });
                }
                else
                {
                    return Redirect(returnUrl);
                }
            }
            _toastNotification.AddErrorToastMessageWithCustomTitle("Kullanıcı adı veya şifre yanlış");
            ModelState.AddModelError(string.Empty, "Kullanıcı adı veya şifre yanlış");
            return View();
        }

    }
}
