using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace  Merkato.Lib.Models
{
    public class Util
    {
        public IEnumerable<DateTime> EachDay(DateTime from, DateTime to)
        {
            for (var day = from.Date; day.Date <= to.Date; day = day.AddDays(1))
                yield return day;
        }

        public static async Task SendEmailWithAttachment(string receiver, string subject, string message, List<string> fileName, string copyMail)
        {
            var smtpServerAdress = "";
            var smtpPort = "";
            var smtpPassword = "";
            var smtpUsername = "";
            var EnableSsl = "";

            if (string.IsNullOrWhiteSpace(subject))
            {
                subject = "Mekato Team";
            }
            if (string.IsNullOrWhiteSpace(message))
            {
                message = $"{subject} Mekato Team";
            }
            await Task.Run(() =>
            {
                using (var smtpCleint = new SmtpClient(smtpServerAdress, int.Parse(smtpPort)))
                {
                    smtpCleint.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    smtpCleint.UseDefaultCredentials = true;
                    smtpCleint.Credentials =
                        (System.Net.ICredentialsByHost)new System.Net.NetworkCredential(smtpUsername, smtpPassword);
                    smtpCleint.EnableSsl = Boolean.Parse(EnableSsl);
                    var mailmessqge = new MailMessage(smtpUsername, receiver, subject, message);


                    foreach (var item in fileName)
                    {
                        Attachment at = new Attachment(item);
                        mailmessqge.Attachments.Add(at);
                    }



                    if (copyMail != null)
                    {
                        MailAddress copy = new MailAddress(copyMail);
                        mailmessqge.CC.Add(copy);
                    }

                    smtpCleint.Send(mailmessqge);
                }
            });

        }

        public static async Task SendEmail(string receiver, string subject, string message, string copyMail)
        {
            var smtpServerAdress = "";
            var smtpPort = "";
            var smtpPassword = "";
            var smtpUsername = "";
            var EnableSsl = "";

            if (string.IsNullOrWhiteSpace(subject))
            {
                subject = "Mekato Team";
            }
            if (string.IsNullOrWhiteSpace(message))
            {
                message = $"{subject} Mekato Team";
            }
            await Task.Run(() =>
            {
                using (var smtpCleint = new SmtpClient(smtpServerAdress, int.Parse(smtpPort)))
                {
                    smtpCleint.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    smtpCleint.UseDefaultCredentials = true;
                    smtpCleint.Credentials =
                        (System.Net.ICredentialsByHost)new System.Net.NetworkCredential(smtpUsername, smtpPassword);
                    smtpCleint.EnableSsl = Boolean.Parse(EnableSsl);
                    var mailmessqge = new MailMessage(smtpUsername, receiver, subject, message);

                    if (copyMail != null)
                    {
                        MailAddress copy = new MailAddress(copyMail);
                        mailmessqge.CC.Add(copy);
                    }

                    smtpCleint.Send(mailmessqge);
                }
            });

        }
    }


}
