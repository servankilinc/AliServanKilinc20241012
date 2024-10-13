using Core.Model;

namespace Model.Dtos.TransferType_;

public class TransferTypeCreateDto : IDto
{
    public string Name { get; set; } = null!;
}
