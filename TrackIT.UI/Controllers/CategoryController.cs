using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using NToastNotify;
using TrackIT.Business.Abstract;
using TrackIT.DataAccess.Entity;
using TrackIT.DTO.Dtos.CategoryDtos;
using TrackIT.Entity.Model;
using TrackIT.UI.ViewModels;

namespace TrackIT.UI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IToastNotification _toastNotification;
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService, IMapper mapper, IToastNotification toastNotification)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _toastNotification = toastNotification;
        }

        public IActionResult Index(string searchQuery)
        {
            var values = _mapper.Map<List<CategoryGetDto>>(_categoryService.TGetWithIncluded());
            var categoryViewModel = new CategoryViewModel
            {
                Categories = values,
            };

            if (!string.IsNullOrEmpty(searchQuery))
            {
                values = _mapper.Map<List<CategoryGetDto>>(_categoryService.TGetWithIncludedSearch(searchQuery));
                categoryViewModel.Categories = values;
            }
            return View(categoryViewModel);
        }
        [HttpPost]
        public IActionResult New(CategoryViewModel model)
        {
            try
            {
                var value = _mapper.Map<Category>(model.CategoryAdd);
                _categoryService.TInsert(value);
                _toastNotification.AddSuccessToastMessage($"{value.Name} sisteme eklemiştir", new ToastrOptions { Title = "Başarılı" });
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _toastNotification.AddErrorToastMessage($"Ekleme işlemi sırasında bir hata oluştu.{ex.Message}", new ToastrOptions { Title = "Hata" });
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Update(CategoryViewModel model)
        {
            try
            {
                var value = _mapper.Map<Category>(model.CategoryUpdate);
                _categoryService.TUpdate(value);
                _toastNotification.AddSuccessToastMessage($"{value.Name} güncellenmiştir.", new ToastrOptions { Title = "Başarılı" });
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _toastNotification.AddErrorToastMessage($"Güncelleme işlemi sırasında bir hata oluştu. {ex.Message}", new ToastrOptions { Title = "Hata" });
                return RedirectToAction("Index");
            }
        }

        public IActionResult Remove(int id)
        {
            try
            {
                var value = _categoryService.TGet(id);
                _categoryService.TDelete(value);
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
