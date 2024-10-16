using Core.Model;
using Microsoft.AspNetCore.Identity;

namespace Model.Entities;

public class User : IdentityUser<Guid>, ISoftDeletableEntity
{
    public string? TCKNO { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime RegistrationDate { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedOnUtc { get; set; }

    public ICollection<Account>? Accounts { get; set; }
    public ICollection<Transfer>? ShippingProcesses { get; set; }
    public ICollection<Transfer>? PurchaseProcesses { get; set; }
}
