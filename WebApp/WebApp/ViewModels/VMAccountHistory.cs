using Core.DataAccess.Pagination;
using Model.Dtos.Account_;
using Model.Models.Transfer_;

namespace WebApp.ViewModels;

public class VMAccountHistory
{
    public AccountResponseDto Account { get; set; } = null!;
    public Paginate<TransferDetailModel> Transfers { get; set; } = null!;
    public AccountHistoryFilterModel FilterModel { get; set; } = null!;
}

public class AccountHistoryFilterModel
{
    public Guid AccountId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool Status { get; set; }
    public int PageIndex { get; set; } 
}
