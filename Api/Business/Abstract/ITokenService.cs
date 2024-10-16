using Core.Auth;
using Model.Entities;

namespace Business.Abstract;

public interface ITokenService
{
    Task<AccessTokenModel> CreateAccessTokenAsync(User user);
}