using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DebugTime.Domain.Model;

namespace Debugtime.Common.Dtos
{
    public class CourseDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AgendaItems { get; set; }
        public string Duration { get; set; }
        public string ProgrammingLanguege { get; set; }
        public string StartDate { get; set; }
        public double Price { get; set; }
        public string TitleImagePath { get; set; }
        public string CatagoryId { get; set; }
        public string Tags { get; set; }
        public string AuthorId { get; set; }
        public CourselLevel Level { get; set; }
        public CourseAccessiblity CourseAccessiblity { get; set; }
    }
}
