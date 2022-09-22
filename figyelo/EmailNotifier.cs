using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace AdvertisementObserver
{
    class EmailNotifier : INotificationSender
    {
        string emailAddressFrom;
        string emailAddressTo;

        SmtpClient emailClient;


        public EmailNotifier(string emailAddress, string password, string emailAddressTo, string emailClientAddress)
        {
            this.emailAddressFrom = emailAddress;
            this.emailAddressTo = emailAddress;

            emailClient = new SmtpClient(emailClientAddress)
            {
                Port = 587,
                Credentials = new NetworkCredential(emailAddressFrom, password),
                EnableSsl = true
            };

        }


        public void SendNotification(string notificationMessage, string notificationSubject = "")
        {
            SendMail(notificationMessage, notificationSubject);
        }


        private void SendMail(string message, string subject)
        {
            MailMessage mail = new MailMessage(emailAddressFrom, emailAddressTo)
            {
                Subject = subject,
                Body = message
            };

            emailClient.Send(mail);
        }

    }
}
