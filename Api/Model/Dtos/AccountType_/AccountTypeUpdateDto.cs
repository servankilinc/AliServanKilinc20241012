using Core.Model;
using FluentValidation;
namespace Model.Dtos.AccountType_;

public class AccountTypeUpdateDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}

public class AccountTypeUpdateDtoValidator : AbstractValidator<AccountTypeUpdateDto>
{
    public AccountTypeUpdateDtoValidator()
    {
        RuleFor(b => b.Id).NotNull().NotEmpty();
        RuleFor(b => b.Name).NotNull().NotEmpty();
    }
}