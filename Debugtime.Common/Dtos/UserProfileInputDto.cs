using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debugtime.Common.Dtos
{
    public class UserProfileInputDto
    {
        public string UserAccountId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }
        public string Company { get; set; }
        public string Location { get; set; }
        public string ShortBio { get; set; }

        public string Website { get; set; }
        public string Facebook { get; set; }
        public string LinkedIn { get; set; }
        public string GooglePlus { get; set; }
    }
}
