using Core.Model;
using Model.Dtos.AccountType_;
using Model.Dtos.User_;

namespace Model.Dtos.Account_;

public class AccountWithUserResponseDto : IDto
{
    public Guid Id { get; set; }
    public string AccountNo { get; set; } = null!;
    public Guid AccountTypeId { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime VerificationDate { get; set; }
    public bool IsVerified { get; set; }
    public double Balance { get; set; }

    public AccountTypeResponseDto? AccountType { get; set; }
    public UserResponseDto? User { get; set; }
}
