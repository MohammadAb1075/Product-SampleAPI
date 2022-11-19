using System.ComponentModel.DataAnnotations;

namespace SampleAPI.Models
{
    public class RegisterViewModel
    {
        
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,}$")]
        //[StringLength(256, ErrorMessage = "Confirm password doesn't match, Try again !")]
        [Required(ErrorMessage = "Password is required")]
        [StringLength(256, MinimumLength = 4, ErrorMessage = "{0} Length Must Be Between{1} & {2} Characters.")]
        public string Password { get; set; }

        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,}$")]
        //[StringLength(256, ErrorMessage = "Confirm password doesn't match, Try again !")]
        [Required(ErrorMessage = "Password is required")]
        [Compare("Password", ErrorMessage = "{0} Length Must Be Between{1} & {2} Characters.")]
        [StringLength(256, MinimumLength = 4, ErrorMessage = "Confirm password doesn't match, Try again !")]
        public string ConfirmPassword { get; set; }
    }
}