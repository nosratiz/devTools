using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using DevTools.Application.Common.Interfaces;
using DevTools.Common.Options;
using Microsoft.Extensions.Options;
using Serilog;

namespace DevTools.Application.Common.Service
{
    public class EmailServices : IEmailServices
    {
        private readonly EmailSetting _emailSetting;

        public EmailServices(IOptionsSnapshot<EmailSetting> emailSetting)
        {
            _emailSetting = new EmailSetting
            {
                Host = emailSetting.Value.Host,
                UserName = emailSetting.Value.UserName,
                Password = emailSetting.Value.Password,
                Port = emailSetting.Value.Port,
                From = emailSetting.Value.From
            };
        }

        public async Task SendMessage(string emailTo, string subject, string body)
        {
            using MailMessage message = new MailMessage(_emailSetting.From, emailTo, subject, body)
            {
                IsBodyHtml = true,
                BodyEncoding = global::System.Text.Encoding.UTF8,
                SubjectEncoding = global::System.Text.Encoding.Default
            };

            var credential = new NetworkCredential(_emailSetting.UserName, _emailSetting.Password);


            var smtp = new SmtpClient(_emailSetting.Host, _emailSetting.Port)
            {
                Credentials = credential,
                EnableSsl = true
            };

            try
            {
                await smtp.SendMailAsync(message).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Log.Information(ex.StackTrace);
                //
            }
        }
    }
}