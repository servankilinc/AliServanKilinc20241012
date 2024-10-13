using Model.Dtos.Account_;
using Model.Dtos.AccountType_;
using Model.Entities;
using Model.Models.Account_;

namespace Business.Abstract;

public interface IAccountService
{
    Task<List<AccountResponseDto>> GetUserAccountsAsync(Guid userId, CancellationToken cancellationToken);
    Task<List<AccountByLastTransfersModel>> GetUserAccountsWithLastTransfersAsync(Guid userId, CancellationToken cancellationToken);
    Task<List<AccountTypeResponseDto>> GetAccountTypeListAsync(CancellationToken cancellationToken);
    Task<AccountResponseDto> CreateAccountAsync(AccountCreateDto accountCreateDto, CancellationToken cancellationToken);
    Task VerifyAccountAsync(Guid accountId, CancellationToken cancellationToken);
    Task RemoveAccountVerificationAsync(Guid accountId, CancellationToken cancellationToken);
}
