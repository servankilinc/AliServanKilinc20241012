namespace Model.Models.Transfer_;

public class TransferRequestModel
{
    public Guid TransferTypeId { get; set; }
    public Guid SenderAccountId { get; set; }
    public Guid RecipientAccountId { get; set; }
    public Guid SenderUserId { get; set; }
    public Guid RecipientUserId { get; set; }
    public double Amount { get; set; }
    public string? Description { get; set; }
}
