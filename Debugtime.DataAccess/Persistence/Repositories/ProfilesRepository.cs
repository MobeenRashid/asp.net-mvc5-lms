using Debugtime.DataAccess.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using Debugtime.Common.Dtos;
using Debugtime.Common.Persistence;
using DebugTime.Domain.Model;
using Debugtime.Common.Configurations;
using System.Linq.Expressions;
using System.Web;
using System.IO;

namespace Debugtime.DataAccess.Persistence.Repositories
{
    public class ProfilesRepository : IProfilesRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ProfilesRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public async Task<DisplayPicture> GetDisplayPictureAsync(Expression<Func<DisplayPicture, bool>> predicate)
        {
            return await _dbContext.DisplayPictures.FirstOrDefaultAsync(predicate);

        }

        public async Task<UserProfile> GetProfileAsync(Expression<Func<UserProfile, bool>> predicate)
        {
            return await _dbContext.UserProfiles.FirstOrDefaultAsync(predicate);
        }

        public async Task<UserProfile> GetProfileAsync<T>(Expression<Func<UserProfile, bool>> predicate, Expression<Func<UserProfile, T>> includeT)
        {
            return await _dbContext.UserProfiles.Include(includeT).FirstOrDefaultAsync(predicate);

        }

        public async Task<UserProfile> GetProfileAsync<T1, T2>(Expression<Func<UserProfile, bool>> predicate, Expression<Func<UserProfile, T1>> includeT1, Expression<Func<UserProfile, T2>> includeT2)
        {
            return await _dbContext.UserProfiles.Include(includeT1).Include(includeT2).FirstOrDefaultAsync(predicate);

        }

        public void UpdateProfile(UserProfile profileInfo)
        {
            _dbContext.Entry(profileInfo).State = EntityState.Modified;
        }

        public DisplayPicture AddDisplayPicture(DisplayPicture dPInfo)
        {
            MakeAllDpsInActive();
            return _dbContext.DisplayPictures.Add(dPInfo);
        }

        public async Task<IList<UserProfile>> GetAllAsync()
        {
            return await _dbContext.UserProfiles.Where(u=>u.UserAccount.IsDeleted==false).ToListAsync();
        }

        public async Task<IList<UserProfile>> GetAllAsync<T>(Expression<Func<UserProfile, T>> includeT) where T : class
        {
            return await _dbContext.UserProfiles.Include(includeT).Where(u => u.UserAccount.IsDeleted == false).ToListAsync();
        }

        public async Task<IList<UserProfile>> GetAllAsync<T, T2>(Expression<Func<UserProfile, T>> includeT, Expression<Func<UserProfile, T2>> includeT2) where T : class
        {
            return await _dbContext.UserProfiles.Include(includeT).Include(includeT2).Where(u => u.UserAccount.IsDeleted == false).ToListAsync();
        }

        public async Task<IList<UserProfile>> GetAllAsync<T, T2, T3>(Expression<Func<UserProfile, T>> includeT, Expression<Func<UserProfile, T2>> includeT2, Expression<Func<UserProfile, T3>> includeT3) where T : class
        {
            return await _dbContext.UserProfiles.Include(includeT).Include(includeT2).Include(includeT3).Where(u => u.UserAccount.IsDeleted == false).ToListAsync();
        }

        private void MakeAllDpsInActive()
        {
            _dbContext.DisplayPictures.ToList().ForEach(dp => dp.IsCurrent = false);
        }
        static public long GetDirectorySize(string p)
        {
            // 1.
            // Get array of all file names.
            string[] a = Directory.GetFiles(p, "*.*");

            // 2.
            // Calculate total bytes of all files in a loop.
            long b = 0;
            foreach (string name in a)
            {
                // 3.
                // Use FileInfo to get length of each file.
                FileInfo info = new FileInfo(name);
                b += info.Length;
            }
            // 4.
            // Return total size
            return b;
        }
        public UsageSummary UsageSummary()
        {
            UsageSummary summary = new UsageSummary();
            string basePath = HttpContext.Current.Server.MapPath("~/Static/Courses/Unique");
            long currentTotalSizeInMegaBytes = Convert.ToInt64(Math.Round(GetDirectorySize(basePath) / (1024m * 1024m)));
            summary.UsedMediaSize=currentTotalSizeInMegaBytes.ToString();
            summary.TotalCourses=_dbContext.Courses.Count();
            summary.TotalUsers = _dbContext.Users.Count();
            summary.BuyCourses = _dbContext.Orders.Count();
            summary.AuthorNames = new List<string>();
            foreach (var item in _dbContext.Users)
            {
                summary.AuthorNames.Add(item.UserName);
            }
            return summary;
        }
    }
}
