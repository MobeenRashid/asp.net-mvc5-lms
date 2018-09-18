using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Debugtime.Business.Utilities.Concretes;
using Debugtime.Business.Utilities.Contracts;

namespace Debugtime.Business.Utilities
{


    public class UnitOfUtility : IUnitOfUtility
    {
        private IUserUtility _userUtility;
        private IProfileUtility _profileUtility;
        private ICourseUtility _courseUtility;
        private IOAuthUtility _oAuthUtility;

        public IUserUtility UserUtility =>
            _userUtility ?? (_userUtility = new UserUtility());

        public IProfileUtility ProfileUtility =>
            _profileUtility ?? (_profileUtility = new ProfileUtility());

        public ICourseUtility CourseUtility =>
            _courseUtility ?? (_courseUtility = new CourseUtility());

        public IOAuthUtility OAuthUtility =>
            _oAuthUtility ?? (_oAuthUtility = new OAuthUtility());
    }
}
