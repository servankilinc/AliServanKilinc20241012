using Business.Abstract;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos.TransferType_;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransferTypeController : ControllerBase
{

    private readonly ITransferTypeService _transferTypeService;
    private readonly IValidator<TransferTypeCreateDto> _validatorTransferTypeCreate;
    private readonly IValidator<TransferTypeUpdateDto> _validatorTransferTypeUpdate;
    public TransferTypeController(ITransferTypeService transferTypeService, IValidator<TransferTypeCreateDto> validatorTransferTypeCreate, IValidator<TransferTypeUpdateDto> validatorTransferTypeUpdate)
    {
        _transferTypeService = transferTypeService;
        _validatorTransferTypeCreate = validatorTransferTypeCreate;
        _validatorTransferTypeUpdate = validatorTransferTypeUpdate;
    }

    [HttpGet("GetTransferTypeList")]
    public async Task<IActionResult> GetTransferTypeList()
    {
        List<TransferTypeResponseDto> responseModel = await _transferTypeService.GetTransferTypeListAsync(new CancellationToken());
        return Ok(responseModel);
    }

    [HttpPost("CreateTransferType")]
    public async Task<IActionResult> CreateTransferType(TransferTypeCreateDto transferTypeCreate)
    {
        _validatorTransferTypeCreate.Validate(transferTypeCreate);
        TransferTypeResponseDto responseModel = await _transferTypeService.CreateTransferTypeAsync(transferTypeCreate, new CancellationToken());
        return Ok(responseModel);
    }

    [HttpPut("UpdateTransferType")]
    public async Task<IActionResult> UpdateTransferType(TransferTypeUpdateDto transferTypeUpdate)
    {
        _validatorTransferTypeUpdate.ValidateAndThrow(transferTypeUpdate);
        TransferTypeResponseDto responseModel = await _transferTypeService.UpdateTransferTypeAsync(transferTypeUpdate, new CancellationToken());
        return Ok(responseModel);
    }
}
