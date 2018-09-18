using Debugtime.DataAccess.Core.IRepositories;
using System;
using System.Threading.Tasks;
using Debugtime.Common.Persistence;
using DebugTime.Domain.Model;

namespace Debugtime.DataAccess.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        private IGenericRepositories<Certification> _certGenRepo;
        private IGenericRepositories<CertificationPath> _certPathGenRepo;
        private IGenericRepositories<Course> _courseGenRepo;
        private IGenericRepositories<CreditCard> _cradetCardGenRepo;
        private IGenericRepositories<DisplayPicture> _dPGenRepo;
        private IGenericRepositories<Links> _linksGenRepo;
        private IGenericRepositories<Order> _ordersGenRepo;
        private IGenericRepositories<UserProfile> _userProfileGenRepo;
        private IGenericRepositories<Video> _videosGenRepo;
        private IGenericRepositories<ApplicationUser> _userGenRepo;

        private IUsersRepository _usersRepository;
        private IProfilesRepository _profileRepository;
        private ICoursesRepository _courseRepository;


        public UnitOfWork()
        {
            _db = new ApplicationDbContext();
        }


        IGenericRepositories<ApplicationUser> IUnitOfWork.UsersGenRepo
        {
            get
            {
                return _userGenRepo ?? (_userGenRepo = new GenericRepositories<ApplicationUser>(_db));
            }
        }

        IGenericRepositories<Certification> IUnitOfWork.CertsGenRepo
        {
            get
            {
                return _certGenRepo ??
                       (_certGenRepo = new GenericRepositories<Certification>(_db));
            }
        }

        IGenericRepositories<CertificationPath> IUnitOfWork.CertPathsRepo
        {
            get
            {
                return _certPathGenRepo ??
                       (_certPathGenRepo = new GenericRepositories<CertificationPath>(_db));
            }
        }

        IGenericRepositories<Course> IUnitOfWork.CoursesGenRepo
        {
            get { return _courseGenRepo ?? (_courseGenRepo = new GenericRepositories<Course>(_db)); }
        }

        IGenericRepositories<CreditCard> IUnitOfWork.CradetCardsGenRepo
        {
            get
            {
                return _cradetCardGenRepo ??
                  (_cradetCardGenRepo = new GenericRepositories<CreditCard>(_db));
            }
        }

        IGenericRepositories<DisplayPicture> IUnitOfWork.DisplayPicturespGenRepo
        {
            get { return _dPGenRepo ?? (_dPGenRepo = new GenericRepositories<DisplayPicture>(_db)); }
        }



        IGenericRepositories<Links> IUnitOfWork.UserLinksGenRepo
        {
            get { return _linksGenRepo ?? (_linksGenRepo = new GenericRepositories<Links>(_db)); }
        }

        IGenericRepositories<Order> IUnitOfWork.OrdersGenRepo
        {
            get { return _ordersGenRepo ?? (_ordersGenRepo = new GenericRepositories<Order>(_db)); }
        }

        IGenericRepositories<UserProfile> IUnitOfWork.UsersProfilesGenRepo
        {
            get { return _userProfileGenRepo ?? (_userProfileGenRepo = new GenericRepositories<UserProfile>(_db)); }
        }

        IGenericRepositories<Video> IUnitOfWork.VideosGenRepo
        {
            get
            {
                if (_videosGenRepo == null)
                    _videosGenRepo = new GenericRepositories<Video>(_db);
                return _videosGenRepo;
            }
        }

        public IUsersRepository UsersRepository
        {
            get { return _usersRepository ?? (_usersRepository = new UsersRepository(_db)); }
        }

        public IProfilesRepository ProfilesRepository
        {
            get
            {
                return _profileRepository ?? (_profileRepository = new ProfilesRepository(_db));
            }
        }

        public ICoursesRepository CoursesRepository
        {
            get
            {
                return _courseRepository ?? (_courseRepository = new CoursesRepository(_db));
            }
        }

        async Task<int> IUnitOfWork.SaveWorkAsync()
        {
            return await _db.SaveChangesAsync();
        }

        #region IDisposable Support
        private bool _disposedValue; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    _db.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                _disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~UnitOfWork() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
