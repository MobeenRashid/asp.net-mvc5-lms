using System;
using System.Linq;
using System.Web.Http.ModelBinding;
using Microsoft.AspNet.Identity;

namespace Debugtime.Common.Wrappers
{
    public class AppIdentityResultWrapper
    {
        public string UserName { get; set; }
        public string UserId { get; set; }
        public bool Succeeded { get; set; }

        public ModelStateDictionary ModelErrors { get; set; }
    }
}
