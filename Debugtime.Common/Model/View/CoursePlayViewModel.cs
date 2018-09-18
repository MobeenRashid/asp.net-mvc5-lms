using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Debugtime.Common.Persistence;
using Microsoft.AspNet.Identity.Owin;

namespace Debugtime.Common.Model.View
{
    public class CoursePlayViewModel
    {
        private ApplicationDbContext _context;
        public CoursePlayViewModel()
        {
            _context = HttpContext.Current.GetOwinContext().Get<ApplicationDbContext>();
            PlayerModel = new MediaPlayerModel();
        }

        public string Id { get; set; }

        public string Title { get; set; }
        public string LessonTitle { get; set; }

        public bool HasAssessment
        {
            get
            {
                if (String.IsNullOrEmpty(Id))
                    return false;

                return _context.Quizes.Any(q => q.QuizId == Id);
            }
        }

        public string Duration { get; set; }

        public bool HasNext { get; set; }
        public bool HasPrev { get; set; }
        public int NextLesson { get; set; }
        public int PrevLesson { get; set; }
        public int NextSection { get; set; }
        public int PrevSection { get; set; }

        public MediaPlayerModel PlayerModel { get; set; }
        public IList<CourseSectionViewModel> CourseSections { get; set; }

        public void SetNextLesson(int section, int lesson)
        {
            ++lesson;
            ++section;

            if(_context.Courses.Any(c=>c.CourseSections.Any(cs=>cs.Videos.Any(vid=>vid.Order==lesson))))
            {
                HasNext = true;
                NextLesson = lesson;
                NextSection = --section;
            }
            else if (_context.Courses.Any(c=>c.CourseSections.Any(cs=>cs.Order==section)))
            {
                HasNext = true;
                NextLesson = 1;
                NextSection = section;
            }
            else
            {
                HasNext = false;
            }

        }
        public void SetPrevLesson(int section, int lesson)
        {
            --lesson;
            --section;

            if(_context.Courses.Any(c=>c.CourseSections.Any(cs=>cs.Videos.Any(vid=>vid.Order==lesson))))
            {
                HasPrev = true;
                PrevLesson = lesson;
                PrevSection = ++section;
            }
            else if (_context.Courses.Any(c=>c.CourseSections.Any(cs=>cs.Order==section)))
            {
                HasPrev = true;
                PrevLesson = 1;
                PrevSection = section;
            }
            else
            {
                HasPrev = false;
            }

        }
    }
}