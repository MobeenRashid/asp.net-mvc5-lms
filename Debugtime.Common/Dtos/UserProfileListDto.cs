namespace Debugtime.Common.Dtos
{
    public class UserProfileListDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string Location { get; set; }
        public string JobTitle { get; set; }
        public string ShortBio { get; set; }
        public string Company { get; set; }
        public string UserName { get; set; }
    }
}