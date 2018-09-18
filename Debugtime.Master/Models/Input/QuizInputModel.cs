using System.ComponentModel.DataAnnotations;

namespace Debugtime.Master.Models.Input
{
    public class QuizInputModel
    {
        [Required(ErrorMessage = "Please Select a Course.")]
        public string CourseId { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "Quiz Title is Required.")]
        public string QuizTitle { get; set; }
    }
}