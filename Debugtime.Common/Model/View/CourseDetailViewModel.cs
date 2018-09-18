using DebugTime.Domain.Model;
using System;
using System.Collections.Generic;

namespace Debugtime.Common.Model.View
{
    public class CourseDetailViewModel
    {
        public CourseDetailViewModel()
        {
            PlayerModel = new MediaPlayerModel();
        }
        public string Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string AgendaItems { get; set; }
        public string Duration { get; set; }
        public double Price { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string Tags { get; set; }
        public Int64 UserCount { get; set; }

        public CourseAccessiblity Accessibility { get; set; }
        public CourselLevel Level { get; set; }
        public AuthorViewModel AuthorModel { get; set; }
        public MediaPlayerModel PlayerModel { get; set; }
        public IList<CourseReviewViewModel> CourseReviews { get; set; }
        public IList<CourseSectionViewModel> CourseSections { get; set; }
    }
}
