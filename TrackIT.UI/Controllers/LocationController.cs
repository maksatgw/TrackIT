using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using TrackIT.Business.Abstract;
using TrackIT.DTO.Dtos.LocationDtos;
using TrackIT.Entity.Model;
using TrackIT.UI.Extensions;
using TrackIT.UI.ViewModels;

namespace TrackIT.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class LocationController : Controller
    {
        private readonly ILocationService _locationService;
        private readonly IMapper _mapper;
        private readonly IToastNotification _toastNotification;

        public LocationController(ILocationService locationService, IMapper mapper, IToastNotification toastNotification)
        {
            _locationService = locationService;
            _mapper = mapper;
            _toastNotification = toastNotification;
        }

        public IActionResult Index  (string searchQuery)
        {
            var locations = _locationService.TGetWithIncluded();
            if (!string.IsNullOrEmpty(searchQuery))
            {
                locations = _locationService.TGetWithIncluded(searchQuery);
            }

            var map = _mapper.Map<List<LocationGetDto>>(locations);
            var locationViewModel = new LocationViewModel
            {
                Locations = map
            };
            return View(locationViewModel);
        }

        [HttpPost]
        public IActionResult New(LocationViewModel model)
        {
            try
            {
                var value = _mapper.Map<Location>(model.LocationAdd);
                _locationService.TInsert(value);
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
        public IActionResult Update(LocationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            try
            {
                var value = _mapper.Map<Location>(model.LocationUpdate);
                _locationService.TUpdate(value);
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
                var value = _locationService.TGet(id);
                _locationService.TDelete(value);
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
