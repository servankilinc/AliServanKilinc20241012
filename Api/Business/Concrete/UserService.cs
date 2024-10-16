using AutoMapper;
using Business.Abstract;
using DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;
using Model.Dtos.User_;
using Model.Entities;
using Model.Models.Account_;

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
        if (account == null) throw new Exception("Bu hesap numarası için bir kayıt bulunamadı");
        if (account.User!.IsDeleted) throw new Exception("Bu hesap artık kullanıma açık değil.");

        return new UserAccountBasicModel()
        {
            AccountId = account.Id,
            AccountNo = account.AccountNo,
            FirstName = account.User!.FirstName,
            LastName = account.User.LastName,
            UserId = account.UserId,
        };
    }

    public async Task<UserResponseDto> GetUserAsync(Guid userId, CancellationToken cancellationToken)
    {
        User user = await _userRepository.GetAsync(filter: u => u.Id == userId, cancellationToken: cancellationToken);
        return _mapper.Map<UserResponseDto>(user);
    }

    public async Task UpdateUserAsync(UserUpdateDto userUpdateModel, CancellationToken cancellationToken)
    {
        User existData = await _userRepository.GetAsync(filter: u => u.Id == userUpdateModel.Id , cancellationToken: cancellationToken);
        if (existData == null) throw new Exception("Günceleme işlemi gerçekleştirilemedi.");
        await _userRepository.UpdateAsync(_mapper.Map(userUpdateModel, existData), cancellationToken: cancellationToken);
    }
}