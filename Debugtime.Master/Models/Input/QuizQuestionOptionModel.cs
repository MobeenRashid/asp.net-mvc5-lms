using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Debugtime.Master.Models.Input
{
    public class QuizQuestionOptionModel
    {
        [Required(ErrorMessage = "Please Select a Course.")]
        public string CourseId { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "Quiz Title is Required.")]
        public string QuizTitle { get; set; }

        [Required(ErrorMessage = "Please Select a Quiz.")]
        public string QuizId { get; set; }

        [Display(Name = "Question Title")]
        [Required(ErrorMessage = "Question is required.")]
        public string QuestionTitle { get; set; }

        [Display(Name = "Question Answer")]
        [Required(ErrorMessage = "Question Answer is required.")]
        public string QuestionAnswer { get; set; }

        [Required(ErrorMessage = "Please select a question.")]
        public string QuestionId { get; set; }

        [Display(Name = "Option Value")]
        [Required(ErrorMessage = @"Option value is required.")]
        public string OptionValue { get; set; }

    }
}