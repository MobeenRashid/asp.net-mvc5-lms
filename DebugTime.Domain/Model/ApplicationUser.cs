using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DebugTime.Domain.Model
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Orders = new HashSet<Order>();
            Subscriptions = new HashSet<Subscription>();
            CreditCards = new HashSet<CreditCard>();
            Assesments = new HashSet<Quiz>();
            Bookmarks = new HashSet<Bookmark>();
            UserCourseProgresses = new HashSet<UserCourseProgress>();
        }


        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime? DateModified { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }


        //navigational properties//
        public UserProfile UserProfile { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Subscription> Subscriptions { get; set; }
        public ICollection<CreditCard> CreditCards { get; set; }
        public ICollection<Quiz> Assesments { get; set; }
        public ICollection<Course> Courses { get; set; }
        public ICollection<CourseReview> CourseReviews { get; set; }
        public ICollection<Bookmark> Bookmarks { get; set; }
        public ICollection<UserCourseProgress> UserCourseProgresses { get; set; }
    }
}
