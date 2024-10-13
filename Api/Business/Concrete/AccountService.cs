using AutoMapper;
using Business.Abstract;
using DataAccess.Abstract;
using Model.Dtos.Account_;
using Model.Dtos.AccountType_;
using Model.Entities;
using Model.Models.Account_;

namespace Business.Concrete;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IAccountTypeRepository _accountTypeRepository;
    private readonly IMapper _mapper;
    public AccountService(IAccountRepository accountRepository, IAccountTypeRepository accountTypeRepository, IMapper mapper)
    {
        _accountRepository = accountRepository;
        _accountTypeRepository = accountTypeRepository;
        _mapper = mapper;
    }

    public async Task<List<AccountResponseDto>> GetUserAccountsAsync(Guid userId, CancellationToken cancellationToken)
    {
        var list = await _accountRepository.GetAllAsync(filter: f => f.UserId == userId, cancellationToken: cancellationToken);

        return list.Select(_mapper.Map<AccountResponseDto>).ToList();
    }

    public async Task<List<AccountByLastTransfersModel>> GetUserAccountsWithLastTransfersAsync(Guid userId, CancellationToken cancellationToken)
    {
        var list = await _accountRepository.GetUserAccountsWithLastTransfersAsync(userId, cancellationToken);// ShippingProcesses include last 5 transfers
        var responseModel = list.Select(a => new AccountByLastTransfersModel
        {
            Account = _mapper.Map<AccountResponseDto>(a),
            Transfers = a.ShippingProcesses != null ? a.ShippingProcesses.Select(t => new TransferBasicModel
            {
                Id = t.Id,
                Amount = t.Amount,
                Date = t.Date,
                IsUserSender = t.SenderUserId == userId,

            }).ToList() : default(List<TransferBasicModel>),
        }).ToList();

        return responseModel;
    }

    public async Task<AccountResponseDto> CreateAccountAsync(AccountCreateDto accountCreateDto, CancellationToken cancellationToken)
    {
        Account accountToInsert = _mapper.Map<Account>(accountCreateDto);
        string _accountNo;
        do
        {
            _accountNo = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 12);
        } while (await _accountRepository.IsExistAsync(filter: a => a.AccountNo == _accountNo));

        accountToInsert.AccountNo = _accountNo;
        accountToInsert.CreatedDate = DateTime.Now;
        Account createdAccount = await _accountRepository.AddAsync(accountToInsert, cancellationToken);

        await VerifyAccountAsync(createdAccount.Id, cancellationToken); // Verify account default

        return _mapper.Map<AccountResponseDto>(createdAccount);
    }

    public async Task<List<AccountTypeResponseDto>> GetAccountTypeListAsync(CancellationToken cancellationToken)
    {
        var list = await _accountTypeRepository.GetAllAsync(cancellationToken: cancellationToken);
        return list.Select(_mapper.Map<AccountTypeResponseDto>).ToList();
    }

    public async Task VerifyAccountAsync(Guid accountId, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetAsync(filter: a => a.Id == accountId, cancellationToken: cancellationToken);
        if (account == null) throw new Exception("Hesap Bulunamadı!");
        account.IsVerified = true;
        account.VerificationDate = DateTime.Now;

        await _accountRepository.UpdateAsync(account, cancellationToken);
    }

    public async Task RemoveAccountVerificationAsync(Guid accountId, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetAsync(filter: a => a.Id == accountId, cancellationToken: cancellationToken);
        if (account == null) throw new Exception("Hesap Bulunamadı!");
        account.IsVerified = false;

        await _accountRepository.UpdateAsync(account, cancellationToken);
    }
}
