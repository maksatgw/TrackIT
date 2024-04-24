using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NToastNotify;
using TrackIT.Business.Abstract;
using TrackIT.DTO.Dtos.CategoryDtos;
using TrackIT.DTO.Dtos.ProductDtos;
using TrackIT.Entity.Model;
using TrackIT.UI.ViewModels;

namespace TrackIT.UI.Controllers
{
    public class ProductController : Controller
    {
        //Dependency Injection Design Pattern'i kullanarak bağımlılıklarımızı dışardan enjekte ettik.
        //Böyleliklie, her bir nesne için new anahtar kelimesi kullanmaktan kaçınarak kodlarımızı daha temiz hale getirdik.
        //DI Cotainer ile de, gerekli durumlarda üretilecek nesne üzerinde kontrol sahibi olduğumuz için performans artışı da yaşadık.
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly IToastNotification _toastNotification;

        public ProductController(IProductService productService, IMapper mapper, ICategoryService categoryService, IToastNotification toastNotification)
        {
            _productService = productService;
            _categoryService = categoryService;
            _mapper = mapper;
            _toastNotification = toastNotification;
        }

        //Filtre nesnelerini query string üzerinden alıyoruz.
        public IActionResult Index(string searchQuery, int filterByCategory)
        {
            //Index benden bir viewmodel bekliyor.
            //Caselerime göre gerekli eklemeler ile paketi View'a gönderiyorum.
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
            //Category Indexlemesi 0 dan başlamıyor. 
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

        #region Yeni
        [HttpGet]
        public IActionResult New()
        {
            //Sadece kategorileri gönderiyorum.
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
                return View(model);
            }

            var value = _mapper.Map<Product>(model.ProductAdd);
            _productService.TInsert(value);
            _toastNotification.AddSuccessToastMessage($"{value.Name} Sisteme Eklenmiştir", new ToastrOptions { Title = "Başarılı" });
            return RedirectToAction("Index");
        }
        #endregion
        [HttpGet]
        public IActionResult Update(int id) 
        {
            var categoryDtos = _mapper.Map<List<CategoryGetDto>>(_categoryService.TGet());
            var productDtos = _mapper.Map<ProductUpdateDto>(_productService.TGet(id));
            var productViewModel = new ProductViewModel()
            {
                Categories = categoryDtos,
                ProductUpdate = productDtos
            };
            return View(productViewModel);
        }

        [HttpPost]
        public IActionResult Update(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var value = _mapper.Map<Product>(model.ProductUpdate);
            _productService.TUpdate(value);
            _toastNotification.AddSuccessToastMessage($"{value.Name} Güncellenmiştir.", new ToastrOptions { Title = "Başarılı" });
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int productId)
        {
            var value = _productService.TGet(productId);
            _productService.TDelete(value);
            _toastNotification.AddSuccessToastMessage($"{value.Name} Sisteme Eklenmiştir", new ToastrOptions { Title = "Başarılı" });
            return RedirectToAction("Index");
        }

    }
}
