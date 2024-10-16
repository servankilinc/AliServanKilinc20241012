using Core.Model;
using FluentValidation;

namespace Model.Dtos.Account_;

public class AccountCreateDto: IDto
{
    public Guid AccountTypeId { get; set; }
    public Guid UserId { get; set; }
}


public class AccountCreateDtoValidator : AbstractValidator<AccountCreateDto>
{
    public AccountCreateDtoValidator()
    {
        RuleFor(b => b.AccountTypeId).NotNull().NotEmpty();  
        RuleFor(b => b.UserId).NotNull().NotEmpty();  
    } 
}