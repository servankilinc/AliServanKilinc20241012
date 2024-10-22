using Core.DataAccess.DynamicQueries;
using Core.DataAccess.Pagination;
using Model.Dtos.User_;
using Model.Models.Account_;
using Model.Models.User_;

namespace Business.Abstract;

public interface IUserService
{
    Task<int> CountAsync(CancellationToken cancellationToken = default);
    Task<UserResponseDto> GetUserAsync(Guid userId, CancellationToken cancellationToken);
    Task<Paginate<UserResponseDto>> GetAllByPaginationAsync(UserListRequestModel userListRequestModel, CancellationToken cancellationToken);
    Task<List<UserResponseDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<UserAccountBasicModel> FindUserByAccountNoAsync(string accountNo, CancellationToken cancellationToken);
    Task UpdateUserAsync(UserUpdateDto userUpdateModel, CancellationToken cancellationToken);
}
