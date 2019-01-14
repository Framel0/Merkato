using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace Merkato.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    /// <summary>
    /// 
    /// </summary>
    public class EmailSender : IEmailSender
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task SendEmailAsync(string email, string subject, string message)
        {
            if (string.IsNullOrWhiteSpace(subject))
            {
                subject = "Merkato Notification";
            }
            if (string.IsNullOrWhiteSpace(message))
            {
                message = $"{subject} Log on to Merkato app to see the details.";
            }
            using (var smtpCleint = new SmtpClient("smtp.gmail.com", 587))
            {
                smtpCleint.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtpCleint.UseDefaultCredentials = true;
                smtpCleint.Credentials =
                    (System.Net.ICredentialsByHost)new System.Net.NetworkCredential("sipmerkato@gmail.com", "SIPConsult@18");
                smtpCleint.EnableSsl = true;
                var mailmessqge = new MailMessage("sipmerkato@gmail.com", email, subject, message);

                MailAddress copy = new MailAddress("sipconsult18@gmail.com");
                mailmessqge.CC.Add(copy);
                //mailmessqge.IsBodyHtml = ishtmlBody;

                smtpCleint.Send(mailmessqge);
            }
                return Task.CompletedTask;
        }
    }
}
