using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Debugtime.Business.Utilities;

namespace Debugtime.Services.Controllers.Base
{
    public class BaseApiController:ApiController
    {
        private IUnitOfUtility _unitOfUtility;

        public IUnitOfUtility UnitOfUtility =>
            _unitOfUtility ?? (_unitOfUtility = new UnitOfUtility());
    }
}