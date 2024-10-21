using AutoMapper;
using Business.Abstract;
using Core.DataAccess.Pagination;
using Core.Exceptions;
using DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;
using Model.Dtos.User_;
using Model.Entities;
using Model.Models.Account_;
using Model.Models.User_;
using System.Collections.Generic;

namespace Business.Concrete;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IAccountRepository accountRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _accountRepository = accountRepository;
        _mapper = mapper;
    }
     
    public async Task<UserAccountBasicModel> FindUserByAccountNoAsync(string accountNo, CancellationToken cancellationToken)
    {
        Account account = await _accountRepository.GetAsync(
            filter: a => a.AccountNo == accountNo,
            include: a => a.Include(a => a.User!),
            cancellationToken: cancellationToken
        );
        if (account == null) throw new BusinessException("Bu hesap numarası için bir kayıt bulunamadı");
        if (account.User!.IsDeleted) throw new BusinessException("Bu hesap artık kullanıma açık değil.");

        return new UserAccountBasicModel()
        {
            AccountId = account.Id,
            AccountNo = account.AccountNo,
            FirstName = account.User!.FirstName,
            LastName = account.User.LastName,
            UserId = account.UserId,
        };
    }

    public async Task<Paginate<UserResponseDto>> GetAllByPaginationAsync(UserListRequestModel userListRequestModel, CancellationToken cancellationToken)
    {
        Paginate<User> paginated = await _userRepository.GetPaginatedListAsync(
            filter: userListRequestModel.Filter?? default,
            sort: userListRequestModel.Sort?? default,
            page: userListRequestModel.PagingRequest!.Page,
            pageSize: userListRequestModel.PagingRequest.PageSize
        );

        return new Paginate<UserResponseDto>
        {
            Data = paginated.Data.Select(_mapper.Map<UserResponseDto>).ToList(),
            DataCount = paginated.DataCount,
            Page = paginated.Page,
            PageCount = paginated.PageCount,
            PageSize = paginated.PageSize
        };
    }

    public async Task<List<UserResponseDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var list = await _userRepository.GetAllAsync();

        return list.Select(_mapper.Map<UserResponseDto>).ToList();
    }

    public async Task<UserResponseDto> GetUserAsync(Guid userId, CancellationToken cancellationToken)
    {
        User user = await _userRepository.GetAsync(filter: u => u.Id == userId, cancellationToken: cancellationToken);
        return _mapper.Map<UserResponseDto>(user);
    }

    public async Task UpdateUserAsync(UserUpdateDto userUpdateModel, CancellationToken cancellationToken)
    {
        User existData = await _userRepository.GetAsync(filter: u => u.Id == userUpdateModel.Id , cancellationToken: cancellationToken);
        if (existData == null) throw new BusinessException("Günceleme işlemi gerçekleştirilemedi.");
        await _userRepository.UpdateAsync(_mapper.Map(userUpdateModel, existData), cancellationToken: cancellationToken);
    }
}