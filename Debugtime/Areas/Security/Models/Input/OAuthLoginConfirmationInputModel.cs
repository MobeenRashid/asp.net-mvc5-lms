using System.ComponentModel.DataAnnotations;

namespace Debugtime.Areas.Security.Models.Input
{
    public class OAuthLoginConfirmationInputModel
    {
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is required.")]
        [MinLength(2, ErrorMessage = "Atleast 2 characters are required.")]
        [MaxLength(20, ErrorMessage = "Maximum 20 characters are allowed.")]
        public string FirstName { get; set; }


        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is required")]
        [MinLength(2, ErrorMessage = "Atleast 2 characters are required.")]
        [MaxLength(20, ErrorMessage = "Maximum 20 characters are allowed.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid email provided.")]
        public string Email { get; set; }
    }
}