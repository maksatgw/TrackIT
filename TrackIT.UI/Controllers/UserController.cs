﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TrackIT.Business.Model;
using TrackIT.DTO.Dtos.UserDtos;
using NuGet.Versioning;
using TrackIT.Business.Abstract;
using Azure.Core;
using NToastNotify;

namespace TrackIT.UI.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IProductRegisterService _productRegisterService;
        private readonly IToastNotification _toastNotification;
        private readonly IMapper _mapper;

        public UserController(UserManager<AppUser> userManager, IMapper mapper, IProductRegisterService productRegisterService, IToastNotification toastNotification)
        {
            _userManager = userManager;
            _mapper = mapper;
            _productRegisterService = productRegisterService;
            _toastNotification = toastNotification;
        }

        public IActionResult Index()
        {
            var values = _userManager.Users.Include(x => x.ProductRegistrations).ToList();
            var response = values.Select(x =>
            {
                var userDto = _mapper.Map<UserGetDto>(x);
                userDto.UserProductCount = x.ProductRegistrations?.Count ?? 0;
                return userDto;
            }).ToList();

            return View(response);
        }
        public async Task<IActionResult> Details(string id)
        {
            var user = await _userManager.Users
            .Include(u => u.ProductRegistrations).ThenInclude(pr => pr.Product)
            .FirstOrDefaultAsync(u => u.Id == id);

            var count = _productRegisterService.TGetProductRegisteredUserCount(id);

            if (user == null)
            {
                return NotFound();
            }
            var userDto = new UserGetDto
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                ProductRegistirations = user.ProductRegistrations,
                UserProductCount = count
            };
            return View(userDto);
        }
        [HttpGet]
        public IActionResult New()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> New(UserAddDto model)
        {
            var identityResult = await _userManager.CreateAsync(new AppUser()
            {
                UserName = model.UserName,
                Email = model.Email,
            });

            if (identityResult.Succeeded)
            {
                _toastNotification.AddSuccessToastMessage("Başarılı", new ToastrOptions { Title = "Başarılı" });
                return RedirectToAction("Index");
            }
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Remove(string id)
        {
            var value = await _userManager.FindByIdAsync(id);
            var response = await _userManager.DeleteAsync(value);
            if (response.Succeeded)
            {
                _toastNotification.AddSuccessToastMessage("Başarılı", new ToastrOptions { Title = "Başarılı" });
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

        }
    }
}