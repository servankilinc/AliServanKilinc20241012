using Core.Model;
using FluentValidation;

namespace Model.Dtos.AccountType_;

public class AccountTypeCreateDto : IDto
{
    public string Name { get; set; } = null!;
}


public class AccountTypeCreateDtoValidator : AbstractValidator<AccountTypeCreateDto>
{
    public AccountTypeCreateDtoValidator()
    {
        RuleFor(b => b.Name).NotNull().NotEmpty(); 
    }
}