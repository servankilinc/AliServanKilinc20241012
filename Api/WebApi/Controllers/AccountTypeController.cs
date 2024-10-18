using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos.AccountType_;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountTypeController : ControllerBase
{
    private readonly IAccountTypeService _accountTypeService;
    public AccountTypeController(IAccountTypeService accountTypeService) => _accountTypeService = accountTypeService;


    [HttpGet("GetAccountTypeList")]
    public async Task<IActionResult> GetAccountTypeList()
    {
        List<AccountTypeResponseDto> responseModel = await _accountTypeService.GetAccountTypeListAsync(new CancellationToken());
        return Ok(responseModel);
    }

    [HttpPost("CreateAccountType")]
    public async Task<IActionResult> CreateAccountType(AccountTypeCreateDto accountTypeCreate)
    {
        AccountTypeResponseDto responseModel = await _accountTypeService.CreateAccountTypeAsync(accountTypeCreate, new CancellationToken());
        return Ok(responseModel);
    }

    [HttpPut("UpdateAccountType")]
    public async Task<IActionResult> UpdateAccountType(AccountTypeUpdateDto accountTypeUpdate)
    {
        AccountTypeResponseDto responseModel = await _accountTypeService.UpdateAccountTypeAsync(accountTypeUpdate, new CancellationToken());
        return Ok(responseModel);
    }
}
