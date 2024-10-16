﻿using Core.DataAccess.Pagination;
using Model.Dtos.TransferType_;
using Model.Models.Account_;
using Model.Models.Transfer_;

namespace Business.Abstract;

public interface ITransferService
{
    Task<Paginate<TransferDetailModel>> GetAccountHistoryAsync(AccountHistoryRequestModel requestModel, CancellationToken cancellationToken);
    Task<List<TransferTypeResponseDto>> GetTransferTypeListAsync(CancellationToken cancellationToken);
    Task SendTransferRequestAsync(TransferRequestModel transferRequestModel, CancellationToken cancellationToken);
    Task RejectTransferAsync(TransferRejectRequestModel rejectModel, CancellationToken cancellationToken);
}
