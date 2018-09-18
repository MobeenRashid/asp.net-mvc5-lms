using System;
using System.ComponentModel.DataAnnotations;

namespace Debugtime.Common.Model.Input
{
    public class UserEditInputModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid email provided.")]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date Created")]
        [DisplayFormat(DataFormatString = "{MM/dd/yyyy}",ApplyFormatInEditMode = true)]
        public DateTime? DateCreated { get; set; }

        [DataType(DataType.Date)]
        [Display(Name="Date Modified")]
        [DisplayFormat(DataFormatString = "{MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateModified { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Phone,Display(Name = "Phone Number")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid phone number provided e.g (092)-3416126378")]
        public string PhoneNumber { get; set; }

        public string UserName { get; set; }
    }
}