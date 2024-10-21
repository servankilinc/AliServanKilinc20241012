using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Dtos.Account_;
using Model.Dtos.TransferType_;
using Model.Models.Transfer_;

namespace WebApp.ViewModels;

public class VMTransferProcess
{
    public List<AccountResponseDto> UserAccounts { get; set; } = null!;
    public List<SelectListItem> TransferTypes { get; set; } = null!;
    public TransferRequestModel? TransferRequestModel { get; set; }
}
