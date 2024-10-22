using Core.DataAccess.Pagination;
using Model.Models.Account_;
using Model.Models.Transfer_;

namespace Business.Abstract;
public interface ITransferService
{
    Task<int> CountAsync(CancellationToken cancellationToken = default);
    Task<Paginate<TransferDetailModel>> GetAccountHistoryAsync(AccountHistoryRequestModel requestModel, CancellationToken cancellationToken);
    Task<Paginate<TransferDetailModel>> GetTransfers(TransferListRequestModel requestModel, CancellationToken cancellationToken);
    Task SendTransferRequestAsync(TransferRequestModel transferRequestModel, CancellationToken cancellationToken);
    Task RejectTransferAsync(TransferRejectRequestModel rejectModel, CancellationToken cancellationToken);
}
