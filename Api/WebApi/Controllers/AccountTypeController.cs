using Business.Abstract;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos.AccountType_;

namespace WebApi.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class AccountTypeController : ControllerBase
{
    private readonly IAccountTypeService _accountTypeService;
    private readonly IValidator<AccountTypeCreateDto> _validatorAccountTypeCreate;
    private readonly IValidator<AccountTypeUpdateDto> _validatorAccountTypeUpdate;
    public AccountTypeController(IAccountTypeService accountTypeService, IValidator<AccountTypeCreateDto> validatorAccountTypeCreate, IValidator<AccountTypeUpdateDto> validatorAccountTypeUpdate)
    {
        _accountTypeService = accountTypeService;
        _validatorAccountTypeCreate = validatorAccountTypeCreate;
        _validatorAccountTypeUpdate = validatorAccountTypeUpdate;
    }

    [HttpGet("GetAccountTypeList")]
    public async Task<IActionResult> GetAccountTypeList()
    {
        List<AccountTypeResponseDto> responseModel = await _accountTypeService.GetAccountTypeListAsync(new CancellationToken());
        return Ok(responseModel);
    }

    [HttpPost("CreateAccountType")]
    public async Task<IActionResult> CreateAccountType(AccountTypeCreateDto accountTypeCreate)
    {
        _validatorAccountTypeCreate.Validate(accountTypeCreate);
        AccountTypeResponseDto responseModel = await _accountTypeService.CreateAccountTypeAsync(accountTypeCreate, new CancellationToken());
        return Ok(responseModel);
    }

    [HttpPut("UpdateAccountType")]
    public async Task<IActionResult> UpdateAccountType(AccountTypeUpdateDto accountTypeUpdate)
    {
        _validatorAccountTypeUpdate.Validate(accountTypeUpdate);
        AccountTypeResponseDto responseModel = await _accountTypeService.UpdateAccountTypeAsync(accountTypeUpdate, new CancellationToken());
        return Ok(responseModel);
    }
}
