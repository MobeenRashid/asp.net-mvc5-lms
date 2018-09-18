using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Net.Mail;

namespace Debugtime.Common.Identity
{
    public class AppEmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            var myMail = new MailMessage("mobeenrashed032@gmail.com", message.Destination)
            {
                Subject = message.Subject,
                Body = message.Body,
                IsBodyHtml = true
            };

            using (var mailClient = new AppGmailClient("mobeenrashed032@gmail.com"))
            {
                myMail.From = new MailAddress(mailClient.UserEmail, "Debugtime");

                await mailClient.SendMailAsync(myMail);
            }
        }
    }
}