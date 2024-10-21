using Core.DataAccess.Pagination;
using Model.Dtos.User_;

namespace WebApp.ViewModels;

public class VMAdminPortalUserList
{
    public Paginate<UserResponseDto> Users { get; set; } = null!;
    public UserListFilterModel FilterModel { get; set; } = null!;
}

public class UserListFilterModel
{
    public int PageIndex { get; set; }
}
