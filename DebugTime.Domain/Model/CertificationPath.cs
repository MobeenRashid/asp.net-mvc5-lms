using System;
using System.Collections.Generic;

namespace DebugTime.Domain.Model
{
    public class CertificationPath
    {
        public CertificationPath()
        {
            Cources = new HashSet<Course>();
        }

        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string CertName { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime? DateModified { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }

        public ICollection<Course> Cources { get; set; }
    }
}
