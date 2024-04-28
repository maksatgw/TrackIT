using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TrackIT.Business.Model;
using TrackIT.DTO.Dtos.UserDtos;
using NuGet.Versioning;
using TrackIT.Business.Abstract;
using Azure.Core;
using NToastNotify;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TrackIT.DTO.Dtos.UserRoleDtos;
using TrackIT.UI.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TrackIT.UI.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IProductRegisterService _productRegisterService;
        private readonly IToastNotification _toastNotification;
        private readonly IMapper _mapper;

        public UserController(UserManager<AppUser> userManager, IMapper mapper, IProductRegisterService productRegisterService, IToastNotification toastNotification, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _productRegisterService = productRegisterService;
            _toastNotification = toastNotification;
            _roleManager = roleManager;
        }

        public IActionResult Index(string searchQuery, int page = 1, int pageSize = 8)
        {
            var users = _userManager.Users.Include(x => x.ProductRegistrations)
                .Where(x => x.isActive == true)
                .ToList();

            var totalPage = (int)Math.Ceiling((decimal)users.Count / pageSize);
            if (!string.IsNullOrEmpty(searchQuery))
            {
                users = _userManager.Users.Include(x => x.ProductRegistrations)
                    .Where(x => x.UserName.Contains(searchQuery) || x.Email.Contains(searchQuery))
                    .ToList();
            }
            var response = users.Select(x =>
            {
                var userDto = _mapper.Map<UserGetDto>(x);
                userDto.UserProductCount = x.ProductRegistrations?.Count ?? 0;
                return userDto;
            }).Skip((page - 1) * pageSize).Take(pageSize).ToList();


            var userViewModel = new UserViewModel
            {
                AppUsers = response,
                TotalPage = totalPage,
                CurrentPage = page
            };
            return View(userViewModel);
        }
        public async Task<IActionResult> Details(string id)
        {
            var user = await _userManager.Users
            .Include(u => u.ProductRegistrations).ThenInclude(pr => pr.Product)
            .FirstOrDefaultAsync(u => u.Id == id);

            var count = _productRegisterService.TGetProductRegisteredUserCount(id);
            var roles = await _userManager.GetRolesAsync(user);

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
                UserProductCount = count,
                Roles = roles.ToList(),
            };
            return View(userDto);
        }


        #region New

        [HttpGet]
        public IActionResult New()
        {
            var roles = _mapper.Map<List<UserRoleGetDto>>(_roleManager.Roles.ToList());
            var userViewModel = new UserViewModel
            {
                AppRoles = roles
            };
            return View(userViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> New(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var roles = _mapper.Map<List<UserRoleGetDto>>(_roleManager.Roles.ToList());
                model.AppRoles = roles;
                return View(model);
            }
            try
            {
                var user = new AppUser
                {
                    UserName = model.UserAdd.UserName,
                    Email = model.UserAdd.Email,
                };
                var findRole = await _roleManager.FindByNameAsync(model.UserAdd.RoleName);
                var checkUserName = await _userManager.FindByNameAsync(model.UserAdd.UserName);
                var checkEmail = await _userManager.FindByEmailAsync(model.UserAdd.Email);
                if (findRole == null)
                {
                    ModelState.AddModelError("", "Failed to assign role to user.");
                    return View(model);
                }
                if (checkUserName != null || checkEmail != null)
                {
                    ModelState.AddModelError("", "Kullanıcı Adı veya Email Sistemde Mevcut");
                    var roles = _mapper.Map<List<UserRoleGetDto>>(_roleManager.Roles.ToList());
                    model.AppRoles = roles;
                    return View(model);
                }
                var identityResult = await _userManager.CreateAsync(user);
                var roleAssign = await _userManager.AddToRoleAsync(user, model.UserAdd.RoleName);
                if (identityResult.Succeeded && roleAssign.Succeeded)
                {
                    _toastNotification.AddSuccessToastMessage("Başarılı", new ToastrOptions { Title = "Başarılı" });
                    return RedirectToAction("Index");
                }
                _toastNotification.AddErrorToastMessage($"Ekleme işlemi sırasında bir hata oluştu", new ToastrOptions { Title = "Hata" });
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _toastNotification.AddErrorToastMessage($"Ekleme işlemi sırasında bir hata oluştu.{ex.Message}", new ToastrOptions { Title = "Hata" });
                return RedirectToAction("Index");
            }
        }
        #endregion

        #region Update

        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            var value = await _userManager.FindByIdAsync(id);
            if (value != null)
            {
                var response = _mapper.Map<UserUpdateDto>(value);
                return View(response);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserUpdateDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var existingUser = await _userManager.FindByIdAsync(model.Id);
            if (existingUser == null)
            {
                _toastNotification.AddErrorToastMessage("Hata", new ToastrOptions { Title = "Kullanıcı Bulunamadı" });
                return RedirectToAction("Index");
            }
            existingUser.Email = model.Email;
            existingUser.UserName = model.UserName;
            var response = await _userManager.UpdateAsync(existingUser);
            if (response.Succeeded)
            {
                _toastNotification.AddSuccessToastMessage("Başarılı", new ToastrOptions { Title = "Başarılı" });
                return RedirectToAction("Index");
            }
            _toastNotification.AddErrorToastMessage("Hata", new ToastrOptions { Title = "Başarısız" });
            return RedirectToAction("Index");
        }
        #endregion

        public async Task<IActionResult> Remove(string id)
        {
            var value = await _userManager.FindByIdAsync(id);
            value.isActive = false;
            var response = await _userManager.UpdateAsync(value);
            var register = _productRegisterService.TGetByUserId(id);
            if (register != null)
            {
                _productRegisterService.TDelete(register);
            }

            if (response.Succeeded)
            {
                _toastNotification.AddSuccessToastMessage("Başarılı", new ToastrOptions { Title = "Başarılı" });
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
