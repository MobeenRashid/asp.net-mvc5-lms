using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debugtime.Common.Dtos
{
    public class EmailDto
    {
        public string UserId { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
    }
}
