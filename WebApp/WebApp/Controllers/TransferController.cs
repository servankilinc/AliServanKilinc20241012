using Business.Abstract;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Dtos.Account_;
using Model.Models.Transfer_;
using System.Security.Claims;
using WebApp.Utils;
using WebApp.ViewModels;

namespace WebApp.Controllers;

public class TransferController : Controller
{
    private readonly IAccountService _accountService;
    private readonly ITransferTypeService _transferTypeService;
    private readonly ITransferService _transferService;
    private readonly IValidator<TransferRequestModel> _validatorTransferRequest;
    private readonly IValidator<TransferRejectRequestModel> _validatorTransferRejectRequest;

    public TransferController(IAccountService accountService, ITransferTypeService transferTypeService, IValidator<TransferRequestModel> validatorTransferRequest, ITransferService transferService, IValidator<TransferRejectRequestModel> validatorTransferRejectRequest)
    {
        _accountService = accountService;
        _transferTypeService = transferTypeService;
        _validatorTransferRequest = validatorTransferRequest;
        _transferService = transferService;
        _validatorTransferRejectRequest = validatorTransferRejectRequest;
    }


    public async Task<IActionResult> Demand(Guid accountId)
    {
        var requestModel =  new TransferRequestModel()
        {
            SenderAccountId = accountId,
            Amount = 0,
        };
        var transferTypeList = await _transferTypeService.GetTransferTypeListAsync(new CancellationToken());         
        ViewBag.TransferTypeList = transferTypeList.Select(d => new SelectListItem(d.Name, d.Id.ToString())).ToList();

        return View(requestModel);
    }

    [HttpPost]
    public async Task<IActionResult> Demand(TransferRequestModel transferRequestModel)
    {
        Claim? nameId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        if (nameId == null) return RedirectToAction("Login", "Auth");
        
        transferRequestModel.SenderUserId = Guid.Parse(nameId.Value);

        var validation = _validatorTransferRequest.Validate(transferRequestModel);
        if (!validation.IsValid)
        {
            var transferTypeList = await _transferTypeService.GetTransferTypeListAsync(new CancellationToken());

            ViewBag.UserAccounts = await _accountService.GetUserAccountsAsync(Guid.Parse(nameId.Value), new CancellationToken());
            ViewBag.TransferTypeList = transferTypeList.Select(d => new SelectListItem(d.Name, d.Id.ToString())).ToList();

            validation.AddToModelState(ModelState);
          
            return View(transferRequestModel);
        }
    
        await _transferService.SendTransferRequestAsync(transferRequestModel, new CancellationToken());
        return RedirectToAction("Index", "Home");
    }

    [Authorize(Roles ="Admin")]
    [HttpPost]
    public async Task<IActionResult> Rejection(TransferRejectRequestModel transferRejectRequestModel)
    {
        var validation = _validatorTransferRejectRequest.Validate(transferRejectRequestModel);
        if (!validation.IsValid)
        {
            validation.AddToModelState(ModelState);
            return View(transferRejectRequestModel);
        }
            
        await _transferService.RejectTransferAsync(transferRejectRequestModel, new CancellationToken());

        return RedirectToAction("Index", "AdminPortal");
    }
}
