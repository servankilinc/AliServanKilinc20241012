using Core.Model;

namespace Model.Dtos.TransferType_;

public class TransferTypeUpdateDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}