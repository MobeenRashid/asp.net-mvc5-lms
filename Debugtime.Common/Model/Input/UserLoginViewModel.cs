using System.ComponentModel.DataAnnotations;

namespace Debugtime.Common.Model.Input
{
    public class UserLoginViewModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Email is invalid.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RemeberMe { get; set; } = false;
    }
}