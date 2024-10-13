using Core.Model;
namespace Model.Dtos.AccountType_;

public class AccountTypeCreateDto : IDto
{
    public string Name { get; set; } = null!;
}
