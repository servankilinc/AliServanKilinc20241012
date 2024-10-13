using Model.Dtos.User_;
using Model.Models.Account_;

namespace Business.Abstract;

public interface IUserService
{
    Task<UserResponseDto> GetUserAsync(Guid userId, CancellationToken cancellationToken);
    Task<UserAccountBasicModel> FindUserByAccountNoAsync(string accountNo, CancellationToken cancellationToken);
    Task UpdateUserAsync(UserUpdateDto userUpdateModel, CancellationToken cancellationToken);
}
