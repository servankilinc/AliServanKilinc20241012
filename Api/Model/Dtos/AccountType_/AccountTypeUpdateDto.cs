using Core.Model;
namespace Model.Dtos.AccountType_;

public class AccountTypeUpdateDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}