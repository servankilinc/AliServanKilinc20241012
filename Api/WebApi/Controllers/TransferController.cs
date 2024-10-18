using Business.Abstract;
using Core.DataAccess.Pagination;
using Microsoft.AspNetCore.Mvc;
using Model.Models.Account_;
using Model.Models.Transfer_;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransferController : ControllerBase
{
    private readonly ITransferService _transferService;
    public TransferController(ITransferService transferService) => _transferService = transferService;


    [HttpGet("GetAccountHistory")]
    public async Task<IActionResult> GetAccountHistory([FromBody] AccountHistoryRequestModel accountHistoryRequest)
    {
        Paginate<TransferDetailModel> responseModel = await _transferService.GetAccountHistoryAsync(accountHistoryRequest, new CancellationToken());
        return Ok(responseModel);
    }

    [HttpPost("SendTransferRequest")]
    public async Task<IActionResult> SendTransferRequest([FromBody] TransferRequestModel transferRequest)
    {
        await _transferService.SendTransferRequestAsync(transferRequest, new CancellationToken());
        return Ok();
    }

    [HttpPost("RejectTransfer")]
    public async Task<IActionResult> RejectTransfer([FromBody] TransferRejectRequestModel transferRejectRequest)
    {
        await _transferService.RejectTransferAsync(transferRejectRequest, new CancellationToken());
        return Ok();
    }
}
