using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Debugtime.Common.Model.Input
{
    public class UserProfileInputModel
    {
        public string UserAccountId { get; set; }

        //Personal Info
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

        [DisplayName("Job Title")]
        [MinLength(2, ErrorMessage = "Atleast 2 characters are required.")]
        [MaxLength(100, ErrorMessage = "Maximum 100 characters are allowed.")]
        public string JobTitle { get; set; }

        [MinLength(2, ErrorMessage = "Atleast 2 characters are required.")]
        [MaxLength(100, ErrorMessage = "Maximum 100 characters are allowed.")]
        public string Company { get; set; }

        public string Location { get; set; }

        [DisplayName("Short Bio")]
        [MaxLength(500, ErrorMessage = "Maximum 500 characters are allowed.")]
        public string ShortBio { get; set; }
        
        [Url(ErrorMessage ="Value must be a valid url.")]
        public string Website { get; set; }

        [Url(ErrorMessage = "Value must be a valid url.")]
        public string Facebook { get; set; }

        [Url(ErrorMessage = "Value must be a valid url.")]
        public string LinkedIn { get; set; }

        [DisplayName("Google+")]
        [Url(ErrorMessage = "Value must be a valid url.")]
        public string GooglePlus { get; set; }
    }
}