namespace Debugtime.Common.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dblevel4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Catagories",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Title = c.String(nullable: false),
                        Slag = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CertificationPaths",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CertName = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Title = c.String(nullable: false),
                        ProgrammingLanguege = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        AgendaItems = c.String(nullable: false),
                        Duration = c.String(),
                        Price = c.Double(nullable: false),
                        ThumbnailPath = c.String(),
                        HasAssessment = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Tags = c.String(),
                        UserCount = c.Long(nullable: false),
                        Accessibility = c.Int(nullable: false),
                        Level = c.Int(nullable: false),
                        CatagoryId = c.String(maxLength: 128),
                        QuizId = c.String(),
                        AuthorId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AuthorId)
                .ForeignKey("dbo.Catagories", t => t.CatagoryId)
                .Index(t => t.CatagoryId)
                .Index(t => t.AuthorId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        Email = c.String(nullable: false, maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.Quizs",
                c => new
                    {
                        QuizId = c.String(nullable: false, maxLength: 128),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.QuizId)
                .ForeignKey("dbo.Courses", t => t.QuizId, cascadeDelete: true)
                .Index(t => t.QuizId);
            
            CreateTable(
                "dbo.QuizQuestions",
                c => new
                    {
                        Key = c.String(nullable: false, maxLength: 128),
                        QuizId = c.String(nullable: false, maxLength: 128),
                        Title = c.String(),
                        Answer = c.String(nullable: false),
                        Order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Key, t.QuizId })
                .ForeignKey("dbo.Quizs", t => t.QuizId, cascadeDelete: true)
                .Index(t => t.QuizId);
            
            CreateTable(
                "dbo.QuestionOptions",
                c => new
                    {
                        Key = c.String(nullable: false, maxLength: 128),
                        QuestionId = c.String(nullable: false, maxLength: 128),
                        Value = c.String(nullable: false),
                        QuestionTitle = c.String(),
                        QuestionKey = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Key, t.QuestionId })
                .ForeignKey("dbo.QuizQuestions", t => new { t.QuestionKey, t.QuestionId }, cascadeDelete: true)
                .Index(t => new { t.QuestionKey, t.QuestionId });
            
            CreateTable(
                "dbo.Bookmarks",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        CourseId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.CourseId })
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.CourseReviews",
                c => new
                    {
                        CourseId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        Stars = c.Int(nullable: false),
                        Review = c.String(),
                    })
                .PrimaryKey(t => new { t.CourseId, t.UserId })
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.CourseId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.CreditCards",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CardNumber = c.String(),
                        AccountId = c.String(nullable: false, maxLength: 128),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AccountId, cascadeDelete: true)
                .Index(t => t.AccountId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        SubscriptionId = c.Guid(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subscriptions", t => t.SubscriptionId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.SubscriptionId);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        OrderId = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                        OrderDate = c.DateTime(nullable: false),
                        OrderAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderDiscount = c.Decimal(precision: 18, scale: 2),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.Subscriptions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.UserCourseProgresses",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        CourseId = c.String(nullable: false, maxLength: 128),
                        Level = c.Int(nullable: false),
                        Lesson = c.Int(nullable: false),
                        Progress = c.Int(nullable: false),
                        IsRecent = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.CourseId })
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.UserProfiles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Location = c.String(),
                        JobTitle = c.String(),
                        ShortBio = c.String(),
                        Company = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Certifications",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        Company = c.String(),
                        DateIssued = c.DateTime(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ProfileId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfiles", t => t.ProfileId, cascadeDelete: true)
                .Index(t => t.ProfileId);
            
            CreateTable(
                "dbo.DisplayPictures",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FileName = c.String(),
                        ContentType = c.String(nullable: false, maxLength: 100),
                        Content = c.Binary(nullable: false),
                        IsCurrent = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        ProfileId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfiles", t => t.ProfileId, cascadeDelete: true)
                .Index(t => t.ProfileId);
            
            CreateTable(
                "dbo.Links",
                c => new
                    {
                        UserProfileId = c.String(nullable: false, maxLength: 128),
                        Website = c.String(),
                        Facebook = c.String(),
                        LinkedIn = c.String(),
                        GooglePlus = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.UserProfileId)
                .ForeignKey("dbo.UserProfiles", t => t.UserProfileId, cascadeDelete: true)
                .Index(t => t.UserProfileId);
            
            CreateTable(
                "dbo.CourseSections",
                c => new
                    {
                        CourseId = c.String(nullable: false, maxLength: 128),
                        Title = c.String(nullable: false, maxLength: 128),
                        Order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CourseId, t.Title })
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.Videos",
                c => new
                    {
                        CourseSectionId = c.String(nullable: false, maxLength: 128),
                        SectionTitle = c.String(nullable: false, maxLength: 128),
                        Path = c.String(nullable: false, maxLength: 128),
                        Title = c.String(nullable: false),
                        Order = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        IsFree = c.Boolean(nullable: false),
                        Duration = c.String(),
                    })
                .PrimaryKey(t => new { t.CourseSectionId, t.SectionTitle, t.Path })
                .ForeignKey("dbo.CourseSections", t => new { t.CourseSectionId, t.SectionTitle }, cascadeDelete: true)
                .Index(t => new { t.CourseSectionId, t.SectionTitle });
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.UserCourses",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        CourseId = c.String(nullable: false, maxLength: 128),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.CourseId })
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.UserQuizAnswers",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        QuizId = c.String(nullable: false, maxLength: 128),
                        QuestionKey = c.String(nullable: false, maxLength: 128),
                        Answer = c.String(nullable: false),
                        AnswerKey = c.String(nullable: false),
                        IsCorrect = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.QuizId, t.QuestionKey });
            
            CreateTable(
                "dbo.QuizesCandidates",
                c => new
                    {
                        Quiz_QuizId = c.String(nullable: false, maxLength: 128),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Quiz_QuizId, t.ApplicationUser_Id })
                .ForeignKey("dbo.Quizs", t => t.Quiz_QuizId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => t.Quiz_QuizId)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.ApplicationUserSubscriptions",
                c => new
                    {
                        ApplicationUserRefId = c.String(nullable: false, maxLength: 128),
                        SubscriptionRefId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUserRefId, t.SubscriptionRefId })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserRefId, cascadeDelete: true)
                .ForeignKey("dbo.Subscriptions", t => t.SubscriptionRefId, cascadeDelete: true)
                .Index(t => t.ApplicationUserRefId)
                .Index(t => t.SubscriptionRefId);
            
            CreateTable(
                "dbo.CoursesCertificationPathsTable",
                c => new
                    {
                        CourseRefId = c.String(nullable: false, maxLength: 128),
                        CertificationPathRefId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.CourseRefId, t.CertificationPathRefId })
                .ForeignKey("dbo.CertificationPaths", t => t.CourseRefId, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.CertificationPathRefId, cascadeDelete: true)
                .Index(t => t.CourseRefId)
                .Index(t => t.CertificationPathRefId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserCourses", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserCourses", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.CoursesCertificationPathsTable", "CertificationPathRefId", "dbo.Courses");
            DropForeignKey("dbo.CoursesCertificationPathsTable", "CourseRefId", "dbo.CertificationPaths");
            DropForeignKey("dbo.Quizs", "QuizId", "dbo.Courses");
            DropForeignKey("dbo.CourseSections", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Videos", new[] { "CourseSectionId", "SectionTitle" }, "dbo.CourseSections");
            DropForeignKey("dbo.Courses", "CatagoryId", "dbo.Catagories");
            DropForeignKey("dbo.UserProfiles", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Links", "UserProfileId", "dbo.UserProfiles");
            DropForeignKey("dbo.DisplayPictures", "ProfileId", "dbo.UserProfiles");
            DropForeignKey("dbo.Certifications", "ProfileId", "dbo.UserProfiles");
            DropForeignKey("dbo.UserCourseProgresses", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserCourseProgresses", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.ApplicationUserSubscriptions", "SubscriptionRefId", "dbo.Subscriptions");
            DropForeignKey("dbo.ApplicationUserSubscriptions", "ApplicationUserRefId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Orders", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Orders", "SubscriptionId", "dbo.Subscriptions");
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CreditCards", "AccountId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Courses", "AuthorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CourseReviews", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CourseReviews", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Bookmarks", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Bookmarks", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.QuizQuestions", "QuizId", "dbo.Quizs");
            DropForeignKey("dbo.QuestionOptions", new[] { "QuestionKey", "QuestionId" }, "dbo.QuizQuestions");
            DropForeignKey("dbo.QuizesCandidates", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.QuizesCandidates", "Quiz_QuizId", "dbo.Quizs");
            DropIndex("dbo.CoursesCertificationPathsTable", new[] { "CertificationPathRefId" });
            DropIndex("dbo.CoursesCertificationPathsTable", new[] { "CourseRefId" });
            DropIndex("dbo.ApplicationUserSubscriptions", new[] { "SubscriptionRefId" });
            DropIndex("dbo.ApplicationUserSubscriptions", new[] { "ApplicationUserRefId" });
            DropIndex("dbo.QuizesCandidates", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.QuizesCandidates", new[] { "Quiz_QuizId" });
            DropIndex("dbo.UserCourses", new[] { "CourseId" });
            DropIndex("dbo.UserCourses", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Videos", new[] { "CourseSectionId", "SectionTitle" });
            DropIndex("dbo.CourseSections", new[] { "CourseId" });
            DropIndex("dbo.Links", new[] { "UserProfileId" });
            DropIndex("dbo.DisplayPictures", new[] { "ProfileId" });
            DropIndex("dbo.Certifications", new[] { "ProfileId" });
            DropIndex("dbo.UserProfiles", new[] { "Id" });
            DropIndex("dbo.UserCourseProgresses", new[] { "CourseId" });
            DropIndex("dbo.UserCourseProgresses", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            DropIndex("dbo.Orders", new[] { "SubscriptionId" });
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.CreditCards", new[] { "AccountId" });
            DropIndex("dbo.CourseReviews", new[] { "UserId" });
            DropIndex("dbo.CourseReviews", new[] { "CourseId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.Bookmarks", new[] { "CourseId" });
            DropIndex("dbo.Bookmarks", new[] { "UserId" });
            DropIndex("dbo.QuestionOptions", new[] { "QuestionKey", "QuestionId" });
            DropIndex("dbo.QuizQuestions", new[] { "QuizId" });
            DropIndex("dbo.Quizs", new[] { "QuizId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Courses", new[] { "AuthorId" });
            DropIndex("dbo.Courses", new[] { "CatagoryId" });
            DropTable("dbo.CoursesCertificationPathsTable");
            DropTable("dbo.ApplicationUserSubscriptions");
            DropTable("dbo.QuizesCandidates");
            DropTable("dbo.UserQuizAnswers");
            DropTable("dbo.UserCourses");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Videos");
            DropTable("dbo.CourseSections");
            DropTable("dbo.Links");
            DropTable("dbo.DisplayPictures");
            DropTable("dbo.Certifications");
            DropTable("dbo.UserProfiles");
            DropTable("dbo.UserCourseProgresses");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Subscriptions");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Orders");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.CreditCards");
            DropTable("dbo.CourseReviews");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.Bookmarks");
            DropTable("dbo.QuestionOptions");
            DropTable("dbo.QuizQuestions");
            DropTable("dbo.Quizs");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Courses");
            DropTable("dbo.CertificationPaths");
            DropTable("dbo.Catagories");
        }
    }
}
