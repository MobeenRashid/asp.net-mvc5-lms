using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Debugtime.Master.Models.Input
{
    public class CourseCatagoryModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Slag is required.")]
        public string Slag { get; set; }

       

    }
}