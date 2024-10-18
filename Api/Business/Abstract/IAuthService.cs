using Model.Models.Auth;

namespace Business.Abstract;

public interface IAuthService
{
    Task<UserLoginResponseModel> Login(UserLoginRequestModel loginRequestModel, CancellationToken cancellationToken = default);
    Task<UserRegisterResponseModel> Register(UserRegisterRequestModel registerRequestModel, CancellationToken cancellationToken = default);
}
