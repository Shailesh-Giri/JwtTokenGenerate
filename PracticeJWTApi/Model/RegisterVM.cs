using System.ComponentModel.DataAnnotations;

namespace PracticeJWTApi.Model
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "UserName can't be blank")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email can't be blank")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Email can't be blank")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email can't be blank")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email can't be blank")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
