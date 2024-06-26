﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.IdentityModel.Tokens;
using NToastNotify;
using TrackIT.Business.Abstract;
using TrackIT.DTO.Dtos.CategoryDtos;
using TrackIT.DTO.Dtos.LocationDtos;
using TrackIT.DTO.Dtos.ProductAssetDtos;
using TrackIT.DTO.Dtos.ProductDtos;
using TrackIT.DTO.Dtos.ProductRegisterDtos;
using TrackIT.DTO.Dtos.ProductRegisterHistoryDtos;
using TrackIT.Entity.Model;
using TrackIT.UI.Extensions;
using TrackIT.UI.ViewModels;

namespace TrackIT.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        //Dependency Injection Design Pattern'i kullanarak bağımlılıklarımızı dışardan enjekte ettik.
        //Böyleliklie, her bir nesne için new anahtar kelimesi kullanmaktan kaçınarak kodlarımızı daha temiz hale getirdik.
        //DI Cotainer ile de, gerekli durumlarda üretilecek nesne üzerinde kontrol sahibi olduğumuz için performans artışı da yaşadık.
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IProductRegisterHistoryService _productRegisterHistoryService;
        private readonly IProductAssetService _productAssetService;
        private readonly ILocationService _locationService;
        private readonly IMapper _mapper;
        private readonly IToastNotification _toastNotification;

        public ProductController(IProductService productService, IMapper mapper, ICategoryService categoryService, IToastNotification toastNotification, IProductRegisterHistoryService productRegisterHistoryService, IProductAssetService productAssetService, ILocationService locationService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _mapper = mapper;
            _toastNotification = toastNotification;
            _productRegisterHistoryService = productRegisterHistoryService;
            _productAssetService = productAssetService;
            _locationService = locationService;
        }

        #region Getirme
        //Filtre nesnelerini query string üzerinden alıyoruz.
        public IActionResult Index(string searchQuery,int filterByLocation, int filterByCategory, int pageSize = 10, int page = 1)
        {
            //Index benden bir viewmodel bekliyor.
            //Caselerime göre gerekli eklemeler ile paketi View'a gönderiyorum.
            var products = _mapper.Map<List<ProductGetDto>>(_productService.TGetWithIncluded(page, pageSize));
            var categories = _mapper.Map<List<CategoryGetDto>>(_categoryService.TGet());
            var locations = _mapper.Map<List<LocationGetDto>>(_locationService.TGet());
            var geProductCount = _productService.TGet();
            var totalPage = (int)Math.Ceiling((decimal)geProductCount.Count / pageSize);

            if (!string.IsNullOrEmpty(searchQuery))
            {
                var productSearchResult = _productService.TGetWithIncludedSearch(searchQuery);
                products = _mapper.Map<List<ProductGetDto>>(productSearchResult);
            }
            //Category Indexlemesi 0 dan başlamıyor. 
            else if (filterByCategory != 0)
            {
                var productFiterByCategory = _productService.TGetByCategory(filterByCategory);
                products = _mapper.Map<List<ProductGetDto>>(productFiterByCategory);
            }
            else if (filterByLocation != 0)
            {
                var productFiterByLocation = _productService.TGetByCategory(filterByLocation);
                products = _mapper.Map<List<ProductGetDto>>(productFiterByLocation);
            }

            var productViewModel = new ProductViewModel
            {
                Products = products,
                Categories = categories,
                Locations = locations,
                CurrentPage = page,
                TotalPage = totalPage,
            };
            return View(productViewModel);
        }
        public IActionResult Detail(int id)
        {
            //önce gelen ürünün varlığını kontrol ediyoruz. 
            var hasProduct = _mapper.Map<ProductGetDto>(_productService.TGet(id));
            var product = _mapper.Map<List<ProductRegisterHistoryGetDto>>(_productRegisterHistoryService.TGetWithIncluded(id));
            //Eğer null ise Toast Mesajı ile kullanıcıya hata gönderip Index'e yönlendiriyoruz.
            if (hasProduct == null)
            {
                _toastNotification.AddErrorToastMessageWithCustomTitle("Ürün bulunamadı");
                return RedirectToAction("Index");
            }
            return View(product);
        }
        #endregion

        #region Yeni
        [HttpGet]
        public IActionResult New()
        {
            //Kategorileri ekleme sayfasında select list olarak alacağım için viewmodel üzerine ekleyip view'a gönderiyorum.
            var categories = _mapper.Map<List<CategoryGetDto>>(_categoryService.TGet());
            var locations = _mapper.Map<List<LocationGetDto>>(_locationService.TGet());
            var productViewModel = new ProductViewModel()
            {
                Locations = locations,
                Categories = categories,
            };
            return View(productViewModel);
        }

        [HttpPost]
        //Ürünler birden fazla Asset alacağı için List form file olarak alıyoruz.
        public IActionResult New(ProductViewModel model, List<IFormFile> AssetUrls)
        {
            //ModelState durumu valid değilse state içindeki hataları gösterir.
            //Select List null gelmesin ve hata mesajları akabinde gönderilmeye devam edilebilsin diye kategorileri gönderiyoruz.
            if (!ModelState.IsValid)
            {
                var categories = _mapper.Map<List<CategoryGetDto>>(_categoryService.TGet());
                model.Categories = categories;
                return View(model);
            }
            try
            {
                //ekleme işleminde birden fazla tabloya ekleme yapacağız.
                //modelden gelen productadd dto sınıfını Product classına mapleyerek insert ediyoruz.
                var product = _mapper.Map<Product>(model.ProductAdd);
                _productService.TInsert(product);
                //AssetUrls içindeki her bir item için
                foreach (var item in AssetUrls)
                {
                    //item geldiyse
                    if (item != null && item.Length > 0)
                    {
                        //image işlemleri
                        var extension = Path.GetExtension(item.FileName);
                        var newAsset = Guid.NewGuid() + extension;
                        var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/product/", newAsset);

                        using (var stream = new FileStream(location, FileMode.Create))
                        {
                            item.CopyTo(stream);
                        }
                        //AssetDto'ya gelen dataları bind edeceğiz.
                        //her bir item için birden fazla kez yapacak bunu.
                        var assets = new ProductAssetAddDto
                        {
                            AssetUrl = newAsset,
                            ProductId = product.ProductId
                        };
                        //assetadddto nesnesini productassete mapleyerek ekleme işlemini bitiriyoruz.
                        var asset = _mapper.Map<ProductAsset>(assets);
                        _productAssetService.TInsert(asset);
                    }
                }
                //her şey yolunda giderse kullanıcıya mesaj ile indexe yönlendirme yapacağız.
                _toastNotification.AddSuccessToastMessageWithCustomTitle($"{product.Name} Sisteme Eklenmiştir");
                return RedirectToAction("Index");
            }
            //işler yolunda gitmez ise hata mesajnı ekrana basıp indexe yönlendiriyoruz.
            catch (Exception ex)
            {
                _toastNotification.AddErrorToastMessageWithCustomTitle($"Ekleme işlemi sırasında bir hata oluştu.{ex.Message}");
                return RedirectToAction("Index");
            }
        }

        #endregion

        #region Güncelle
        //Güncelleme
        [HttpGet]
        public IActionResult Update(int id)
        {
            //ürün - kategori - asset listelerini mapleyip gönderiyoruz.
            var product = _mapper.Map<ProductUpdateDto>(_productService.TGet(id));
            var categories = _mapper.Map<List<CategoryGetDto>>(_categoryService.TGet());
            var locations = _mapper.Map<List<LocationGetDto>>(_locationService.TGet());
            var assets = _mapper.Map<List<ProductAssetGetDto>>(_productAssetService.TGetWithIncluded(id));
            //product eğer yoksa
            if (product == null)
            {
                //kullacıya bildir ve indexe gönder
                _toastNotification.AddErrorToastMessageWithCustomTitle("Ürün bulunamadı");
                return RedirectToAction("Index");
            }
            //hata yoksa viewModele ata ve view'a gönder
            var productViewModel = new ProductViewModel()
            {
                Categories = categories,
                Locations = locations,
                ProductUpdate = product,
                ProductAssets = assets
            };
            return View(productViewModel);
        }

        [HttpPost]
        public IActionResult Update(ProductViewModel model, List<IFormFile> AssetUrls)
        {
            //ModelState durumu valid değilse state içindeki hataları gösterir.
            //Select List null gelmesin ve hata mesajları akabinde gönderilmeye devam edilebilsin diye kategorileri gönderiyoruz.
            if (!ModelState.IsValid)
            {
                var categories = _mapper.Map<List<CategoryGetDto>>(_categoryService.TGet());
                model.Categories = categories;
                return View(model);
            }
            try
            {
                //productu maple ve update'ini yap.
                var product = _mapper.Map<Product>(model.ProductUpdate);
                _productService.TUpdate(product);
                //sonrasında gönderilen assetleri asset listesine ekle
                //var olan resmi update etmek yerine ben listeye yeni asset ekletiyorum
                //kullanıcı resmi update etmek yerine listedekileri silebilir.
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
                            ProductId = product.ProductId
                        };
                        var productAsset = _mapper.Map<ProductAsset>(assets);
                        _productAssetService.TInsert(productAsset);
                    }
                }
                _toastNotification.AddSuccessToastMessageWithCustomTitle($"{product.Name} Güncellenmiştir.");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _toastNotification.AddErrorToastMessageWithCustomTitle($"Güncelleme işlemi sırasında bir hata oluştu.{ex.Message}");
                return RedirectToAction("Index");
            }
        }
        #endregion

        #region Silme
        //ürün içinden asset kaldırmak.
        //asset kaldırmak için hem assetId hem de productId yi getiriyoruz.
        public IActionResult RemoveAsset(int assetId, int productId)
        {
            try
            {
                //asset ve productları idlerine göre getiriyoruz.
                var asset = _productAssetService.TGet(assetId);
                var product = _productService.TGet(productId);
                //eğer ikisinden biri null gelir ise.
                if (asset == null || product == null)
                {
                    //kullanıcıya mesajı göster ve indexe yönlendir.
                    _toastNotification.AddErrorToastMessageWithCustomTitle("Güncelleme işlemi sırasında bir hata oluştu.");
                    return RedirectToAction("Index");
                }
                //gelmez ise asseti sil ve mesajı kullanıcıya göster.
                _productAssetService.TDelete(asset);
                _toastNotification.AddSuccessToastMessageWithCustomTitle($"{product.Name} Güncellenmiştir.");
                //her şey yolunda ise Update'e gönderiyoruz ki kullanıcı işlemine devam edebilsin.
                return RedirectToAction("Update", new { id = productId });
            }
            catch (Exception ex)
            {
                _toastNotification.AddErrorToastMessageWithCustomTitle($"Güncelleme işlemi sırasında bir hata oluştu.{ex.Message}");
                return RedirectToAction("Index");
            }

        }

        //ürünü kaldırmak.
        public IActionResult Remove(int productId)
        {
            try
            {
                //ürünğ bulalım
                var product = _productService.TGet(productId);
                //eğer ürün bulunamazsa
                if (product == null)
                {
                    //kullanıcıya mesajı göster ve indexe yönlendir.
                    _toastNotification.AddErrorToastMessage("Silinecek Öğe Bulunamadı", new ToastrOptions { Title = "Hata" });
                    return RedirectToAction("Index");
                }
                _productService.TDelete(product);
                _toastNotification.AddSuccessToastMessageWithCustomTitle($"{product.Name} Silinmiştir.");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _toastNotification.AddErrorToastMessageWithCustomTitle($"Silme işlemi sırasında bir hata oluştu.{ex.Message}");
                return RedirectToAction("Index");
            }
        }
        #endregion

    }
}
