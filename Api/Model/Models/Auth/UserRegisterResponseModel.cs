using Core.Auth;
using Model.Dtos.User_;

namespace Model.Models.Auth;

public class UserRegisterResponseModel
{
    public AccessTokenModel Access { get; set; } = null!;
    public UserResponseDto User { get; set; } = null!;
}
