using Business.Abstract;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos.User_;
using Model.Models.Account_;
using Model.Models.User_;

namespace WebApi.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IValidator<UserUpdateDto> _validatorUserUpdate;
    public UserController(IUserService userService, IValidator<UserUpdateDto> validatorUserUpdate)
    {
        _userService = userService;
        _validatorUserUpdate = validatorUserUpdate;
    }


    [HttpGet("GetUserCount")]
    public async Task<IActionResult> GetUserCount()
    {
        var result = await _userService.CountAsync(new CancellationToken());
        return Ok(result);
    }


    [HttpGet("GetUser")]
    public async Task<IActionResult> GetUser(Guid userId)
    {
        UserResponseDto responseModel = await _userService.GetUserAsync(userId, new CancellationToken());
        return Ok(responseModel);
    }

    [HttpPost("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var responseModel = await _userService.GetAllAsync(new CancellationToken());
        return Ok(responseModel);
    }

    [HttpGet("GetAllByPagination")]
    public async Task<IActionResult> GetAllUserByPagination([FromBody] UserListRequestModel requestModel)
    {
        var result = await _userService.GetAllByPaginationAsync(requestModel, new CancellationToken());
        return Ok(result);
    }

    [HttpGet("FindUserByAccountNo")]
    public async Task<IActionResult> FindUserByAccountNo(string accountNo)
    {
        if (accountNo == null) throw new ArgumentNullException(nameof(accountNo));
        UserAccountBasicModel responseModel = await _userService.FindUserByAccountNoAsync(accountNo, new CancellationToken());
        return Ok(responseModel);
    }

    [HttpPut("UpdateUser")]
    public async Task<IActionResult> UpdateUser([FromBody] UserUpdateDto userUpdateRequest)
    {
        _validatorUserUpdate.ValidateAndThrow(userUpdateRequest);
        await _userService.UpdateUserAsync(userUpdateRequest, new CancellationToken());
        return Ok();
    }
}
