using Model.Dtos.Account_;
using Model.Dtos.TransferType_;
using Model.Dtos.User_;

namespace Model.Models.Transfer_;

public class TransferDetailModel
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

    public TransferTypeResponseDto? TransferType { get; set; }
    public AccountResponseDto? SenderAccount { get; set; }
    public AccountResponseDto? ReceivingAccount { get; set; }
    public UserResponseDto? SenderUser { get; set; }
    public UserResponseDto? ReceivingUser { get; set; }
}