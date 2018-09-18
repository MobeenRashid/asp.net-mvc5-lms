using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using DebugTime.Domain.Model;
using System.Data.Entity;

namespace Debugtime.Common.Persistence.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            MigrationsDirectory = @"Persistence\Migrations";
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            RoleManager<IdentityRole> roleManager =
                new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new IdentityRole("Admin"));
                roleManager.Create(new IdentityRole("User"));
                roleManager.Create(new IdentityRole("Author"));

            }

            if (!context.Courses.Any())
            {
                var courseList = new List<Course>
                {
                    new Course
                    {
                        Id = Guid.NewGuid().ToString(),
                        ProgrammingLanguege = "C#",
                        Title = "Forms with React and Redux",
                        Tags="JavaScript || Course",
                        Level = CourselLevel.Intermediate,
                        Duration = "2h 5m",
                        Accessibility=CourseAccessiblity.Free,
                        AuthorId = "8c12752e-fae9-4545-aaa5-6f0013e2d8ff",
                        Description = "Redux-form is the best way to save form state in Redux. When building a form, you may wish for the ability to save form state, allowing form values to be interacted with from anywhere in your application. Using redux-form allows you to easily create forms and use redux-form actions to manage your form’s state || This course will cover the basics of forms in React and Redux, explain the value of redux-form, and cover the basics of creation of forms using redux-form. Beyond that, you will also learn about validation of forms that were created with redux-form, and management of their error messages, allowing you to build robust forms while retaining the ability to save the state of form items || For more on Redux, learn about Redux design issues in our mini course Redux Design Issues and Testing. Redux is a popular implementation of Flux that helps us easily manage our application state and organize our React apps. Despite elegantly solving state management problems, Redux comes with a few design issues of its own. We explore those in our mini course.",
                        AgendaItems = "Learn the basics of forms with Redux and React || Save form states with Redux || Validate your form with redux-form || Closed captions available",
                        ThumbnailPath = "~/Content/Images/Courses/Thumbnail-1.png",
                        Price = 4.5
                    },
                    new Course
                    {
                        Id = Guid.NewGuid().ToString(),
                        ProgrammingLanguege = "C#",
                        Title = @"Responsive Tips and Tricks",
                        Tags=@"Html&Css || Course",
                        Level = CourselLevel.Proficient,
                        Duration = "1h 15m",
                        Accessibility=CourseAccessiblity.Premium,
                        AuthorId = "8c12752e-fae9-4545-aaa5-6f0013e2d8ff",
                        Description = @"Redux-form is the best way to save form state in Redux. When building a form, you may wish for the ability to save form state, allowing form values to be interacted with from anywhere in your application. Using redux-form allows you to easily create forms and use redux-form actions to manage your form’s state || This course will cover the basics of forms in React and Redux, explain the value of redux-form, and cover the basics of creation of forms using redux-form. Beyond that, you will also learn about validation of forms that were created with redux-form, and management of their error messages, allowing you to build robust forms while retaining the ability to save the state of form items || For more on Redux, learn about Redux design issues in our mini course Redux Design Issues and Testing. Redux is a popular implementation of Flux that helps us easily manage our application state and organize our React apps. Despite elegantly solving state management problems, Redux comes with a few design issues of its own. We explore those in our mini course.",
                        AgendaItems = @"Learn the basics of forms with Redux and React || Save form states with Redux || Validate your form with redux-form || Closed captions available",
                        ThumbnailPath = "~/Content/Images/Courses/Thumbnail-3.png",
                        Price = 7.5
                    },
                    new Course
                    {
                        Id = Guid.NewGuid().ToString(), ProgrammingLanguege = "C#",
                        Title = "Visualizing Data with D3.js",
                        Tags="JavaScript || Course",
                        Level = CourselLevel.Ninja,
                        Duration = "3h 24m",
                        Accessibility=CourseAccessiblity.Premium,
                        AuthorId = "8c12752e-fae9-4545-aaa5-6f0013e2d8ff",
                        Description = @"Redux-form is the best way to save form state in Redux. When building a form, you may wish for the ability to save form state, allowing form values to be interacted with from anywhere in your application. Using redux-form allows you to easily create forms and use redux-form actions to manage your form’s state || This course will cover the basics of forms in React and Redux, explain the value of redux-form, and cover the basics of creation of forms using redux-form. Beyond that, you will also learn about validation of forms that were created with redux-form, and management of their error messages, allowing you to build robust forms while retaining the ability to save the state of form items || For more on Redux, learn about Redux design issues in our mini course Redux Design Issues and Testing. Redux is a popular implementation of Flux that helps us easily manage our application state and organize our React apps. Despite elegantly solving state management problems, Redux comes with a few design issues of its own. We explore those in our mini course.",
                        AgendaItems = @"Learn the basics of forms with Redux and React || Save form states with Redux || Validate your form with redux-form || Closed captions available",
                        ThumbnailPath = "~/Content/Images/Courses/Thumbnail-2.png",
                        Price = 4.5
                    },
                    new Course
                    {
                        Id = Guid.NewGuid().ToString(), ProgrammingLanguege = "C#",
                        Title = "Exam Ref-483 from Novice to Ninja",
                        Tags="C# || Course",
                        Level = CourselLevel.Novice,
                        Duration = "4h 5m",
                        Accessibility=CourseAccessiblity.Premium,
                        AuthorId = "8c12752e-fae9-4545-aaa5-6f0013e2d8ff",
                        Description = "Redux-form is the best way to save form state in Redux. When building a form, you may wish for the ability to save form state, allowing form values to be interacted with from anywhere in your application. Using redux-form allows you to easily create forms and use redux-form actions to manage your form’s state || This course will cover the basics of forms in React and Redux, explain the value of redux-form, and cover the basics of creation of forms using redux-form. Beyond that, you will also learn about validation of forms that were created with redux-form, and management of their error messages, allowing you to build robust forms while retaining the ability to save the state of form items || For more on Redux, learn about Redux design issues in our mini course Redux Design Issues and Testing. Redux is a popular implementation of Flux that helps us easily manage our application state and organize our React apps. Despite elegantly solving state management problems, Redux comes with a few design issues of its own. We explore those in our mini course.",
                        AgendaItems = "Learn the basics of forms with Redux and React || Save form states with Redux || Validate your form with redux-form || Closed captions available",
                        Price = 40
                    },
                    new Course
                    {
                        Id = Guid.NewGuid().ToString(), ProgrammingLanguege = "C#",
                        Title = "Exam Ref-487 from Novice to Ninja",
                        Tags="Asp.Net || Course",
                        Level = CourselLevel.Proficient,
                        Duration = "3h 10m",
                        Accessibility=CourseAccessiblity.Free,
                        AuthorId = "8c12752e-fae9-4545-aaa5-6f0013e2d8ff",
                        Description = "Redux-form is the best way to save form state in Redux. When building a form, you may wish for the ability to save form state, allowing form values to be interacted with from anywhere in your application. Using redux-form allows you to easily create forms and use redux-form actions to manage your form’s state || This course will cover the basics of forms in React and Redux, explain the value of redux-form, and cover the basics of creation of forms using redux-form. Beyond that, you will also learn about validation of forms that were created with redux-form, and management of their error messages, allowing you to build robust forms while retaining the ability to save the state of form items || For more on Redux, learn about Redux design issues in our mini course Redux Design Issues and Testing. Redux is a popular implementation of Flux that helps us easily manage our application state and organize our React apps. Despite elegantly solving state management problems, Redux comes with a few design issues of its own. We explore those in our mini course.",
                        AgendaItems = "Learn the basics of forms with Redux and React || Save form states with Redux || Validate your form with redux-form || Closed captions available",
                        Price = 4.5
                    },
                    new Course
                    {
                        Id = Guid.NewGuid().ToString(), ProgrammingLanguege = "C#",
                        Title = "Exam Ref-586 from Novice to Ninja",
                        Tags="Microsoft Azure || Course",
                        Level = CourselLevel.Proficient,
                        Duration = "2h 5m",
                        Accessibility=CourseAccessiblity.Premium,
                        AuthorId = "8e79c8f1-c95f-429a-8078-aeb2490db4aa",
                        Description = "Redux-form is the best way to save form state in Redux. When building a form, you may wish for the ability to save form state, allowing form values to be interacted with from anywhere in your application. Using redux-form allows you to easily create forms and use redux-form actions to manage your form’s state || This course will cover the basics of forms in React and Redux, explain the value of redux-form, and cover the basics of creation of forms using redux-form. Beyond that, you will also learn about validation of forms that were created with redux-form, and management of their error messages, allowing you to build robust forms while retaining the ability to save the state of form items || For more on Redux, learn about Redux design issues in our mini course Redux Design Issues and Testing. Redux is a popular implementation of Flux that helps us easily manage our application state and organize our React apps. Despite elegantly solving state management problems, Redux comes with a few design issues of its own. We explore those in our mini course.",
                        AgendaItems = "Learn the basics of forms with Redux and React || Save form states with Redux || Validate your form with redux-form || Closed captions available",
                        ThumbnailPath = "~/Content/Images/Courses/Thumbnail-1.png",
                        Price = 70
                    },
                    new Course
                    {
                        Id = Guid.NewGuid().ToString(), ProgrammingLanguege = "C#",
                        Title = "Exam Ref-473 from Novice to Ninja",
                        Tags="Css || Course",
                        Level = CourselLevel.Intermediate,
                        Duration = "2h 5m",
                        Accessibility=CourseAccessiblity.Free,
                        AuthorId = "8e79c8f1-c95f-429a-8078-aeb2490db4aa",
                        Description = "Redux-form is the best way to save form state in Redux. When building a form, you may wish for the ability to save form state, allowing form values to be interacted with from anywhere in your application. Using redux-form allows you to easily create forms and use redux-form actions to manage your form’s state || This course will cover the basics of forms in React and Redux, explain the value of redux-form, and cover the basics of creation of forms using redux-form. Beyond that, you will also learn about validation of forms that were created with redux-form, and management of their error messages, allowing you to build robust forms while retaining the ability to save the state of form items || For more on Redux, learn about Redux design issues in our mini course Redux Design Issues and Testing. Redux is a popular implementation of Flux that helps us easily manage our application state and organize our React apps. Despite elegantly solving state management problems, Redux comes with a few design issues of its own. We explore those in our mini course.",
                        AgendaItems = "Learn the basics of forms with Redux and React || Save form states with Redux || Validate your form with redux-form || Closed captions available",
                        Price = 10
                    },
                    new Course
                    {
                        Id = Guid.NewGuid().ToString(), ProgrammingLanguege = "C#",
                        Title = "Exam Ref-673 from Novice to Ninja",
                        Tags="Microsoft || Exam ||Course",
                        Level = CourselLevel.Intermediate,
                        Duration = "5h 9m",
                        Accessibility=CourseAccessiblity.Free,
                        AuthorId = "8e79c8f1-c95f-429a-8078-aeb2490db4aa",
                        Description = "Redux-form is the best way to save form state in Redux. When building a form, you may wish for the ability to save form state, allowing form values to be interacted with from anywhere in your application. Using redux-form allows you to easily create forms and use redux-form actions to manage your form’s state || This course will cover the basics of forms in React and Redux, explain the value of redux-form, and cover the basics of creation of forms using redux-form. Beyond that, you will also learn about validation of forms that were created with redux-form, and management of their error messages, allowing you to build robust forms while retaining the ability to save the state of form items || For more on Redux, learn about Redux design issues in our mini course Redux Design Issues and Testing. Redux is a popular implementation of Flux that helps us easily manage our application state and organize our React apps. Despite elegantly solving state management problems, Redux comes with a few design issues of its own. We explore those in our mini course.",
                        AgendaItems = "Learn the basics of forms with Redux and React || Save form states with Redux || Validate your form with redux-form || Closed captions available",
                        ThumbnailPath = "~/Content/Images/Courses/Thumbnail-1.png",
                        Price = 46
                    },
                    new Course
                    {
                        Id = Guid.NewGuid().ToString(), ProgrammingLanguege = "C#",
                        Title = "Exam Ref-434 from Novice to Ninja",
                        Tags="Html5 || Course",
                        Level = CourselLevel.Intermediate,
                        Duration = "2h 5m",
                        Accessibility=CourseAccessiblity.Free,
                        AuthorId = "8e79c8f1-c95f-429a-8078-aeb2490db4aa",

                        Description = "Redux-form is the best way to save form state in Redux. When building a form, you may wish for the ability to save form state, allowing form values to be interacted with from anywhere in your application. Using redux-form allows you to easily create forms and use redux-form actions to manage your form’s state || This course will cover the basics of forms in React and Redux, explain the value of redux-form, and cover the basics of creation of forms using redux-form. Beyond that, you will also learn about validation of forms that were created with redux-form, and management of their error messages, allowing you to build robust forms while retaining the ability to save the state of form items || For more on Redux, learn about Redux design issues in our mini course Redux Design Issues and Testing. Redux is a popular implementation of Flux that helps us easily manage our application state and organize our React apps. Despite elegantly solving state management problems, Redux comes with a few design issues of its own. We explore those in our mini course.",
                        AgendaItems = "Learn the basics of forms with Redux and React || Save form states with Redux || Validate your form with redux-form || Closed captions available",
                        Price = 10
                    },
                    new Course
                    {
                        Id = Guid.NewGuid().ToString(), ProgrammingLanguege = "C#",
                        Title = "Exam Ref-773 from Novice to Ninja",
                        Tags="Microsoft || Exam ||Course",
                        Level = CourselLevel.Intermediate,
                        Duration = "5h 9m",
                        Accessibility=CourseAccessiblity.Free,
                        AuthorId = "8e79c8f1-c95f-429a-8078-aeb2490db4aa",

                        Description = "Redux-form is the best way to save form state in Redux. When building a form, you may wish for the ability to save form state, allowing form values to be interacted with from anywhere in your application. Using redux-form allows you to easily create forms and use redux-form actions to manage your form’s state || This course will cover the basics of forms in React and Redux, explain the value of redux-form, and cover the basics of creation of forms using redux-form. Beyond that, you will also learn about validation of forms that were created with redux-form, and management of their error messages, allowing you to build robust forms while retaining the ability to save the state of form items || For more on Redux, learn about Redux design issues in our mini course Redux Design Issues and Testing. Redux is a popular implementation of Flux that helps us easily manage our application state and organize our React apps. Despite elegantly solving state management problems, Redux comes with a few design issues of its own. We explore those in our mini course.",
                        AgendaItems = "Learn the basics of forms with Redux and React || Save form states with Redux || Validate your form with redux-form || Closed captions available",
                        Price = 16
                    }

                };

                foreach (var course in courseList)
                {
                    course.CourseSections = new List<CourseSection>
                    {
                        new CourseSection
                        {
                            CourseId = course.Id,
                            Title ="Introduction",
                            Order=1
                        },
                        new CourseSection
                        {
                            CourseId = course.Id,
                            Title ="Setting Up Enivornment",
                            Order=2
                        },
                        new CourseSection
                        {
                            CourseId = course.Id,
                            Title ="Getting Started",
                            Order=3
                        },
                        new CourseSection
                        {
                            CourseId = course.Id,
                            Title ="Bulding a Feature end to end",
                            Order=4
                        },
                        new CourseSection
                        {
                            CourseId = course.Id,
                            Title ="Conclusion",
                            Order=5
                        },
                        new CourseSection
                        {
                            CourseId = course.Id,
                            Title ="Next Steps",
                            Order=6
                        }
                    };
                }

                foreach (var sec in courseList)
                {
                    foreach (var vid in sec.CourseSections)
                    {
                        vid.Videos = new List<Video>
                        {
                            new Video
                            {
                                Title="Introduction",
                                IsFree = true,
                                Path="~/Static/Courses/Unique/001.mp4",
                                SectionTitle = vid.Title,
                                CourseSectionId = vid.CourseId,
                                Order = 1,
                                Duration ="5:00"
                            },
                            new Video
                            {
                                Title="MVC Architectural Pattern",
                                IsFree = false,
                                Path="~/Static/Courses/Unique/002.mp4",
                                SectionTitle = vid.Title,
                                CourseSectionId = vid.CourseId,
                                Order = 2,
                                Duration ="15:00"
                            },
                            new Video
                            {
                                Title="Setting Up the Development Environment",
                                IsFree = false,
                                Path="~/Static/Courses/Unique/003.mp4",
                                SectionTitle = vid.Title,
                                CourseSectionId = vid.CourseId,
                                Order = 3,
                                Duration ="6:00"
                            },
                            new Video
                            {
                                Title="Your First ASP.NET MVC App",
                                IsFree = false,
                                Path="~/Static/Courses/Unique/004.mp4",
                                SectionTitle = vid.Title,
                                CourseSectionId = vid.CourseId,
                                Order = 4,
                                Duration ="2:00"
                            }
                        };
                    }
                }
                context.Courses.AddRange(courseList);
            }


            context.SaveChanges();
        }
    }
}
