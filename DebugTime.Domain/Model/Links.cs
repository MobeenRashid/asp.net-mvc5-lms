using System;

namespace DebugTime.Domain.Model
{
    public class Links
    {
        public string UserProfileId { get; set; }
        public string Website { get; set; } 
        public string Facebook { get; set; } 
        public string LinkedIn { get; set; } 
        public string GooglePlus { get; set; } 
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime? DateModified { get; set; } = DateTime.Now;


        public UserProfile UserProfile { get; set; }
    }
}