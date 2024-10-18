using FluentValidation;

namespace Model.Models.Auth;

public class UserRegisterRequestModel
{
    public string TCKNO { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public class UserRegisterRequestModelValidator : AbstractValidator<UserRegisterRequestModel>
{
    public UserRegisterRequestModelValidator()
    {
        RuleFor(b => b.TCKNO)
            .NotEmpty().WithMessage("T.C. Kimlik Numarası boş olamaz.")
            .Length(11).WithMessage("T.C. Kimlik Numarası 11 haneli olmalıdır.")
            .Must(BeAllDigits).WithMessage("T.C. Kimlik Numarası yalnızca sayılardan oluşmalıdır.");
        RuleFor(b => b.FirstName).MinimumLength(2).NotEmpty();
        RuleFor(b => b.LastName).MinimumLength(2).NotEmpty();
        RuleFor(b => b.Email).EmailAddress().NotEmpty().EmailAddress();
        RuleFor(b => b.Password).MinimumLength(6).NotEmpty();
    }
    private bool BeAllDigits(string tckn)
    {
        return tckn.All(char.IsDigit);
    }
}