using Core.DataAccess.Pagination;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Models.Transfer_;

namespace WebApp.ViewModels;

public class VMAdminPortalTransferList
{
    public List<SelectListItem>? TransferTypeList { get; set; }
    public List<SelectListItem>? UserList { get; set; }
    public List<SelectListItem>? AccountList { get; set; }
    public Paginate<TransferDetailModel> Transfers { get; set; } = null!;
    public TransferListFilterModel FilterModel { get; set; } = null!;
}
public class TransferListFilterModel
{
    public Guid UserId { get; set; }
    public Guid AccountId { get; set; }
    public bool Status { get; set; }
    public Guid TransferTypeId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int PageIndex { get; set; }
}
