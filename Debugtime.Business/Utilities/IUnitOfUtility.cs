using Debugtime.Business.Utilities.Contracts;

namespace Debugtime.Business.Utilities
{
    public interface IUnitOfUtility
    {
        IProfileUtility ProfileUtility { get; }
        IUserUtility UserUtility { get; }
        ICourseUtility CourseUtility { get; }
        IOAuthUtility OAuthUtility { get; }
    }
}