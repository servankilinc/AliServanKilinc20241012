using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels;

public class VMChangePassword
{
    [Required]
    [MinLength(6)]
    public string OldPassword { get; set; } = null!;
    [Required]
    [MinLength(6)]
    public string NewPassword { get; set; } = null!;
    [Required]
    [MinLength(6)] 
    public string NewPasswordAgain { get; set; } = null!;
}