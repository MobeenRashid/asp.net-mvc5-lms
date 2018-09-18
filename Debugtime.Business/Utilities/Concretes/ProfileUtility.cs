using Debugtime.Business.Utilities.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Debugtime.Common.Dtos;
using Debugtime.DataAccess.Core.IRepositories;
using Debugtime.DataAccess.Persistence.Repositories;
using System.IO;
using System.Web.UI.WebControls;
using Debugtime.Common.Configurations;
using DebugTime.Domain.Model;

namespace Debugtime.Business.Utilities.Concretes
{
    public class ProfileUtility : AutoMapperProfileConfiguration, IProfileUtility
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProfileUtility()
        {
            _unitOfWork = new UnitOfWork();
        }

        public async Task<UserAvatarDto> GetAvatarByIdAsync(string id)
        {

            if (String.IsNullOrEmpty(id))
                return null;

            var dp = await _unitOfWork.ProfilesRepository.GetDisplayPictureAsync(ur => ur.ProfileId == id);

            return EntityMapper.Map<DisplayPicture, UserAvatarDto>(dp);
        }

        public async Task<UserProfileInputDto> GetProfileToEditByIdAsync(string id)
        {
            if (String.IsNullOrEmpty(id))
                return null;

            var userProfile = await _unitOfWork.ProfilesRepository.GetProfileAsync(up => up.Id == id, up => up.Links);

            if (userProfile == null)
                return null;

            var profileDto = EntityMapper.Map<UserProfile, UserProfileInputDto>(userProfile);

            if(userProfile.Links!=null)
            profileDto = EntityMapper.Map<Links, UserProfileInputDto>(userProfile.Links, profileDto);

            return profileDto;
        }

        public async Task<bool> UploadAvatarInfoAsync(UserAvatarDto avatarDto)
        {
            if (avatarDto == null)
                return false;

            var dpInfo = EntityMapper.Map<UserAvatarDto, DisplayPicture>(avatarDto);

            _unitOfWork.ProfilesRepository.AddDisplayPicture(dpInfo);

            var result = await _unitOfWork.SaveWorkAsync();

            if (result > 0)
                return true;

            return false;
        }

        public async Task<IList<UserProfileListDto>> GetProfileListAsync()
        {
            try
            {
                var users = await _unitOfWork.ProfilesRepository.GetAllAsync(p=>p.UserAccount);

                var mm= users.Select(u => EntityMapper.Map<UserProfile, UserProfileListDto>(u)).ToList();

                for (var i = 0; i < mm.Count; i++)
                    mm[i].UserName = users[i].UserAccount.UserName;

                return mm;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> UpdateProfileAsync(UserProfileInputDto profileDto)
        {
            try
            {
                var userInDb =await 
                    _unitOfWork.UsersRepository.GetSingleAsync(u => u.Id == profileDto.UserAccountId,
                        u => u.UserProfile.Links);

                if (userInDb == null)
                    return false;

                UserProfile userProfile;
                if (userInDb.UserProfile == null)
                {
                    userProfile = EntityMapper.Map<UserProfileInputDto, UserProfile>(profileDto);
                    userProfile.Links = EntityMapper.Map<UserProfileInputDto, Links>(profileDto);
                }

                userProfile = EntityMapper.Map<UserProfileInputDto, UserProfile>(profileDto, userInDb.UserProfile);

                userProfile.Links = EntityMapper.Map<UserProfileInputDto, Links>(profileDto, userInDb.UserProfile?.Links);

                userInDb.UserProfile = userProfile;

                _unitOfWork.UsersRepository.UpdateUser(userInDb);
                var result = await _unitOfWork.SaveWorkAsync();

                if (result > 0)
                    return true;

                return false;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async Task<UserProfileViewDto> GetProfileToViewByUserNameAsync(string userName)
        {
            if (String.IsNullOrEmpty(userName))
                return null;

            var userProfile = await _unitOfWork.ProfilesRepository.GetProfileAsync(up => up.UserAccount.UserName == userName, up => up.Links);

            if (userProfile == null)
                return null;

            var profileDto = EntityMapper.Map<UserProfile, UserProfileViewDto>(userProfile);

            if(userProfile.Links!=null)
            profileDto = EntityMapper.Map<Links, UserProfileViewDto>(userProfile.Links, profileDto);

            return profileDto;
        }

        public async Task<string> GetFullNameByIdAsync(string id)
        {
            var profile = await _unitOfWork.ProfilesRepository.GetProfileAsync(pf => pf.Id == id);

            if (profile!=null)
                return profile.FullName;

            return null;
        }

        public async Task<string> GetFirstNameByIdAsync(string id)
        {
            var profile = await _unitOfWork.ProfilesRepository.GetProfileAsync(pf => pf.Id == id);

            if (profile != null)
                return profile.FirstName;

            return null;
        }

        //public async Task<UsageSymmaryDto> GetUsageSummaryAsync()
        //{
        
        //    var usageSummary = _unitOfWork.ProfilesRepository.UsageSummary();

        //    if (usageSummary == null)
        //        return null;
        //    UsageSymmaryDto summary = new UsageSymmaryDto();
        //    summary.TotalUsers = usageSummary.TotalUsers;
        //    summary.TotalCourses = usageSummary.TotalCourses;
        //    summary.UsedMediaSize = usageSummary.UsedMediaSize;
        //    summary.BuyCourses = usageSummary.BuyCourses;
        //    summary.AuthorNames = usageSummary.AuthorNames;
            
        //    //var profileDto = EntityMapper.Map<UsageSummary, UsageSymmaryDto>(usageSummary);

        //    return summary;
        //}
    }
}
