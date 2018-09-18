using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Debugtime.Common.Identity
{
    public class AppGmailClient:SmtpClient
    {
        public string UserEmail { get; set; }
        public AppGmailClient(string from):base("smtp.gmail.com",587)
        {
            this.UserEmail = from;
            this.EnableSsl = true;
            this.UseDefaultCredentials = false;
            this.Credentials = new NetworkCredential(this.UserEmail, @"(""SecretKey"",""8129"");");
            this.DeliveryMethod = SmtpDeliveryMethod.Network;
        }
    }
}
