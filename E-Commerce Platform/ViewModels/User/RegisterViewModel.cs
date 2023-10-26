using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Platform.ViewModels.User;

public class RegisterViewModel
{
    //[StringLength(25, MinimumLength = 3, ErrorMessage = "Name must be between 2 and 50 characters!!!")]
    //[RegularExpression("^[A-Z][a-z]*$", ErrorMessage = "The input must start with an uppercase letter and contain only lowercase letters.")]
    [Required(ErrorMessage = "Please enter name")]
    public string Name { get; set; }

    //[StringLength(50, MinimumLength = 3, ErrorMessage = "Last name must be between 2 and 50 characters!!!")]
    //[RegularExpression("^[A-Z][a-z]*$", ErrorMessage = "The input must start with an uppercase letter and contain only lowercase letters.")]
    [Required(ErrorMessage = "Lastname is required.")]
    public string LastName { get; set; }

    //[EmailAddress(ErrorMessage = "Invalid email address format.")]
    [Required(ErrorMessage = "Email is required.")]
    public string Email { get; set; }

    //[MinLength(8, ErrorMessage = "Email password must be at least 8 characters!!!")]
    [Required(ErrorMessage = "Email password is required.")]
    public string Password { get; set; }

    [Required]
    [Compare("Password", ErrorMessage = "Password's doesn't match")]
    public string ConfirmPassword { get; set; }

    public string PIN { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string PhysicalImageName { get; set; }
}
