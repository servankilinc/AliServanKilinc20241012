﻿using Business.Abstract;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos.Account_;
using Model.Models.Account_;

namespace WebApi.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly IValidator<AccountCreateDto> _validatorAccountCreate;
    public AccountController(IAccountService accountService, IValidator<AccountCreateDto> validatorAccountCreate)
    {
        _accountService = accountService;
        _validatorAccountCreate = validatorAccountCreate;
    }
    
    [HttpGet("GetUserCount")]
    public async Task<IActionResult> GetUserCount()
    {
        var result = await _accountService.CountAsync(new CancellationToken());
        return Ok(result);
    }

    [HttpGet("GetAccountInfo")]
    public async Task<IActionResult> GetAccount(Guid accountId)
    {
        var result = await _accountService.GetAccountAsync(accountId ,new CancellationToken());
        return Ok(result);
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll(Guid accountId)
    {
        var result = await _accountService.GetAllAsync(new CancellationToken());
        return Ok(result);
    }

    [HttpGet("GetAllByPagination")]
    public async Task<IActionResult> GetAllByPagination(AccountListRequestModel requestModel)
    {
        var result = await _accountService.GetAllByPaginationAsync(requestModel, new CancellationToken());
        return Ok(result);
    }

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
        _validatorAccountCreate.ValidateAndThrow(createRequest);
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
