using Core.Model;
using FluentValidation;

namespace Model.Dtos.TransferType_;

public class TransferTypeCreateDto : IDto
{
    public string Name { get; set; } = null!;
}


public class TransferTypeCreateDtoValidator : AbstractValidator<TransferTypeCreateDto>
{
    public TransferTypeCreateDtoValidator()
    { 
        RuleFor(b => b.Name).NotNull().NotEmpty();
    }
}