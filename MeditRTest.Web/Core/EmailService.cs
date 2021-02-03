using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using MeditRTest.Web.Core;
using SendGrid;
using SendGrid.Helpers.Mail;


namespace UpdatePortal.Service
{
    public class EmailService
    {
        private readonly EmailSettings _emailService;
        public EmailService(EmailSettings emailSettings)
        {
            _emailService = emailSettings;
        }


        public async Task SendEmail(string to, string message, string subject)
        {
            if (!_emailService.IsLocal)
            {
                var client = new SendGridClient(_emailService.ApiKey);
                var fromEmailAddress = new EmailAddress(_emailService.FromEmail, _emailService.FromName);
                var toEmailAddress = new EmailAddress(to);
                var msg = MailHelper.CreateSingleEmail(fromEmailAddress, toEmailAddress, subject, String.Empty, message);
                var response = await client.SendEmailAsync(msg);
            }
        }

        //public async Task SendEmail(string to, string message, string subject, string attachmentFile)
        //{
        //    if (!_emailService.IsLocal)
        //    {
        //        var apiKey = _emailService.Password;
        //        var client = new SendGridClient(apiKey);
        //        var fromEmailAddress = new EmailAddress(_emailService.FromEmail, _emailService.FromName);
        //        var toEmailAddress = new EmailAddress(to);
        //        var msg = MailHelper.CreateSingleEmail(fromEmailAddress, toEmailAddress, subject, String.Empty, message);
        //        var bytes = File.ReadAllBytes(attachmentFile);
        //        var file = Convert.ToBase64String(bytes);
        //        msg.AddAttachment("بطاقة الطلب.pdf", file);
        //        var response = await client.SendEmailAsync(msg);
        //    }
        //}
    }
}