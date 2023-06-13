using System.ComponentModel.DataAnnotations;

namespace PracticeJWTApi.Model
{
    public class SignInVM
    {
        [Required(ErrorMessage = "EmailOrUserName can't be blank")]
        public string EmailOrUserName { get; set; }

        [Required(ErrorMessage = "Password can't be blank")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; } = false;
    }
}
