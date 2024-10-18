using AutoMapper;
using Model.Dtos.Account_;
using Model.Dtos.AccountType_;
using Model.Dtos.TransferType_;
using Model.Dtos.User_;
using Model.Entities;
using Model.Models.Auth;

namespace Business.MappingProfiles_;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        // CreateMap<source, dest>


        CreateMap<Account, AccountResponseDto>().ReverseMap();
        CreateMap<AccountCreateDto, Account>().ReverseMap();

        CreateMap<AccountType, AccountTypeResponseDto>().ReverseMap();
        CreateMap<AccountTypeCreateDto, AccountType>().ReverseMap();
        CreateMap<AccountTypeUpdateDto, AccountType>().ReverseMap();

        CreateMap<TransferType, TransferTypeResponseDto>().ReverseMap();
        CreateMap<TransferTypeCreateDto, TransferType>().ReverseMap();
        CreateMap<TransferTypeUpdateDto, TransferType>().ReverseMap();

        CreateMap<User, UserResponseDto>().ReverseMap();
        CreateMap<UserUpdateDto, User>().ReverseMap();
        CreateMap<UserRegisterRequestModel, User>().ReverseMap();
    }
}