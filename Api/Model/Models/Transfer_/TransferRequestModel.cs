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
