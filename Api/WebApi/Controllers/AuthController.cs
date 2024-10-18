using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Model.Models.Auth;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService) => _authService = authService;

    [HttpPost("Login")]
    public async Task<IActionResult> Login(UserLoginRequestModel loginRequest)
    {
        UserLoginResponseModel responseModel = await _authService.Login(loginRequest, new CancellationToken());
        return Ok(responseModel);
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(UserRegisterRequestModel registerRequest)
    {
        UserRegisterResponseModel responseModel = await _authService.Register(registerRequest, new CancellationToken());
        return Ok(responseModel);
    }
}
