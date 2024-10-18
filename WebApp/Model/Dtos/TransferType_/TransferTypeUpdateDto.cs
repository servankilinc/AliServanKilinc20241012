using Core.Model;
using FluentValidation;

namespace Model.Dtos.TransferType_;

public class TransferTypeUpdateDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}

public class TransferTypeUpdateDtoValidator : AbstractValidator<TransferTypeUpdateDto>
{
    public TransferTypeUpdateDtoValidator()
    {
        RuleFor(b => b.Id).NotNull().NotEmpty();
        RuleFor(b => b.Name).NotNull().NotEmpty();
    }
}