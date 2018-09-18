namespace Debugtime.Common.Model.Input
{
    public class UploadAvatarInputModel
    {
        public string ProfileId { get; set; }
        public string FileName { get; set; }

        public string ContentType { get; set; }
        public byte[] Content { get; set; }
    }
}