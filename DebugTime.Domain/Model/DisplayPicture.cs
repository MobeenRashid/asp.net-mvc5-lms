using System;
using System.ComponentModel.DataAnnotations;

namespace DebugTime.Domain.Model
{
    public class DisplayPicture
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string FileName { get; set; }

        [StringLength(100)]
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        public bool IsCurrent { get; set; } = true;

        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime? DateModified { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }

        //navigation properties//
        public string ProfileId { get; set; }
        public UserProfile Profile { get; set; }
    }
}