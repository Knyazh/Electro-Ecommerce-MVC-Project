using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Platform.ViewModels.User;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(25, MinimumLength = 3, ErrorMessage = "Name must be between 2 and 50 characters!!!")]
    [RegularExpression("^[A-Z][a-z]*$", ErrorMessage = "The input must start with an uppercase letter and contain only lowercase letters.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Lastname is required.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Last name must be between 2 and 50 characters!!!")]
    [RegularExpression("^[A-Z][a-z]*$", ErrorMessage = "The input must start with an uppercase letter and contain only lowercase letters.")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address format.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Email password is required.")]
    [MinLength(8, ErrorMessage = "Email password must be at least 8 characters!!!")]
    public string Password { get; set; }

    [Required]
    [Compare("Password", ErrorMessage = "Password's doesn't match")]
    public string ConfirmPassword { get; set; }
}
