using VisiProject.Notifications.Models;

namespace VisiProject.Notifications.Services;

public interface IEmailSenderService
{
    Task SendEmailAsync(IAttachments attachments, string emailTemplate);
}