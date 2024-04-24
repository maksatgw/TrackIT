using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using TrackIT.Business.Abstract;

namespace TrackIT.UI.Controllers
{
    public class ProductRegisterController : Controller
    {
        private readonly IProductRegisterService _productRegisterService;
        private readonly IToastNotification _toastNotification;

        public ProductRegisterController(IProductRegisterService productRegisterService, IToastNotification toastNotification)
        {
            _productRegisterService = productRegisterService;
            _toastNotification = toastNotification;
        }

        public IActionResult Index()
        {
            return View();
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
