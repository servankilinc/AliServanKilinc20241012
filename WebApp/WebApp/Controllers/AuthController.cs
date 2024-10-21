using Core.Exceptions;
using DataAccess.Abstract;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;
using Model.Models.Auth;
using System.Security.Claims;
using FluentValidation;
using AutoMapper;
using System.Threading;
using WebApp.Utils;

namespace WebApp.Controllers;

public class AuthController : Controller
{
    private readonly IUserRepository _userRepository;
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    private readonly IValidator<UserLoginRequestModel> _validatorLogin;
    private readonly IValidator<UserRegisterRequestModel> _validatorRegister;
    private readonly IMapper _mapper;
    public AuthController(IUserRepository userRepository, UserManager<User> userManager, SignInManager<User> signInManager, IValidator<UserLoginRequestModel> validatorLogin, IValidator<UserRegisterRequestModel> validatorRegister, IMapper mapper, RoleManager<IdentityRole<Guid>> roleManager)
    {
        _userRepository = userRepository;
        _userManager = userManager;
        _signInManager = signInManager;
        _validatorLogin = validatorLogin;
        _validatorRegister = validatorRegister;
        _mapper = mapper;
        _roleManager = roleManager;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(UserLoginRequestModel loginRequestModel)
    {
        var validation = _validatorLogin.Validate(loginRequestModel);
        if (!validation.IsValid)
        {
            validation.AddToModelState(ModelState); 
            return View(loginRequestModel);
        }

        User? existUser = !string.IsNullOrEmpty(loginRequestModel.Email) ?
           await _userRepository.GetAsync(filter: u => u.Email!.ToLower() == loginRequestModel.Email.ToLower()) :
           await _userRepository.GetAsync(filter: u => u.TCKNO == loginRequestModel.TCKNO);

        if (existUser == null)
        {
            ViewBag.Feedback = "Kullanıcı bulunamadı.";
            return View(loginRequestModel);
        }

        bool passwordStatus = await _userManager.CheckPasswordAsync(existUser, loginRequestModel.Password);
        if (!passwordStatus)
        {
            ViewBag.Feedback = "Girdiğiniz şifre doğru değil.";
            return View(loginRequestModel);
        }

        var properties = new AuthenticationProperties() { 
            ExpiresUtc = DateTime.UtcNow.AddMinutes(60),
            AllowRefresh = true,
            IsPersistent = true,
            RedirectUri = "/home/index"
        };
        await _signInManager.SignInAsync(existUser, properties); 
 
        ViewBag.Feedback = "Giriş işlemi başarısız, lütfen tekrar deneyin.";
        return View(loginRequestModel);
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(UserRegisterRequestModel registerRequestModel)
    {
        var validation = _validatorRegister.Validate(registerRequestModel);
        if (!validation.IsValid)
        {
            validation.AddToModelState(ModelState);
            return View(registerRequestModel);
        }

        User userToRegister = _mapper.Map<User>(registerRequestModel);
        userToRegister.RegistrationDate = DateTime.Now;
        userToRegister.UserName = $"{registerRequestModel.Email}_{DateTime.UtcNow.ToString()}";

        var resultRegister = await _userManager.CreateAsync(userToRegister, registerRequestModel.Password);
        if (!resultRegister.Succeeded)
        {
            ViewBag.Feedback = "İşlem sırasında bir sorun oluştur.";
            return View(registerRequestModel);
        }
        bool isRoleUserExist = await _roleManager.RoleExistsAsync("User");
        if (!isRoleUserExist) await _roleManager.CreateAsync(new IdentityRole<Guid>("User"));
        var resultRole = await _userManager.AddToRoleAsync(userToRegister, "User");
        
        User regisiretedUser = await _userRepository.GetAsync(filter: u => u.TCKNO == registerRequestModel.TCKNO, cancellationToken: new CancellationToken());

        var properties = new AuthenticationProperties()
        {
            AllowRefresh = true,
            IsPersistent = true,
            RedirectUri = "/home/index"
        };
        await _signInManager.SignInAsync(regisiretedUser, properties);

        ViewBag.Feedback = "İşlem sırasında bir sorun oluştur.";
        return View(registerRequestModel);
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login");
    }
}
