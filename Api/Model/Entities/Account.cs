using Core.Model;

namespace Model.Entities;

public class Account : SoftDeletableEntity
{
    public Guid Id { get; set; }
    public string AccountNo { get; set; } = null!;
    public Guid AccountTypeId { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime VerificationDate { get; set; }
    public bool IsVerified { get; set; }
    public double Balance { get; set; }

    public AccountType? AccountType { get; set; }
    public User? User { get; set; }
    public ICollection<Transfer>? ShippingProcesses { get; set; }
    public ICollection<Transfer>? PurchaseProcesses { get; set; }
}
