using Core.Model;
using FluentValidation;

namespace Model.Dtos.User_;

public class UserUpdateDto : IDto
{
    public Guid? Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
}


public class UserUpdateDtoValidator : AbstractValidator<UserUpdateDto>
{
    public UserUpdateDtoValidator()
    {
        RuleFor(b => b.Id).NotNull().NotEmpty();
        RuleFor(b => b.FirstName).NotNull().NotEmpty();
        RuleFor(b => b.LastName).NotNull().NotEmpty();
        RuleFor(b => b.Email).NotNull().NotEmpty().EmailAddress();
    }
}