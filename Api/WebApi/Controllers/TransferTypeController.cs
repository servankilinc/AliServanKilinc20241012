using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos.TransferType_;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransferTypeController : ControllerBase
{

    private readonly ITransferTypeService _transferTypeService;
    public TransferTypeController(ITransferTypeService transferTypeService) => _transferTypeService = transferTypeService;


    [HttpGet("GetTransferTypeList")]
    public async Task<IActionResult> GetTransferTypeList()
    {
        List<TransferTypeResponseDto> responseModel = await _transferTypeService.GetTransferTypeListAsync(new CancellationToken());
        return Ok(responseModel);
    }

    [HttpPost("CreateTransferType")]
    public async Task<IActionResult> CreateTransferType(TransferTypeCreateDto transferTypeCreate)
    {
        TransferTypeResponseDto responseModel = await _transferTypeService.CreateTransferTypeAsync(transferTypeCreate, new CancellationToken());
        return Ok(responseModel);
    }

    [HttpPut("UpdateTransferType")]
    public async Task<IActionResult> UpdateTransferType(TransferTypeUpdateDto transferTypeUpdate)
    {
        TransferTypeResponseDto responseModel = await _transferTypeService.UpdateTransferTypeAsync(transferTypeUpdate, new CancellationToken());
        return Ok(responseModel);
    }
}
