using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Dtos.User_;
using Model.Entities;
using Model.Models.Transfer_;
using System.Security.Claims;
using WebApp.Utils;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly IValidator<UserUpdateDto> _validatorUserUpdate;

        public UserController(IUserService userService, IValidator<UserUpdateDto> validatorUserUpdate, UserManager<User> userManager, IUserRepository userRepository)
        {
            _userService = userService;
            _validatorUserUpdate = validatorUserUpdate;
            _userManager = userManager;
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Index()
        {
            Claim? nameId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (nameId == null) return RedirectToAction("Login", "Auth");

            var result = await _userService.GetUserAsync(Guid.Parse(nameId.Value), new CancellationToken());

            return View(result);
        }

        public async Task<IActionResult> EditInformations()
        {
            Claim? nameId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (nameId == null) return RedirectToAction("Login", "Auth");

            var user = await _userService.GetUserAsync(Guid.Parse(nameId.Value), new CancellationToken());
            UserUpdateDto updateModel = new()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id,
            };
            return View(updateModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditInformations(UserUpdateDto updateDto)
        {
            Claim? nameId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (nameId == null) return RedirectToAction("Login", "Auth");

            updateDto.Id = Guid.Parse(nameId.Value);

            var validation = _validatorUserUpdate.Validate(updateDto);
            if (!validation.IsValid)
            {
                validation.AddToModelState(ModelState);
                return View(updateDto);
            }

            await _userService.UpdateUserAsync(updateDto, new CancellationToken());

            return RedirectToAction("Index", "User");
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(VMChangePassword changePasswordModel)
        {
            if(changePasswordModel.NewPassword != changePasswordModel.NewPasswordAgain) 
                ModelState.AddModelError("NewPasswordAgain", "Yeni şifre ile uyuşmamaktadır lütfen kontrol ediniz.");
            if(changePasswordModel.OldPassword == changePasswordModel.NewPassword) 
                ModelState.AddModelError("NewPassword", "Eski şifrenizi girdiniz lütfen değiştiriniz.");

            if(!ModelState.IsValid)
            {
                return View(changePasswordModel);
            }

            Claim? nameId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (nameId == null) return RedirectToAction("Login", "Auth");
            var existUser = await _userRepository.GetAsync(filter: u => u.Id == Guid.Parse(nameId.Value));
            bool passwordStatus = await _userManager.CheckPasswordAsync(existUser,changePasswordModel.OldPassword);
            if (!passwordStatus)
            {
                ViewBag.Feedback = "Girdiğiniz şifre doğru değil.";
                return View(changePasswordModel);
            }

            var result = await _userManager.ChangePasswordAsync(existUser, changePasswordModel.OldPassword, changePasswordModel.NewPassword);
            if(!result.Succeeded) {
                ViewBag.Feedback = "İşlem başarısız oldu, lütfen tekrar deneyin.";
                return View(changePasswordModel);
            }
            return RedirectToAction("Logout", "Auth");
        }

        public async Task<IActionResult> FindUserAndAccount(string accountNo)
        {
            if (string.IsNullOrWhiteSpace(accountNo)) throw new ArgumentNullException(nameof(accountNo));
            var result = await _userService.FindUserByAccountNoAsync(accountNo, new CancellationToken());
            return Ok(result);
        }
    }
}
