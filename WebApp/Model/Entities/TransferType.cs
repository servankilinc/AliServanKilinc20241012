using Core.Model;

namespace Model.Entities;

public class TransferType : SoftDeletableEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;

    public ICollection<Transfer>? Transfers { get; set; }
}
