using Business.Abstract;
using Core.DataAccess.Pagination;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos.User_;
using Model.Models.Account_;
using Model.Models.User_;

namespace WebApi.Controllers;

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

    [HttpGet("GetUser")]
    public async Task<IActionResult> GetUser(Guid userId)
    {
        UserResponseDto responseModel = await _userService.GetUserAsync(userId, new CancellationToken());
        return Ok(responseModel);
    }

    [HttpPost("GetAll")]
    public async Task<IActionResult> GetAll([FromBody] UserListRequestModel requestModel)
    {
        Paginate<UserResponseDto> responseModel = await _userService.GetAllAsync(requestModel, new CancellationToken());
        return Ok(responseModel);
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
