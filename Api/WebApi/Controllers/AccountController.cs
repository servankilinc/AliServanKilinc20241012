using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos.Account_;
using Model.Models.Account_;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    public AccountController(IAccountService accountService) => _accountService = accountService;


    [HttpGet("GetUserAccounts")]
    public async Task<IActionResult> GetUserAccounts(Guid userId)
    {
        List<AccountResponseDto> responseModel = await _accountService.GetUserAccountsAsync(userId, new CancellationToken());
        return Ok(responseModel);
    }

    [HttpGet("GetUserAccountsWithLastTransfers")]
    public async Task<IActionResult> GetUserAccountsWithLastTransfers(Guid userId)
    {
        List<AccountByLastTransfersModel> responseModel = await _accountService.GetUserAccountsWithLastTransfersAsync(userId, new CancellationToken());
        return Ok(responseModel);
    }

    [HttpPost("CreateAccount")]
    public async Task<IActionResult> CreateAccount(AccountCreateDto createRequest)
    {
        AccountResponseDto responseModel = await _accountService.CreateAccountAsync(createRequest, new CancellationToken());
        return Ok(responseModel);
    }

    [HttpPost("VerifyAccount")]
    public async Task<IActionResult> VerifyAccount([FromBody] Guid accountId)
    {
        await _accountService.VerifyAccountAsync(accountId, new CancellationToken());
        return Ok();
    }

    [HttpPost("RemoveAccountVerification")]
    public async Task<IActionResult> RemoveAccountVerification([FromBody] Guid accountId)
    {
        await _accountService.RemoveAccountVerificationAsync(accountId, new CancellationToken());
        return Ok();
    }
}
