using Microsoft.AspNet.Identity.Owin;

namespace Debugtime.Common.Dtos
{
    public class ExternalSignInDto
    {
        public ExternalLoginInfo LoginInfo { get; set; }
        public bool IsPersistence { get; set; }
    }
}
