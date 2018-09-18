using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debugtime.Common.Dtos
{
    public class ExternalLoginDto
    {
        public string UserId { get; set; }
        public UserLoginInfo LoginInfo { get; set; }
    }
}
