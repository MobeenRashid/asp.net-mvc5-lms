namespace Debugtime.Common.Dtos
{
    public class UserSignInDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public bool IsPersistence { get; set; }
        public bool RememberBrowser { get; set; }
    }
}