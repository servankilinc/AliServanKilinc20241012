using AutoMapper;
using Business.Abstract;
using Core.Auth;
using Core.Exceptions;
using DataAccess.Abstract;
using Microsoft.AspNetCore.Identity;
using Model.Dtos.User_;
using Model.Entities;
using Model.Models.Auth;

namespace Business.Concrete;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    private readonly IMapper _mapper;

    public AuthService(ITokenService tokenService, IUserRepository userRepository, UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager, IMapper mapper)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _userManager = userManager;
        _roleManager = roleManager;
        _mapper = mapper;
    }

    public async Task<UserLoginResponseModel> Login(UserLoginRequestModel loginRequestModel, CancellationToken cancellationToken)
    {
        User? existUser = !string.IsNullOrEmpty(loginRequestModel.Email) ? 
            await _userRepository.GetAsync(filter: u => u.Email!.ToLower() == loginRequestModel.Email.ToLower(), cancellationToken: cancellationToken) :
            await _userRepository.GetAsync(filter: u => u.TCKNO == loginRequestModel.TCKNO, cancellationToken: cancellationToken);

        if (existUser == null) throw new BusinessException($"Kullanıcı bulunamadı.");

        bool passwordStatus = await _userManager.CheckPasswordAsync(existUser, loginRequestModel.Password);
        if (!passwordStatus) throw new BusinessException("Girdiğiniz şifre doğru değil.");

        AccessTokenModel tokenModel = await _tokenService.CreateAccessTokenAsync(existUser);

        return new UserLoginResponseModel()
        {
            User = _mapper.Map<UserResponseDto>(existUser),
            Access = tokenModel
        };
    }

    public async Task<UserRegisterResponseModel> Register(UserRegisterRequestModel registerRequestModel, CancellationToken cancellationToken)
    {
        User userToRegister = _mapper.Map<User>(registerRequestModel);
        userToRegister.RegistrationDate = DateTime.Now;
        userToRegister.UserName = $"{registerRequestModel.Email}_{DateTime.UtcNow.ToString()}";

        var resultRegister = await _userManager.CreateAsync(userToRegister, registerRequestModel.Password);
        if (!resultRegister.Succeeded) throw new Exception(string.Join(separator: $" ", value: resultRegister.Errors.Select(e => e.Description).ToArray()));

        bool isRoleUserExist = await _roleManager.RoleExistsAsync("User");
        if (!isRoleUserExist) await _roleManager.CreateAsync(new IdentityRole<Guid>("User"));
        var resultRole = await _userManager.AddToRoleAsync(userToRegister, "User");

        User regisiretedUser = await _userRepository.GetAsync(filter: u => u.TCKNO == registerRequestModel.TCKNO, cancellationToken: cancellationToken);

        AccessTokenModel tokenModel = await _tokenService.CreateAccessTokenAsync(regisiretedUser);

        return new UserRegisterResponseModel()
        {
            User = _mapper.Map<UserResponseDto>(regisiretedUser),
            Access = tokenModel
        };
    }
}
