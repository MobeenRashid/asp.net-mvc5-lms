using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Debugtime.Master.Models.Input
{
    public class VideoInputModel
    {
        public string Id { get; set; }
        public string Order { get; set; }
        public string CourseSectionId { get; set; }
        public IEnumerable<SelectListItem> CoursesCatagory { get; set; }
        public bool IsFree { get; set; }
        public bool IsDeleted { get; set; }
        //[Display(Name = "Upload Video")]
        //[Required(ErrorMessage = "Video is required")]
        //[DataType(DataType.Upload)]


        [Display(Name = "Title")]
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [Display(Name = "Section Title")]
        [Required(ErrorMessage = "Section Title is required.")]
        public string SectionTitle { get; set; }


        //[Display(Name = "Duration")]
        //[Required(ErrorMessage = "Duration is required")]
        public string Duration { get; set; }

        public string Path { get; set; }

        public string FileName { get; set; }


    }
}