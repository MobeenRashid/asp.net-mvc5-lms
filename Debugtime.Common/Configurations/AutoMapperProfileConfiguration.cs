using AutoMapper;
using Debugtime.Common.Dtos;
using Debugtime.Common.Model.View;
using DebugTime.Domain.Model;

namespace Debugtime.Common.Configurations
{

    public class AutoMapperProfileConfiguration : Profile
    {
        private static IMapper _entityMapper;
        public IMapper EntityMapper
        {
            get
            {
                if (_entityMapper != null)
                    return _entityMapper;

                var config = new MapperConfiguration(
                    cfg =>
                    {
                        cfg.CreateMap<ApplicationUser, ApplicationUserDto>().ReverseMap();
                        cfg.CreateMap<ApplicationUser, UserRegisterDto>().ReverseMap();
                        cfg.CreateMap<ApplicationUser, UserReviewModel>().ReverseMap();

                        cfg.CreateMap<UserProfile, UserProfileInputDto>().ReverseMap();
                        cfg.CreateMap<UserProfile, UserProfileListDto>().ReverseMap();
                        cfg.CreateMap<UserProfile, ApplicationUserDto>().ReverseMap();
                        cfg.CreateMap<UserProfile, UserReviewModel>().ReverseMap();

                        cfg.CreateMap<Links, UserProfileInputDto>().ReverseMap();

                        cfg.CreateMap<UserProfile, UserProfileViewDto>().ReverseMap();
                        cfg.CreateMap<UserProfile, AuthorViewModel>().ReverseMap();
                        cfg.CreateMap<Links, UserProfileViewDto>().ReverseMap();


                        cfg.CreateMap<DisplayPicture, UserAvatarDto>().ReverseMap();
                        cfg.CreateMap<Course, CourseDto>().ReverseMap();
                        cfg.CreateMap<Course, CourseCardViewModel>().ReverseMap();
                        cfg.CreateMap<Course, CourseDetailViewModel>().ReverseMap();
                        cfg.CreateMap<Course, CoursePlayViewModel>().ReverseMap();
                        cfg.CreateMap<Course, CourseDashboardViewModel>().ReverseMap();
                        cfg.CreateMap<Course, CourseFlexViewModel>().ReverseMap();

                        cfg.CreateMap<Catagory, CourseCatagoryDto>().ReverseMap();
                        cfg.CreateMap<Video, CourseVideoDto>().ReverseMap();

                        cfg.CreateMap<UserRegisterDto, OAuthRegisterDto>().ReverseMap();

                        cfg.CreateMap<CourseReview, CourseReviewViewModel>().ReverseMap();

                        cfg.CreateMap<CourseSection, CourseSectionViewModel>().ReverseMap();

                        cfg.CreateMap<Video, VideoViewModel>().ReverseMap();

                        cfg.CreateMap<Bookmark, BookmarkViewModel>().ReverseMap();

                        cfg.CreateMap<Quiz, AssessmentQuizViewModel>().ReverseMap();

                        cfg.CreateMap<QuizQuestion, AssessmentQuestionViewModel>().ReverseMap();
                        cfg.CreateMap<QuestionOption, AssessmentQuestionOptionViewModel>().ReverseMap();

                        cfg.CreateMap<UserQuizAnswer, UserQuizAnswerDto>().ReverseMap();

                        cfg.CreateMap<UserCourseProgress, UserCourseProgressViewModel>();
                    });

                _entityMapper = config.CreateMapper();
                return _entityMapper;
            }
        }
    }
}