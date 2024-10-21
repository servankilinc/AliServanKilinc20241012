using Core.DataAccess.Pagination;
using Model.Dtos.Account_;
using Model.Models.Account_;

namespace Business.Abstract;

public interface IAccountService
{
    Task<AccountResponseDto> GetAccountAsync(Guid accountId, CancellationToken cancellationToken = default);
    Task<List<AccountResponseDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Paginate<AccountResponseDto>> GetAllByPaginationAsync(AccountListRequestModel requestModel, CancellationToken cancellationToken = default);
    Task<List<AccountResponseDto>> GetUserAccountsAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<List<AccountByLastTransfersModel>> GetUserAccountsWithLastTransfersAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<AccountResponseDto> CreateAccountAsync(AccountCreateDto accountCreateDto, CancellationToken cancellationToken = default);
    Task VerifyAccountAsync(Guid accountId, CancellationToken cancellationToken = default);
    Task RemoveAccountVerificationAsync(Guid accountId, CancellationToken cancellationToken = default);
}
