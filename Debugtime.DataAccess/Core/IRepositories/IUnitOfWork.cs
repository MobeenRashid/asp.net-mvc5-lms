using System;
using System.Threading.Tasks;
using DebugTime.Domain.Model;

namespace Debugtime.DataAccess.Core.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepositories<ApplicationUser> UsersGenRepo { get; }
        IGenericRepositories<Certification> CertsGenRepo { get; }
        IGenericRepositories<CertificationPath> CertPathsRepo { get; }
        IGenericRepositories<Course> CoursesGenRepo { get; }
        IGenericRepositories<CreditCard> CradetCardsGenRepo { get; }
        IGenericRepositories<DisplayPicture> DisplayPicturespGenRepo { get; }
        IGenericRepositories<Links> UserLinksGenRepo { get; }
        IGenericRepositories<Order> OrdersGenRepo { get; }
        IGenericRepositories<UserProfile> UsersProfilesGenRepo { get; }
        IGenericRepositories<Video> VideosGenRepo { get; }

        IUsersRepository UsersRepository { get; }
        IProfilesRepository ProfilesRepository { get; }
        ICoursesRepository CoursesRepository { get; }
        Task<int> SaveWorkAsync();
    }
}
