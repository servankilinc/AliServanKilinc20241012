using FluentValidation;

namespace Model.Models.Transfer_;

public class TransferRejectRequestModel
{
    public Guid TransferId { get; set; }
    public string? Description { get; set; }
}

public class TransferRejectRequestModelValidator : AbstractValidator<TransferRejectRequestModel>
{
    public TransferRejectRequestModelValidator()
    {
        RuleFor(b => b.TransferId).NotNull().NotEmpty();
    }
}