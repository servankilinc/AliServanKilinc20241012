using Model.Dtos.AccountType_;

namespace Business.Abstract;

public interface IAccountTypeService
{
    Task<List<AccountTypeResponseDto>> GetAccountTypeListAsync(CancellationToken cancellationToken = default);
    Task<AccountTypeResponseDto> CreateAccountTypeAsync(AccountTypeCreateDto accountTypeCreateDto, CancellationToken cancellationToken = default);
    Task<AccountTypeResponseDto> UpdateAccountTypeAsync(AccountTypeUpdateDto accountTypeUpdateDto, CancellationToken cancellationToken = default);
}