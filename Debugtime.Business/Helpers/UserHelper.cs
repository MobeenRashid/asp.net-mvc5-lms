using System.Collections.Generic;
using System.Text.RegularExpressions;
using Debugtime.Common.Dtos;
using Debugtime.DataAccess.Core.IRepositories;

namespace Debugtime.Business.Helpers
{
    public class UserHelper
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserHelper(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<string> AppUserNames => _unitOfWork.UsersRepository.AllNames;

        public UserRegisterDto FixUser(UserRegisterDto newUser)
        {
            newUser.FirstName = Trim(newUser.FirstName);
            newUser.LastName = Trim(newUser.LastName);
            newUser.UserName = GetUniqueName(newUser.FullName);
            newUser.Email = Trim(newUser.Email);
            return newUser;
        }

        public string GetUniqueName(string userName, int userNumber = 1)
        {
            if (userNumber == 1)
            {
                userName = userName.ToLower().Replace(" ", ".");
                if (AppUserNames == null || AppUserNames.Count == 0)
                {
                    return userName;
                }
            }

            foreach (var name in AppUserNames)
            {
                if (userName.Equals(name))
                {
                    var pureName = PurifyUserName(userName);
                    userName = GetUniqueName($"{pureName}.{userNumber}", ++userNumber);
                }
            }

            return userName;
        }
        private string PurifyUserName(string userName)
        {
            var regexRep = new Regex(@"\b\.\d\b");
            userName = regexRep.Replace(userName, "");
            return Trim(userName);
        }

        public string Trim(string name)
        {
            return name.Trim();
        }
    }
}
