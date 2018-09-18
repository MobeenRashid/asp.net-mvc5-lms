using System.ComponentModel.DataAnnotations;

namespace Debugtime.Master.Models.Input
{
    public class QuestionOptionInputModel
    {
        [Required(ErrorMessage = "Please select a question.")]
        public string QuestionId { get; set; }

        public string OptionQuestionTitle { get; set; }

        [Display(Name = "Option Value")]
        [Required(ErrorMessage = @"Option value is required.")]
        public string OptionValue { get; set; }

        [Required(ErrorMessage = "Please Select a Quiz.")]
        public string QuestionQuizId { get; set; }

    }
}