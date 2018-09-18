using Debugtime.Common.Dtos;
using DebugTime.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Debugtime.DataAccess.Core.IRepositories
{
    public interface IUsersRepository
    {
        List<string> AllNames { get; }

        Task<ApplicationUser> GetSingleAsync(Expression<Func<ApplicationUser, bool>> predicate);

        Task<ApplicationUser> GetSingleAsync<T>(Expression<Func<ApplicationUser, bool>> predicate, Expression<Func<ApplicationUser, T>> includeT);

        Task<ApplicationUser> GetSingleAsync<T1, T2>(Expression<Func<ApplicationUser, bool>> predicate, Expression<Func<ApplicationUser, T1>> includeT1, Expression<Func<ApplicationUser, T2>> includeT2);

        void UpdateUser(ApplicationUser user);

        Task<IEnumerable<ApplicationUser>> GetAllAsync();

        Task<IEnumerable<ApplicationUser>> GetAllAsync<T>(Expression<Func<ApplicationUser, T>> includeT) where T : class;

        Task<IEnumerable<ApplicationUser>> GetAllAsync<T, T2>(Expression<Func<ApplicationUser, T>> includeT, Expression<Func<ApplicationUser, T2>> includeT2) where T : class;

        Task<IEnumerable<ApplicationUser>> GetAllAsync<T, T2, T3>(Expression<Func<ApplicationUser, T>> includeT, Expression<Func<ApplicationUser, T2>> includeT2, Expression<Func<ApplicationUser, T3>> includeT3) where T : class;
        Task<bool> DeleteAsync(string userId);
        bool Delete(ApplicationUser user);
    }
}