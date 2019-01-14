using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace  Merkato.Lib.Models.ServiceModel
{
    /// <summary>
    /// 
    /// </summary>
    public class EmailNotificationSender
    {
        /// <summary>
        /// 
        /// </summary>
        public TimerCallback TimerCallback { get; private set; }

        private readonly ILogger _logger;
        /// <summary>
        /// 
        /// </summary>
        public EmailNotificationSender()
        {
            //TimerCallback = new TimerCallback(SchedularCallback);
            //Schedular = new Timer(TimerCallback);
           
        }
        /// <summary>
        /// 
        /// </summary>
        public Timer Schedular { get; private set; }
        SmtpConfig _smtpConfig;
        //EmailNotificationScheduler _emailScheduler;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="receiver"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <param name="Context"></param>
        /// <param name="ishtmlBody"></param>
        public void SendEmail(string receiver, string subject, string message, MerkatoDbContext Context, bool ishtmlBody = false)
        {

            if (_smtpConfig == null)
                _smtpConfig = Parameters.Get<SmtpConfig>(SmtpConfig.KEY_NAME, Context);

            //_logger.LogInformation("The smtp Config is:", _smtpConfig);

            if (string.IsNullOrWhiteSpace(subject))
            {
                subject = "WorkFlow Notification";
            }
            if (string.IsNullOrWhiteSpace(message))
            {
                message = $"{subject} Log on to Workflow app to see the details.";
            }
            using (var smtpCleint = new SmtpClient(_smtpConfig.ServerAdress, _smtpConfig.ServerPort))
            {
                smtpCleint.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtpCleint.UseDefaultCredentials = true;
                smtpCleint.Credentials =
                    (System.Net.ICredentialsByHost)new System.Net.NetworkCredential(_smtpConfig.Credential.Username, _smtpConfig.Credential.Password);
                smtpCleint.EnableSsl = _smtpConfig.EnableSsl;
                var mailmessqge = new MailMessage(_smtpConfig.Credential.Username, receiver, subject, message);
                
                if (!string.IsNullOrWhiteSpace(_smtpConfig.CopyMail))
                {
                    var splits = _smtpConfig.CopyMail.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in splits)
                    {
                        MailAddress copy = new MailAddress(item);
                        mailmessqge.CC.Add(copy);
                    }
                }
                mailmessqge.IsBodyHtml = ishtmlBody;
                //_logger.LogInformation("mail message is:", mailmessqge);

                try
                {
                    smtpCleint.Send(mailmessqge);

                }
                catch (SmtpFailedRecipientException ex)
                {

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception caught in RetryIfBusy(): {0}",
                            ex.ToString());
                }

            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="receiver"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <param name="ishtmlBody"></param>
        public string SendEmailWithVerification(string receiver, string subject, string message, MerkatoDbContext Context, bool ishtmlBody = false)
        {
            string result = string.Empty;

            if (_smtpConfig == null)
                _smtpConfig = Parameters.Get<SmtpConfig>(SmtpConfig.KEY_NAME, Context);


            if (string.IsNullOrWhiteSpace(subject))
            {
                subject = "Merkato Notification";
            }
            if (string.IsNullOrWhiteSpace(message))
            {
                message = $"{subject} Log on to Merkato app form more details.";
            }
            using (var smtpCleint = new SmtpClient(_smtpConfig.ServerAdress, _smtpConfig.ServerPort))
            {
                smtpCleint.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtpCleint.UseDefaultCredentials = true;
                smtpCleint.Credentials =
                    (System.Net.ICredentialsByHost)new System.Net.NetworkCredential(_smtpConfig.Credential.Username, _smtpConfig.Credential.Password);
                smtpCleint.EnableSsl = _smtpConfig.EnableSsl;
                var mailmessqge = new MailMessage(_smtpConfig.Credential.Username, receiver, subject, message);

                if (!string.IsNullOrWhiteSpace(_smtpConfig.CopyMail))
                {
                    var splits = _smtpConfig.CopyMail.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in splits)
                    {
                        MailAddress copy = new MailAddress(item);
                        mailmessqge.CC.Add(copy);
                    }
                }
                mailmessqge.IsBodyHtml = ishtmlBody;
                //_logger.LogInformation("mail message is:", mailmessqge);

                try
                {
                    smtpCleint.Send(mailmessqge);

                    result = "sucess";
       
                }
                catch (SmtpFailedRecipientException ex)
                {
                    SmtpStatusCode status = ex.StatusCode;
                    if (status == SmtpStatusCode.MailboxBusy ||
                        status == SmtpStatusCode.MailboxUnavailable)
                    {
                        Console.WriteLine("Delivery failed - retrying in 5 seconds.");
                        System.Threading.Thread.Sleep(5000);
                        smtpCleint.Send(mailmessqge);
                        
                    }
                    else
                    {
                        Console.WriteLine("Failed to deliver message to {0}",
                            ex.FailedRecipient);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception caught in RetryIfBusy(): {0}",
                            ex.ToString());
                }

            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <param name="Context"></param>
        public void SendEmail(List<string> to, string subject,string message, MerkatoDbContext Context)
        {

            if (_smtpConfig == null)
                _smtpConfig = Parameters.Get<SmtpConfig>(SmtpConfig.KEY_NAME, Context);

            if (string.IsNullOrWhiteSpace(subject))
            {
                subject = "Merkato Notification";
            }
            if (string.IsNullOrWhiteSpace(message))
            {
                message = $"{subject} Login credentilas.";
            }
            using (var smtpCleint = new SmtpClient(_smtpConfig.ServerAdress, _smtpConfig.ServerPort))
            {
                smtpCleint.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtpCleint.UseDefaultCredentials = true;
                smtpCleint.Credentials =
                    (System.Net.ICredentialsByHost)new System.Net.NetworkCredential(_smtpConfig.Credential.Username, _smtpConfig.Credential.Password);
                smtpCleint.EnableSsl = _smtpConfig.EnableSsl;
                var mailmessage = new MailMessage();
                mailmessage.Body = message;
                mailmessage.Subject = subject;
                mailmessage.IsBodyHtml = false;
                mailmessage.From =  new MailAddress(_smtpConfig.Credential.Username);
                foreach (var item in to)
                {
                    mailmessage.To.Add(item);
                }
                smtpCleint.Send(mailmessage);
            }
        }

    }
}
