using Core.DataAccess.Pagination;

namespace Model.Models.Account_;

public class AccountHistoryRequestModel
{
    public Guid AccountId { get; set; }
    public PagingRequest? PagingRequest { get; set; }
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public bool ProcessStatus { get; set; }
    public int SortBy { get; set; } = 0; 
}