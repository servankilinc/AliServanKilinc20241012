using FluentValidation;

namespace Model.Models.Auth;

public class UserLoginRequestModel
{
    public string? TCKNO { get; set; }
    public string? Email { get; set; }
    public string Password { get; set; } = null!;
}


public class UserLoginRequestModelValidator : AbstractValidator<UserLoginRequestModel>
{
    public UserLoginRequestModelValidator()
    {
        RuleFor(b => b.Password).MinimumLength(6).NotEmpty();

        When(f => !string.IsNullOrEmpty(f.Email), () => {
            RuleFor(b => b.Email).EmailAddress().NotEmpty().EmailAddress();
        });
        When(f => !string.IsNullOrEmpty(f.TCKNO), () => {
            RuleFor(b => b.TCKNO)
                .NotEmpty().WithMessage("T.C. Kimlik Numarası boş olamaz.")
                .Length(11).WithMessage("T.C. Kimlik Numarası 11 haneli olmalıdır.")
                .Must(BeAllDigits).WithMessage("T.C. Kimlik Numarası yalnızca sayılardan oluşmalıdır.");
        });

        RuleFor(b => new { b.TCKNO, b.Email })
              .Must(fields => !string.IsNullOrEmpty(fields.TCKNO) || !string.IsNullOrEmpty(fields.Email))
              .WithMessage("T.C. Kimlik Numarası veya Email alanlarından en az biri dolu olmalıdır.");
    }


    private bool BeAllDigits(string tckn)
    {
        return tckn.All(char.IsDigit);
    }
}