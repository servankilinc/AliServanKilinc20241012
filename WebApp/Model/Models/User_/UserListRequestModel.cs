using Core.DataAccess.DynamicQueries;
using Core.DataAccess.Pagination;

namespace Model.Models.User_;

public class UserListRequestModel
{
    public PagingRequest? PagingRequest { get; set; }
    public Filter? Filter { get; set; }
    public Sort? Sort { get; set; }
}
