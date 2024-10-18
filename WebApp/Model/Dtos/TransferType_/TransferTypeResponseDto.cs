using Core.Model;

namespace Model.Dtos.TransferType_;

public class TransferTypeResponseDto: IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}
