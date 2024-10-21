using Core.DataAccess.DynamicQueries;
using Core.DataAccess.Pagination;

namespace Model.Models.Transfer_;

public class TransferListRequestModel
{
    public PagingRequest? PagingRequest { get; set; }
    public Filter? Filter { get; set; }
    public Sort? Sort { get; set; }
}