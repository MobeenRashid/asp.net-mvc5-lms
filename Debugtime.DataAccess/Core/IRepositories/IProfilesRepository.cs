using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Debugtime.Common.Dtos;
using System.Linq.Expressions;
using DebugTime.Domain.Model;

namespace Debugtime.DataAccess.Core.IRepositories
{
    public interface IProfilesRepository
    {
         Task<DisplayPicture> GetDisplayPictureAsync(Expression<Func<DisplayPicture, bool>> predicate);

        Task<UserProfile> GetProfileAsync(Expression<Func<UserProfile, bool>> predicate);
        

        Task<UserProfile> GetProfileAsync<T>(Expression<Func<UserProfile, bool>> predicate, Expression<Func<UserProfile, T>> includeT);

        Task<UserProfile> GetProfileAsync<T1,T2>(Expression<Func<UserProfile, bool>> predicate, Expression<Func<UserProfile, T1>> includeT1, Expression<Func<UserProfile, T2>> includeT2);

        void UpdateProfile(UserProfile profileInfo);

        UsageSummary UsageSummary();


        DisplayPicture AddDisplayPicture(DisplayPicture dPInfo);

        Task<IList<UserProfile>> GetAllAsync();

        Task<IList<UserProfile>> GetAllAsync<T>(Expression<Func<UserProfile, T>> includeT) where T : class;

        Task<IList<UserProfile>> GetAllAsync<T, T2>(Expression<Func<UserProfile, T>> includeT, Expression<Func<UserProfile, T2>> includeT2) where T : class;

        Task<IList<UserProfile>> GetAllAsync<T, T2, T3>(Expression<Func<UserProfile, T>> includeT, Expression<Func<UserProfile, T2>> includeT2, Expression<Func<UserProfile, T3>> includeT3) where T : class;
    }
}
