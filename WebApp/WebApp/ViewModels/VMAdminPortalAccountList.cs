using Core.DataAccess.Pagination;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Dtos.Account_;

namespace WebApp.ViewModels;

public class VMAdminPortalAccountList
{
    public Paginate<AccountWithUserResponseDto>? AccountList { get; set; }
    public List<SelectListItem>? UserList { get; set; }
    public List<SelectListItem>? AccountTypeList { get; set; }
    public AccountListFilterModel FilterModel { get; set; } = null!;
}

public class AccountListFilterModel
{
    public string? AccountNo { get; set; }
    public Guid UserId { get; set; }
    public Guid AccountTypeId { get; set; }
    public int PageIndex { get; set; }
}