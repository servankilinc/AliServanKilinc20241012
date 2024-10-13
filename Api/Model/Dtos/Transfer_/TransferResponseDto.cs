using Core.Model;
using Model.Entities;

namespace Model.Dtos.Transfer_;

public class TransferResponseDto: IDto
{
    public Guid Id { get; set; }
    public Guid TransferTypeId { get; set; }
    public Guid SenderAccountNo { get; set; }
    public Guid RecipientAccountNo { get; set; }
    public DateTime Date { get; set; }
    public bool Status { get; set; }
    public string? RejectionDetailDescription { get; set; }
    public double Amount { get; set; }
    public string? Description { get; set; }

    public TransferType? TransferType { get; set; }
}
