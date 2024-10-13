using Core.Model;

namespace Model.Entities;

public class Transfer : IEntity
{
    public Guid Id { get; set; }
    public Guid TransferTypeId { get; set; }
    public Guid SenderAccountId { get; set; }
    public Guid RecipientAccountId { get; set; }
    public Guid SenderUserId { get; set; }
    public Guid RecipientUserId { get; set; }
    public DateTime Date { get; set; } 
    public bool Status { get; set; }
    public string? RejectionDetailDescription { get; set; }
    public double Amount { get; set; }
    public string? Description { get; set; }

    public TransferType? TransferType { get; set; }
    public Account? SenderAccount { get; set; }
    public Account? ReceivingAccount { get; set; }
    public User? SenderUser { get; set; }
    public User? ReceivingUser { get; set; }
}
