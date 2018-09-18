using System.ComponentModel.DataAnnotations;

namespace Debugtime.Master.Models.Input
{
    public class QuizQuestionInputModel
    {
        [Display(Name = "Question Title")]
        [Required(ErrorMessage = "Question is required.")]
        public string QuestionTitle { get; set; }

        [Display(Name = "Question Answer")]
        [Required(ErrorMessage = "Question Answer is required.")]
        public string QuestionAnswer { get; set; }

        [Required(ErrorMessage = "Please Select a Quiz.")]
        public string QuizId { get; set; }
    }
}