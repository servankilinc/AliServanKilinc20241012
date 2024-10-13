namespace Model.Models.Account_;

public class UserAccountBasicModel
{
    public string? UserId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public Guid AccountId { get; set; }
    public string AccountNo { get; set; } = null!;
}
