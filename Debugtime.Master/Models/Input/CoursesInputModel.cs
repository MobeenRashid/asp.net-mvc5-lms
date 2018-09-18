using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Debugtime.Master.Models.Input
{
    public class CoursesInputModel
    {
   
        public string Id { get; set; }
        public string AuthorId { get; set; }
        public string ProgrammingLanguege { get; set; }
        [Display(Name = "Title")]
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }


        [Display(Name = "Description")]
        [Required(ErrorMessage = "Desciption is required")]
        public string Description { get; set; }

        [Display(Name = "Agenda Items")]
        [Required(ErrorMessage = "Agenda Items is required")]
        public string AgendaItems { get; set; }

        [Display(Name = "Tags")]
        [Required(ErrorMessage = "Tags is required")]
        public string Tags { get; set; }

        [Display(Name = "Duration")]
        [Required(ErrorMessage = "Duration is required")]
        public string Duration { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "Price is required")]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        [Display(Name = "Start Date")]
        [Required(ErrorMessage = "Start Date is required")]
        public string StartDate { get; set; }

        [Display(Name = "Upload Image")]
        //[Required(ErrorMessage = "Image is required")]
        //[DataType(DataType.Upload)]
        public string TitleImagePath { get; set; }

        [Required(ErrorMessage = "CatagoryId is required")]
        public string CatagoryId { get; set; }

        public IEnumerable<SelectListItem> CoursesCatagory { get; set; }
    }
}