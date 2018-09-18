using System.Collections.Generic;
using Debugtime.Common.Persistence;
using Debugtime.DataAccess.Core.IRepositories;
using System.Linq;
using DebugTime.Domain.Model;
using System;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Debugtime.DataAccess.Persistence.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public UsersRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public List<string> AllNames
        {
            
            get
            {
                if (!_dbContext.Users.Any())
                    return null;
                return _dbContext.Users.Select(ur => ur.UserName).ToList();
            }
        }

        public async Task<ApplicationUser> GetSingleAsync(Expression<Func<ApplicationUser, bool>> predicate)
        {
            return await _dbContext.Users.SingleAsync(predicate);
        }

        public async Task<ApplicationUser> GetSingleAsync<T>(Expression<Func<ApplicationUser, bool>> predicate, Expression<Func<ApplicationUser, T>> includeT)
        {
            return await _dbContext.Users.Include(includeT).SingleAsync(predicate);
        }

        public async Task<ApplicationUser> GetSingleAsync<T1, T2>(Expression<Func<ApplicationUser, bool>> predicate, Expression<Func<ApplicationUser, T1>> includeT1, Expression<Func<ApplicationUser, T2>> includeT2)
        {
            return await _dbContext.Users.Include(includeT1).Include(includeT2).SingleAsync(predicate);
        }

        public void UpdateUser(ApplicationUser user)
        {
            _dbContext.Entry(user).State = EntityState.Modified;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllAsync()
        {
            return await _dbContext.Users.Where(u => u.IsDeleted == false).ToListAsync();
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllAsync<T>(Expression<Func<ApplicationUser, T>> includeT) where T : class
        {
            return await _dbContext.Users.Include(includeT).Where(u => u.IsDeleted == false).ToListAsync();
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllAsync<T, T2>(Expression<Func<ApplicationUser, T>> includeT, Expression<Func<ApplicationUser, T2>> includeT2) where T : class
        {
            return await _dbContext.Users.Include(includeT).Include(includeT2).Where(u => u.IsDeleted == false).ToListAsync();
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllAsync<T, T2, T3>(Expression<Func<ApplicationUser, T>> includeT, Expression<Func<ApplicationUser, T2>> includeT2, Expression<Func<ApplicationUser, T3>> includeT3) where T : class
        {
            return await _dbContext.Users.Include(includeT).Include(includeT2).Include(includeT3).Where(u=>u.IsDeleted==false).ToListAsync();
        }

        public async Task<bool> DeleteAsync(string userId)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return false;
            user.IsDeleted = true;
            _dbContext.Entry(user).State = EntityState.Modified;

            return true;
        }

        public bool Delete(ApplicationUser user)
        {
            if (user == null)
                return false;

            user.IsDeleted = true;
            _dbContext.Entry(user).State = EntityState.Modified;

            return true;
        }
    }
}