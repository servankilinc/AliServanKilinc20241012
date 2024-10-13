using Core.Model;

namespace Model.Dtos.Account_;

public class AccountCreateDto: IDto
{
    public Guid AccountTypeId { get; set; }
    public Guid UserId { get; set; }
}
