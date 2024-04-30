using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TrackIT.Business.Model;
using TrackIT.DTO.Dtos.UserDtos;
using TrackIT.Business.Abstract;
using NToastNotify;
using TrackIT.DTO.Dtos.UserRoleDtos;
using TrackIT.UI.ViewModels;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using TrackIT.UI.Extensions;
using QRCoder;
using System.Drawing;
using TrackIT.Entity.Model;
using TrackIT.DTO.Dtos.ProductRegisterDtos;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Drawing.Imaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace TrackIT.UI.Controllers
{

    public class UserController : Controller
    {
        //Kullanacağımı dependencyleri ekliyoruz.
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
            var query = _userManager.Users.Include(x => x.ProductRegistrations).Where(x => x.isActive == true);

            if (!string.IsNullOrEmpty(searchQuery))
            {
                query = query.Where(x => x.UserName.Contains(searchQuery) || x.Email.Contains(searchQuery));
            }
            var totalUsers = query.Count();
            var totalPages = (int)Math.Ceiling((decimal)totalUsers / pageSize);
            var users = query
                .Select(x => new UserGetDto
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Email = x.Email,
                    UserProductCount = x.ProductRegistrations.Count
                })
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            var userViewModel = new UserViewModel
            {
                AppUsers = users,
                TotalPage = totalPages,
                CurrentPage = page
            };
            return View(userViewModel);
        }

        public async Task<IActionResult> Detail(string id)
        {
            var user = await _userManager.Users
            .Include(u => u.ProductRegistrations).ThenInclude(pr => pr.Product)
            .FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                _toastNotification.AddErrorToastMessageWithCustomTitle("Kullanıcı Bulunamadı");
                return RedirectToAction("Index");
            }
            var count = _productRegisterService.TGetProductRegisteredUserCount(id);
            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault(id);
            var register = _mapper.Map<List<ProductRegisterGetDto>>(user.ProductRegistrations);
            var userDto = new UserGetDto
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                ProductRegistirations = register,
                UserProductCount = count,
                Role = role,
            };
            return View(userDto);
        }

        public async Task<IActionResult> CreateRegisterDocument(string id)
        {
            var user = await _userManager.Users
             .Include(u => u.ProductRegistrations)
             .ThenInclude(pr => pr.Product)
             .FirstOrDefaultAsync(u => u.Id == id);

            var register = _mapper.Map<List<ProductRegisterGetDto>>(user.ProductRegistrations);

            foreach (var item in register)
            {
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(item.ProductId.ToString(), QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(20); // QR kodunun boyutu (20x20 piksel)

                using (MemoryStream stream = new MemoryStream())
                {
                    qrCodeImage.Save(stream, ImageFormat.Png);
                    byte[] imageBytes = stream.ToArray();
                    string base64String = Convert.ToBase64String(imageBytes);
                    var QRData = $"data:image/png;base64,{base64String}";
                    item.QRData = QRData;
                }
            }
            var userDto = new UserGetDto
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                ProductRegistirations = register,
            };

            if (userDto.ProductRegistirations.Count == 0)
            {
                _toastNotification.AddErrorToastMessageWithCustomTitle("Zimmet Bulunamadı");
                return RedirectToAction("Details", new { id = id });
            }
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
                    Name = model.UserAdd.Name,
                    Surname= model.UserAdd.Surname
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
                var identityResult = await _userManager.CreateAsync(user, model.UserAdd.Password);
                foreach (IdentityError item in identityResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                    var roles = _mapper.Map<List<UserRoleGetDto>>(_roleManager.Roles.ToList());
                    model.AppRoles = roles;
                    return View(model);
                }
                var roleAssign = await _userManager.AddToRoleAsync(user, model.UserAdd.RoleName);
                if (identityResult.Succeeded && roleAssign.Succeeded)
                {
                    _toastNotification.AddSuccessToastMessageWithCustomTitle($"Kullanıcı Ekleme Başarılı");
                    return RedirectToAction("Index");
                }
                _toastNotification.AddErrorToastMessageWithCustomTitle("Ekleme İşleminde Bir Hata oluştu");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _toastNotification.AddErrorToastMessageWithCustomTitle($"Ekleme işlemi sırasında bir hata oluştu.{ex.Message}");
                return RedirectToAction("Index");
            }
        }
        #endregion

        #region Update

        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var roles = _mapper.Map<List<UserRoleGetDto>>(_roleManager.Roles.ToList());
                var userDto = _mapper.Map<UserUpdateDto>(user);

                var userViewModel = new UserViewModel
                {
                    UserUpdate = userDto,
                    AppRoles = roles
                };
                return View(userViewModel);
            }
            _toastNotification.AddErrorToastMessageWithCustomTitle("Kullanıcı Bulunamadı");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var existingUser = await _userManager.FindByIdAsync(model.UserUpdate.Id);
            if (existingUser == null)
            {
                _toastNotification.AddErrorToastMessageWithCustomTitle("Kullanıcı Bulunamadı");
                return RedirectToAction("Index");
            }

            existingUser.Email = model.UserUpdate.Email;
            existingUser.UserName = model.UserUpdate.UserName;
            existingUser.Name = model.UserUpdate.Name;
            existingUser.Surname = model.UserUpdate.Surname;
            var newPasswordHash = _userManager.PasswordHasher.HashPassword(existingUser, model.UserUpdate.Password);
            existingUser.PasswordHash = newPasswordHash;

            var updateResult = await _userManager.UpdateAsync(existingUser);
            
            if (!updateResult.Succeeded)
            {
                _toastNotification.AddErrorToastMessageWithCustomTitle("Kullanıcı Güncellenirken Bir Hata Oluştu");
                return RedirectToAction("Index");
            }   
            var getUserRole = _userManager.GetRolesAsync(existingUser);
            var roleToRemove = await _userManager.RemoveFromRoleAsync(existingUser, getUserRole.Result.First());
            var roleToAdd = await _userManager.AddToRoleAsync(existingUser, model.UserUpdate.RoleName);
            if (roleToRemove.Succeeded && roleToAdd.Succeeded)
            {
                _toastNotification.AddSuccessToastMessageWithCustomTitle("Güncelleme Başarılı");
                return RedirectToAction("Index");
            }

            _toastNotification.AddErrorToastMessageWithCustomTitle("Rol Güncellenirken Bir Hata Oluştu");
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
                _toastNotification.AddSuccessToastMessageWithCustomTitle("Kullanıcı Askıya Alındı");
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
