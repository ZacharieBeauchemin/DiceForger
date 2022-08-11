using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class NewUserModel {
    //TODO: Make language independant
    [Required(ErrorMessage = "The username is required")]
    [RegularExpression(@"^[a-zA-Z0-9._-]*$", ErrorMessage = "The username in invalid.")]
    [StringLength(32, ErrorMessage = "The username must contain between {2} and {1} characters.", MinimumLength = 3)]
    [DataType(DataType.Text)]
    [Display(Name = "Username")]
    public string Username { get; set; } = "";

    [Required(ErrorMessage = "The email is required")]
    [EmailAddress(ErrorMessage = "The email is invalid.")]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email")]
    public string Email { get; set; } = "";

    [Required(ErrorMessage = "The password is required")]
    [StringLength(32, ErrorMessage = "The password must contain between {2} and {1} characters.", MinimumLength = 8)]
    [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*_-]).*", ErrorMessage = "The password must contain a number, an upper and lower case letter as well as a special character.")]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; } = "";
}