using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Web.Http.ModelBinding;
using Debugtime.Common.Persistence.EntityConfigurations;
using Debugtime.Common.Persistence.Migrations;
using DebugTime.Domain.Model;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Debugtime.Common.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext() : base("name=DebugTimeConnection")
        {

        }

        public static ApplicationDbContext Creat()
        {
            return new ApplicationDbContext();
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<CreditCard> CradetCards { get; set; }
        public DbSet<Links> Links { get; set; }
        public DbSet<DisplayPicture> DisplayPictures { get; set; }
        public DbSet<Certification> Certifications { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CertificationPath> CertificationPaths { get; set; }
        public DbSet<Course> Courses { get; set; }

        public DbSet<Video> Videos { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<UserCourseProgress> UsersCoursesProgresses { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<CourseSection> CourseSections { get; set; }
        public DbSet<CourseReview> CourseReviews { get; set; }
        public DbSet<Quiz> Quizes { get; set; }
        public DbSet<QuizQuestion> QuizQuestions { get; set; }
        public DbSet<QuestionOption> QuestionOptions { get; set; }
        public DbSet<UserQuizAnswer> UserQuizAnswers { get; set; }
        public DbSet<Catagory> Catagory { get; set; }
        public DbSet<Bookmark> UserBookmarks { get; set; }
        public DbSet<UserCourse> UserCourses { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Statement> Statements { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ApplicationUserEntityConfiguration());
            modelBuilder.Configurations.Add(new CertificationEntityConfiguration());
            modelBuilder.Configurations.Add(new CertificationPathEntityConfiguration());
            modelBuilder.Configurations.Add(new CourseEntityConfiguration());
            modelBuilder.Configurations.Add(new CreditCardEntityConfiguration());
            modelBuilder.Configurations.Add(new DisplayPictureEntityConfiguration());
            modelBuilder.Configurations.Add(new LinksEntityConfiguration());
            modelBuilder.Configurations.Add(new OrderEntityConfiguration());
            modelBuilder.Configurations.Add(new SubscriptionEntityConfiguration());
            modelBuilder.Configurations.Add(new UserProfileEntityConfiguration());
            modelBuilder.Configurations.Add(new VideoEntityConfiguration());
            modelBuilder.Configurations.Add(new UserCourseProgressEntityConfiguration());
            modelBuilder.Configurations.Add(new OrderDetailEntityConfiguration());
            modelBuilder.Configurations.Add(new CourseSectionEntityConfiguration());
            modelBuilder.Configurations.Add(new CourseReviewEntityConfiguration());
            modelBuilder.Configurations.Add(new QuizEntityConfiguration());
            modelBuilder.Configurations.Add(new QuizQuestionEntityConfiguration());
            modelBuilder.Configurations.Add(new QuestionOptionEntityConfiguration());
            modelBuilder.Configurations.Add(new UserQuizAnswerEntityConfiguration());
            modelBuilder.Configurations.Add(new CatagoryEntityConfiguration());
            modelBuilder.Configurations.Add(new UserBookmarkEntityConfiguration());
            modelBuilder.Configurations.Add(new UserCourseEntityConfiguration());
            modelBuilder.Configurations.Add(new TransactionEntityConfiguration());
            modelBuilder.Configurations.Add(new StatementEntityConfiguration());

            base.OnModelCreating(modelBuilder);
        }


    }
}
