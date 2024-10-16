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
                .Must(BeAllDigits).WithMessage("T.C. Kimlik Numarası yalnızca sayılardan oluşmalıdır.")
                .Must(IsValidTCKN).WithMessage("Geçersiz T.C. Kimlik Numarası.");
        });

        RuleFor(b => new { b.TCKNO, b.Email })
              .Must(fields => !string.IsNullOrEmpty(fields.TCKNO) || !string.IsNullOrEmpty(fields.Email))
              .WithMessage("T.C. Kimlik Numarası veya Email alanlarından en az biri dolu olmalıdır.");
    }


    private bool BeAllDigits(string tckn)
    {
        return tckn.All(char.IsDigit);
    }

    private bool IsValidTCKN(string tckn)
    {
        if (tckn.Length != 11 || tckn.StartsWith("0")) return false;

        var digits = tckn.Select(c => c - '0').ToArray();
        int sumOdd = digits[0] + digits[2] + digits[4] + digits[6] + digits[8];
        int sumEven = digits[1] + digits[3] + digits[5] + digits[7];

        int checkDigit10 = (sumOdd * 7 - sumEven) % 10;
        int checkDigit11 = (digits.Take(10).Sum()) % 10;

        return digits[9] == checkDigit10 && digits[10] == checkDigit11;
    }
}