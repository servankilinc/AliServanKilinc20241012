using Business.Abstract;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Model.Models.Auth;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IValidator<UserLoginRequestModel> _userLoginValidator;
    private readonly IValidator<UserRegisterRequestModel> _userRegisterValidator;
    public AuthController(IAuthService authService, IValidator<UserLoginRequestModel> userLoginValidator, IValidator<UserRegisterRequestModel> userRegisterValidator)
    {
        _authService = authService;
        _userLoginValidator = userLoginValidator;
        _userRegisterValidator = userRegisterValidator;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(UserLoginRequestModel loginRequest)
    {
        _userLoginValidator.ValidateAndThrow(loginRequest);
        UserLoginResponseModel responseModel = await _authService.Login(loginRequest, new CancellationToken());
        return Ok(responseModel);
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(UserRegisterRequestModel registerRequest)
    {
        _userRegisterValidator.ValidateAndThrow(registerRequest);
        UserRegisterResponseModel responseModel = await _authService.Register(registerRequest, new CancellationToken());
        return Ok(responseModel);
    }
}
