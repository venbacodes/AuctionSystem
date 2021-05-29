using Application.AppSettingsModels;
using Application.Common.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuctionSystem.Infrastructure
{
    public class EmailSender : IEmailSender
    {
        //static string smtpAddress = "smtp.gmail.com";
        //static int portNumber = 587;
        //static bool enableSSL = true;
        //static string emailFromAddress = "sender@gmail.com"; //Sender Email Address  
        //static string password = "Abc@123$%^"; //Sender Password  
        //static string emailToAddress = "receiver@gmail.com"; //Receiver Email Address  
        //static string subject = "Hello";
        //static string body = "Hello, This is Email sending test using gmail.";

        private readonly EmailSettings _options;

        public EmailSender(IOptions<EmailSettings> options)
        {
            _options = options.Value;
        }

        public async Task<bool> SendEmailAsync(string receiver, string subject, string htmlMessage)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(_options.Email);
                    mail.To.Add(receiver);
                    mail.Subject = subject;
                    mail.Body = htmlMessage;
                    mail.IsBodyHtml = true;
                    using (SmtpClient smtp = new SmtpClient(_options.SMTP, _options.Port))
                    {
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential(_options.Email, _options.Password);
                        smtp.EnableSsl = _options.EnableSSL;
                        await smtp.SendMailAsync(mail);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                foreach (var p in _options.GetType().GetProperties())
                {
                    System.Console.WriteLine(p.Name + " : " + p.GetValue(_options));
                }
                Console.WriteLine(ex.Message);
                return false;
            }
        }

    }
}