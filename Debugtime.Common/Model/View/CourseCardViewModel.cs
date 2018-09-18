using System;
using System.Collections.Generic;
using Debugtime.Common.Helpers;
using DebugTime.Domain.Model;

namespace Debugtime.Common.Model.View
{
    public class CourseCardViewModel
    {
        public CourseCardViewModel()
        {
            CourseReviews = new HashSet<CourseReview>();
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
        public double Price { get; set; }
        public string ThumbnailPath { get; set; }
        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public string LastUpdate => AppTimer.LastUpdateTime(this.DateCreated);

        public string AuthorName { get; set; }
        public CourseAccessiblity Accessibility { get; set; }
        public CourselLevel Level { get; set; }
        public IEnumerable<CourseReview> CourseReviews { get; set; }
        public IEnumerable<BookmarkViewModel> Bookmarks { get; set; }
        public IEnumerable<UserCourseProgress> UserCourseProgresses { get; set; }

    }
}