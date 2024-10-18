using AutoMapper;
using Business.Abstract;
using Core.Exceptions;
using DataAccess.Abstract;
using Model.Dtos.AccountType_;
using Model.Entities;

namespace Business.Concrete;

public class AccountTypeService : IAccountTypeService
{
    private readonly IAccountTypeRepository _accountTypeRepository;
    private readonly IMapper _mapper;
    public AccountTypeService(IAccountTypeRepository accountTypeRepository, IMapper mapper)
    {
        _accountTypeRepository = accountTypeRepository;
        _mapper = mapper;
    }

    public async Task<List<AccountTypeResponseDto>> GetAccountTypeListAsync(CancellationToken cancellationToken = default)
    {
        var list = await _accountTypeRepository.GetAllAsync(cancellationToken: cancellationToken);
        return list.Select(_mapper.Map<AccountTypeResponseDto>).ToList();
    }

    public async Task<AccountTypeResponseDto> CreateAccountTypeAsync(AccountTypeCreateDto accountTypeCreateDto, CancellationToken cancellationToken = default)
    {
        AccountType dataToInsert = _mapper.Map<AccountType>(accountTypeCreateDto);
        AccountType insertedData = await _accountTypeRepository.AddAsync(dataToInsert, cancellationToken: cancellationToken);
        return _mapper.Map<AccountTypeResponseDto>(insertedData);
    }

    public async Task<AccountTypeResponseDto> UpdateAccountTypeAsync(AccountTypeUpdateDto accountTypeUpdateDto, CancellationToken cancellationToken = default)
    {
        AccountType existData = await _accountTypeRepository.GetAsync(filter: f => f.Id == accountTypeUpdateDto.Id, cancellationToken: cancellationToken);
        if (existData == null) throw new BusinessException("Günceleme işlemi gerçekleştirilemedi.");
        AccountType dataToUpdate = _mapper.Map(accountTypeUpdateDto, existData);
        AccountType updatedData = await _accountTypeRepository.UpdateAsync(dataToUpdate, cancellationToken: cancellationToken);
        return _mapper.Map<AccountTypeResponseDto>(updatedData);
    }
}
