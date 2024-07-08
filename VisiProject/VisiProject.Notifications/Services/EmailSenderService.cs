﻿using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Mail;
using VisiProject.Notifications.Models;

namespace VisiProject.Notifications.Services;

public class EmailSenderService : IEmailSenderService
{
    private readonly ILogger<EmailSenderService> _logger;

    private const string SenderEmail = "";
    private const string Password = "";
    private const string Host = "smtp.gmail.com";
    private const string TemplateEmailPath = "..\\VisiProject.Notifications\\Templates\\";
    private const int Port = 587;

    public EmailSenderService(ILogger<EmailSenderService> logger)
    {
        _logger = logger;
    }

    public async Task SendEmailAsync(IAttachments attachments, string emailTemplate)
    {
        _logger.LogInformation("Sending an email to {RecipientEmail} with a subject of {Subject}.", attachments.Email, attachments.Subject);

        string htmlBody;
        using StreamReader reader = new StreamReader(TemplateEmailPath + emailTemplate);
        htmlBody = await reader.ReadToEndAsync();
        htmlBody = htmlBody.Replace("{FirstName}", attachments.FirstName);
        htmlBody = htmlBody.Replace("{LastName}", attachments.LastName);
        htmlBody = htmlBody.Replace("{CurrentYear}", DateTime.Now.Year.ToString());
        htmlBody = htmlBody.Replace("{Email}", attachments.Email);
        if (attachments.RedirectUrl != null)
        {
            htmlBody = htmlBody.Replace("{RedirectUrl}", attachments.RedirectUrl);
        }

        var message = new MailMessage()
        {
            From = new MailAddress(SenderEmail),
            Subject = attachments.Subject,
            IsBodyHtml = true,
            Body = htmlBody
        };

        message.To.Add(new MailAddress(attachments.Email));

        var smtp = new SmtpClient(Host)
        {
            Port = Port,
            Credentials = new NetworkCredential(SenderEmail, Password),
            EnableSsl = true
        };
        
        await smtp.SendMailAsync(message);
        
        _logger.LogInformation("Email was sent successfully");
    }
}