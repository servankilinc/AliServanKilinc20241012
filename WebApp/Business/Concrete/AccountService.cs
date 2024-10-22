using AutoMapper;
using Business.Abstract;
using Core.DataAccess.Pagination;
using Core.Exceptions;
using DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;
using Model.Dtos.Account_;
using Model.Dtos.AccountType_;
using Model.Dtos.User_;
using Model.Entities;
using Model.Models.Account_;

namespace Business.Concrete;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;
    public AccountService(IAccountRepository accountRepository, IMapper mapper)
    {
        _accountRepository = accountRepository;
        _mapper = mapper;
    }


    public async Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        var result = await _accountRepository.CountAsync(cancellationToken: cancellationToken);
        return result;
    }


    public async Task<AccountResponseDto> GetAccountAsync(Guid accountId, CancellationToken cancellationToken = default)
    {
        Account account = await _accountRepository.GetAsync(filter: a => a.Id == accountId, cancellationToken: cancellationToken);
        return _mapper.Map<AccountResponseDto>(account);
    }
    

    public async Task<List<AccountResponseDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var list = await _accountRepository.GetAllAsync(cancellationToken: cancellationToken);
        return list.Select(_mapper.Map<AccountResponseDto>).ToList();
    }


    public async Task<Paginate<AccountWithUserResponseDto>> GetAllByPaginationAsync(AccountListRequestModel requestModel, CancellationToken cancellationToken = default)
    {
        Paginate<Account> paginated = await _accountRepository.GetPaginatedListAsync(
            include: a => a.Include(i => i.AccountType!).Include(i => i.User!),
            filter: requestModel.Filter ?? default,
            sort: requestModel.Sort ?? default,
            page: requestModel.PagingRequest!.Page,
            pageSize: requestModel.PagingRequest.PageSize
        );

        return new Paginate<AccountWithUserResponseDto>
        {
            Data = paginated.Data.Select(a => new AccountWithUserResponseDto()
            {
                AccountNo = a.AccountNo,
                AccountType = _mapper.Map<AccountTypeResponseDto>(a.AccountType),
                User = _mapper.Map<UserResponseDto>(a.User),
                UserId = a.UserId,
                AccountTypeId = a.AccountTypeId,
                Balance = a.Balance,
                CreatedDate = a.CreatedDate,
                Id = a.Id,
                IsVerified = a.IsVerified,
                VerificationDate = a.VerificationDate
            }).ToList(),
            DataCount = paginated.DataCount,
            Page = paginated.Page,
            PageCount = paginated.PageCount,
            PageSize = paginated.PageSize
        };
    }


    public async Task<List<AccountResponseDto>> GetUserAccountsAsync(Guid userId, CancellationToken cancellationToken)
    {
        var list = await _accountRepository.GetAllAsync(where: f => f.UserId == userId, include: f => f.Include(a => a.AccountType!), cancellationToken: cancellationToken);


        return list.Select(a => new AccountResponseDto()
        {
            AccountNo = a.AccountNo,
            AccountType = new AccountTypeResponseDto() { Id = a.AccountTypeId, Name = a.AccountType!.Name },
            AccountTypeId = a.AccountTypeId,
            Balance = a.Balance,
            CreatedDate = a.CreatedDate,
            Id = a.Id,
            IsVerified = a.IsVerified,
            UserId = userId,
            VerificationDate = a.VerificationDate
        }).ToList();
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


    public async Task VerifyAccountAsync(Guid accountId, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetAsync(filter: a => a.Id == accountId, cancellationToken: cancellationToken);
        if (account == null) throw new BusinessException("Hesap Bulunamadı!");
        account.IsVerified = true;
        account.VerificationDate = DateTime.Now;

        await _accountRepository.UpdateAsync(account, cancellationToken);
    }


    public async Task RemoveAccountVerificationAsync(Guid accountId, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetAsync(filter: a => a.Id == accountId, cancellationToken: cancellationToken);
        if (account == null) throw new BusinessException("Hesap Bulunamadı!");
        account.IsVerified = false;

        await _accountRepository.UpdateAsync(account, cancellationToken);
    }
}
