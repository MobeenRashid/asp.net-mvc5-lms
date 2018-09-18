namespace Debugtime.Common.Model.View
{
    public class UserProfileViewModel
    {
        public string Id { get; set; }

        //Personal Info
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

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