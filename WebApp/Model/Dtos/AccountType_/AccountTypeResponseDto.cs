using Core.Model;
namespace Model.Dtos.AccountType_;

public class AccountTypeResponseDto: IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}
