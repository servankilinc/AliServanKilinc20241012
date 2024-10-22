using Business.Abstract;
using Core.DataAccess.Pagination;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Models.Account_;
using Model.Models.Transfer_;

namespace WebApi.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class TransferController : ControllerBase
{
    private readonly ITransferService _transferService;
    private readonly IValidator<AccountHistoryRequestModel> _validatorAccountHistoryRequest;
    private readonly IValidator<TransferRequestModel> _validatorTransferRequest;
    private readonly IValidator<TransferRejectRequestModel> _validatorTransferRejectRequest;
    public TransferController(ITransferService transferService, IValidator<AccountHistoryRequestModel> validatorAccountHistoryRequest, IValidator<TransferRejectRequestModel> validatorTransferRejectRequest, IValidator<TransferRequestModel> validatorTransferRequest)
    {
        _transferService = transferService;
        _validatorAccountHistoryRequest = validatorAccountHistoryRequest;
        _validatorTransferRequest = validatorTransferRequest;
        _validatorTransferRejectRequest = validatorTransferRejectRequest;
    }

    [HttpPost("GetTransferCount")]
    public async Task<IActionResult> GetTransferCount()
    { 
        var result = await _transferService.CountAsync(new CancellationToken());
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("GetAllByPagination")]
    public async Task<IActionResult> GetAllByPagination([FromBody] TransferListRequestModel requestModel)
    {
        var result = await _transferService.GetTransfers(requestModel, new CancellationToken());
        return Ok(result);
    }

    [HttpPost("GetAccountHistory")]
    public async Task<IActionResult> GetAccountHistory([FromBody] AccountHistoryRequestModel accountHistoryRequest)
    {
        _validatorAccountHistoryRequest.ValidateAndThrow(accountHistoryRequest);
        Paginate<TransferDetailModel> responseModel = await _transferService.GetAccountHistoryAsync(accountHistoryRequest, new CancellationToken());
        return Ok(responseModel);
    }

    [HttpPost("SendTransferRequest")]
    public async Task<IActionResult> SendTransferRequest([FromBody] TransferRequestModel transferRequest)
    {
        _validatorTransferRequest.ValidateAndThrow(transferRequest);
        await _transferService.SendTransferRequestAsync(transferRequest, new CancellationToken());
        return Ok();
    }

    [HttpPost("RejectTransfer")]
    public async Task<IActionResult> RejectTransfer([FromBody] TransferRejectRequestModel transferRejectRequest)
    {
        _validatorTransferRejectRequest.ValidateAndThrow(transferRejectRequest);
        await _transferService.RejectTransferAsync(transferRejectRequest, new CancellationToken());
        return Ok();
    }
}
