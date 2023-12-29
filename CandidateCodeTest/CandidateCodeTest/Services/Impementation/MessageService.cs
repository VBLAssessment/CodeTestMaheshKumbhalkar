using CandidateCodeTest.Common.Interfaces;
using System;
using System.Net;
using System.Net.Mail;

namespace CandidateCodeTest.Services.Implementations
{
    public class MessageService : IMessageService
    {
        public ILogEntry _AddLogsr;
        public MessageService(ILogEntry AddLogsr)
        {
            _AddLogsr = AddLogsr;
        }
        public void SendEmail()
        {
            int retry = 3;
            bool failed;
            do
            {
                try
                {
                    failed = false;
                    MailMessage myMail = SendMessage();
                    ConfigureSMTPMail(myMail);
                    break;
                }
                catch (SmtpException ex)
                {
                    _AddLogsr.AddLogs($"SendEmail() Failed, Retry Number: {retry}, SmtpException: {ex.Message}");
                    failed = true;
                    retry--;
                }
                catch (Exception ex)
                {
                    _AddLogsr.AddLogs($"SendEmail() Failed, Retry Number: {retry}, Exception Message: {ex.Message}");
                    failed = true;
                    retry--;
                }
            }
            while (failed && retry != 0);
        }


        /// <summary>
        /// Function used to configure the SMTP for messaging mail.
        /// </summary>
        /// <param name="myMail"></param>
        private static void ConfigureSMTPMail(MailMessage myMail)
        {
            try
            {
                using SmtpClient mySmtpClient = new("smtp.gmail.com", 465);
                mySmtpClient.UseDefaultCredentials = false;
                mySmtpClient.Credentials = new NetworkCredential("emailassignmenttest@gmail.com", "Email@123");
                mySmtpClient.EnableSsl = true;
                mySmtpClient.Send(myMail);
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Function used to compose the message.
        /// </summary>
        /// <returns></returns>
        private static MailMessage SendMessage()
        {
            try
            {
                MailAddress from = new("emailassignmenttest@gmail.com", "Do Not Reply - Test Mail");
                MailAddress to = new("manishkumbhalkar@gmail.com", "Mahesh");
                MailMessage myMail = new(from, to)
                {
                    // set subject and encoding
                    Subject = "Test message",
                    SubjectEncoding = System.Text.Encoding.UTF8,
                    // set body-message and encoding
                    Body = "<b>Test Mail</b><br>using <b>HTML</b>.",
                    BodyEncoding = System.Text.Encoding.UTF8,
                    // text or html
                    IsBodyHtml = true
                };
                return myMail;
            }
            catch (Exception)
            {

                throw;
            }


        }
    }
}
