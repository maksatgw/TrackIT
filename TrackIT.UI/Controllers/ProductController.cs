using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NToastNotify;
using TrackIT.Business.Abstract;
using TrackIT.DTO.Dtos.CategoryDtos;
using TrackIT.DTO.Dtos.ProductAssetDtos;
using TrackIT.DTO.Dtos.ProductDtos;
using TrackIT.DTO.Dtos.ProductRegisterDtos;
using TrackIT.DTO.Dtos.ProductRegisterHistoryDtos;
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
        private readonly IProductRegisterHistoryService _productRegisterHistoryService;
        private readonly IProductAssetService _productAssetService;
        private readonly IMapper _mapper;
        private readonly IToastNotification _toastNotification;

        public ProductController(IProductService productService, IMapper mapper, ICategoryService categoryService, IToastNotification toastNotification, IProductRegisterHistoryService productRegisterHistoryService, IProductAssetService productAssetService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _mapper = mapper;
            _toastNotification = toastNotification;
            _productRegisterHistoryService = productRegisterHistoryService;
            _productAssetService = productAssetService;
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
        public IActionResult Detail(int id)
        {
            var value = _mapper.Map<ProductGetDto>(_productService.TGet(id));
            var values = _mapper.Map<List<ProductRegisterHistoryGetDto>>(_productRegisterHistoryService.TGetWithIncluded(id));
            if (value == null)
            {
                _toastNotification.AddErrorToastMessage($"Ürün bulunamadı", new ToastrOptions { Title = "Hata" });
                return RedirectToAction("Index");
            }
            return View(values);
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
        public IActionResult New(ProductViewModel model, List<IFormFile> AssetUrls)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                var value = _mapper.Map<Product>(model.ProductAdd);
                _productService.TInsert(value);
                foreach (var item in AssetUrls)
                {
                    if (item != null && item.Length > 0)
                    {
                        var extension = Path.GetExtension(item.FileName);
                        var newAsset = Guid.NewGuid() + extension;
                        var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/product/", newAsset);

                        using (var stream = new FileStream(location, FileMode.Create))
                        {
                            item.CopyTo(stream);
                        }
                        var assets = new ProductAssetAddDto
                        {
                            AssetUrl = newAsset,
                            ProductId = value.ProductId
                        };
                        var productAsset = _mapper.Map<ProductAsset>(assets);
                        _productAssetService.TInsert(productAsset);
                    }
                }
                _toastNotification.AddSuccessToastMessage($"{value.Name} Sisteme Eklenmiştir", new ToastrOptions { Title = "Başarılı" });
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _toastNotification.AddErrorToastMessage($"Ekleme işlemi sırasında bir hata oluştu.{ex.Message}", new ToastrOptions { Title = "Hata" });
                return RedirectToAction("Index");
            }
        }

        #endregion
        [HttpGet]
        public IActionResult Update(int id)
        {
            var categoryDtos = _mapper.Map<List<CategoryGetDto>>(_categoryService.TGet());
            var productDtos = _mapper.Map<ProductUpdateDto>(_productService.TGet(id));
            if (productDtos == null)
            {
                _toastNotification.AddErrorToastMessage($"Ürün bulunamadı", new ToastrOptions { Title = "Hata" });
                return RedirectToAction("Index");
            }
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
            try
            {
                var value = _mapper.Map<Product>(model.ProductUpdate);
                _productService.TUpdate(value);
                _toastNotification.AddSuccessToastMessage($"{value.Name} Güncellenmiştir.", new ToastrOptions { Title = "Başarılı" });
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _toastNotification.AddErrorToastMessage($"Güncelleme işlemi sırasında bir hata oluştu.{ex.Message}", new ToastrOptions { Title = "Hata" });
                return RedirectToAction("Index");
            }
        }
        public IActionResult Remove(int productId)
        {
            try
            {
                var value = _productService.TGet(productId);
                _productService.TDelete(value);
                _toastNotification.AddSuccessToastMessage($"{value.Name} Silinmiştir.", new ToastrOptions { Title = "Başarılı" });
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _toastNotification.AddErrorToastMessage($"Silme işlemi sırasında bir hata oluştu.{ex.Message}", new ToastrOptions { Title = "Hata" });
                return RedirectToAction("Index");
            }
        }

    }
}
