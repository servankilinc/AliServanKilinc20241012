using Core.Model;

namespace Model.Entities;

public class AccountType : SoftDeletableEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;

    public ICollection<Account>? Accounts { get; set; }
}
