using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DebugTime.Domain.Model
{
    public class UserProfile
    {
        public UserProfile()
        {
            Certifications = new HashSet<Certification>();
        }

        [Key, ForeignKey("UserAccount")]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string Location { get; set; }
        public string JobTitle { get; set; }
        public string ShortBio { get; set; }
        public string Company { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime? DateModified { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }


        //naviagtional properties//
        public Links Links { get; set; }
        public ApplicationUser UserAccount { get; set; }
        public ICollection<DisplayPicture> DisplayPictures { get; set; }
        public ICollection<Certification> Certifications { get; set; }
    }
}