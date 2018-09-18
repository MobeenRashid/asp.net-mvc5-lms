using System;

namespace DebugTime.Domain.Model
{
    public class Certification
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public string Company { get; set; }
        public DateTime DateIssued { get; set; } = DateTime.Now;
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateModified { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }

        public string ProfileId { get; set; }

        //navigational properties
        public UserProfile UserProfile { get; set; }
    }
}
