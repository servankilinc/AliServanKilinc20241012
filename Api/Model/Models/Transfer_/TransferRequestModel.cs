using FluentValidation;

namespace Model.Models.Transfer_;

public class TransferRequestModel
{
    public Guid TransferTypeId { get; set; }
    public string RecipientUserFullName { get; set; } = null!;
    public Guid SenderAccountId { get; set; }
    public string RecipientAccountNo { get; set; } = null!;
    public Guid SenderUserId { get; set; }
    public double Amount { get; set; }
    public string? Description { get; set; }
}

public class TransferRequestModelValidator : AbstractValidator<TransferRequestModel>
{
    public TransferRequestModelValidator()
    {
        RuleFor(b => b.TransferTypeId).NotEqual(Guid.Empty).NotNull().NotEmpty();
        RuleFor(b => b.RecipientUserFullName).NotNull().NotEmpty();
        RuleFor(b => b.SenderAccountId).NotEqual(Guid.Empty).NotNull().NotEmpty();
        RuleFor(b => b.RecipientAccountNo).NotNull().NotEmpty().Length(12);
        RuleFor(b => b.SenderUserId).NotEqual(Guid.Empty).NotNull().NotEmpty();
        RuleFor(b => b.Amount).NotNull().NotEmpty();
    }
}