using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Debugtime.Common.Dtos
{
    public class UserAvatarDto
    {
        public string ProfileId { get; set; }
        public string FileName { get; set; }

        public string ContentType { get; set; }
        public byte[] Content { get; set; }
    }
}
