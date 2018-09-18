using System;
using System.Collections.Generic;

namespace DebugTime.Domain.Model
{
    public class Course
    {
        public Course()
        {
            CertificationPaths = new HashSet<CertificationPath>();
            CourseSections = new HashSet<CourseSection>();
            CourseReviews = new HashSet<CourseReview>();
            UserCourseProgresses = new HashSet<UserCourseProgress>();
        }

        public string Id { get; set; }

        public string Title { get; set; }
        public string ProgrammingLanguege { get; set; }
        public string Description { get; set; }
        public string AgendaItems { get; set; }
        public string Duration { get; set; }
        public double Price { get; set; }
        public string ThumbnailPath { get; set; }
        public bool HasAssessment { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateModified { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }
        public string Tags { get; set; }
        public Int64 UserCount { get; set; }
        public CourseAccessiblity Accessibility { get; set; } = CourseAccessiblity.Free;
        public CourselLevel Level { get; set; }



        public string CatagoryId { get; set; }
        public string QuizId { get; set; }
        public string AuthorId { get; set; }


        //navigational properties//
        public Catagory Catagory { get; set; }
        public Quiz Quiz { get; set; }
        public ApplicationUser Author { get; set; }

        public ICollection<CertificationPath> CertificationPaths { get; set; }
        public ICollection<CourseSection> CourseSections { get; set; }
        public ICollection<CourseReview> CourseReviews { get; set; }
        public ICollection<Bookmark> Bookmarks { get; set; }
        public ICollection<UserCourseProgress> UserCourseProgresses { get; set; }
    }
}