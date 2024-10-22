using Core.DataAccess.DynamicQueries;
using Core.DataAccess.Pagination;

namespace Model.Models.Account_;

public class AccountListRequestModel
{
    public PagingRequest? PagingRequest { get; set; }
    public Filter? Filter { get; set; }
    public Sort? Sort { get; set; }
}
