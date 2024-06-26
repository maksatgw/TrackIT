﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using NToastNotify;
using TrackIT.Business.Abstract;
using TrackIT.DataAccess.Entity;
using TrackIT.DTO.Dtos.CategoryDtos;
using TrackIT.Entity.Model;
using TrackIT.UI.Extensions;
using TrackIT.UI.ViewModels;

namespace TrackIT.UI.Controllers
{
    [Authorize(Roles = "Admin")]
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
            var categories = _mapper.Map<List<CategoryGetDto>>(_categoryService.TGetWithIncluded());
            if (!string.IsNullOrEmpty(searchQuery))
            {
                categories = _mapper.Map<List<CategoryGetDto>>(_categoryService.TGetWithIncludedSearch(searchQuery));
            }
            var categoryViewModel = new CategoryViewModel
            {
                Categories = categories,
            };
            return View(categoryViewModel);
        }
        [HttpPost]
        public IActionResult New(CategoryViewModel model)
        {
            try
            {
                var value = _mapper.Map<Category>(model.CategoryAdd);
                _categoryService.TInsert(value);
                _toastNotification.AddSuccessToastMessageWithCustomTitle($"{value.Name} sisteme eklemiştir");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _toastNotification.AddSuccessToastMessageWithCustomTitle($"Ekleme işlemi sırasında bir hata oluştu.{ex.Message}");
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
                _toastNotification.AddSuccessToastMessageWithCustomTitle($"{value.Name} güncellenmiştir.");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _toastNotification.AddErrorToastMessageWithCustomTitle($"Güncelleme işlemi sırasında bir hata oluştu. {ex.Message}");
                return RedirectToAction("Index");
            }
        }

        public IActionResult Remove(int id)
        {
            try
            {
                var value = _categoryService.TGet(id);
                _categoryService.TDelete(value);
                _toastNotification.AddSuccessToastMessageWithCustomTitle($"{value.Name} Silinmiştir.");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _toastNotification.AddErrorToastMessageWithCustomTitle($"Silme işlemi sırasında bir hata oluştu.{ex.Message}");
                return RedirectToAction("Index");
            }
        }
    }
}
