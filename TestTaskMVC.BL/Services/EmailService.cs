﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MimeKit;
using TestTaskMVC.BL.Interfaces;
using TestTaskMVC.DomainModels.Models;
using MailKit;
namespace TestTaskMVC.BL.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _emailConfiguration;
        private readonly UserManager<AppUser> _userManager;


        public EmailService(EmailConfiguration config, UserManager<AppUser> userManager)
        {
            _emailConfiguration = config;
            _userManager = userManager;
        }
        public async Task SendEmailAsync(string mail, string username, string html, string content)
        {
            
            
            AppUser appUser = await _userManager.FindByEmailAsync(mail);

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_emailConfiguration.Title, _emailConfiguration.From));
            message.To.Add(new MailboxAddress(appUser.Name, appUser.Email));
            message.Subject = _emailConfiguration.Subject;
            string emailBody = html;
            message.Body = new TextPart { Text = emailBody };
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.Port, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(_emailConfiguration.Username, _emailConfiguration.Password);
            smtp.Send(message);
            smtp.Disconnect(true);
        }
    }
}
