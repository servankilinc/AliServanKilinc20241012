using Core.Model;

namespace Model.Dtos.User_;

public class UserResponseDto: IDto
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public DateTime RegistrationDate { get; set; }
}
