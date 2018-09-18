using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Debugtime.Master.Models.Input
{
    public class UserRegisterViewModel
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

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(5, ErrorMessage = "Atleast 5 characters are required")]
        [MaxLength(50, ErrorMessage = "Maximum 50 characters are allowed.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required.")]
        [Compare("Password", ErrorMessage = "Confirm password didn't match.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}