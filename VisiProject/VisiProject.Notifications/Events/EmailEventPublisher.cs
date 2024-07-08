using VisiProject.Notifications.Models;

namespace VisiProject.Notifications.Events;

public class EmailEventPublisher
{
    public event EventHandler<EmailEventArgs> EmailEvent;

    public void RaiseEmailEvent(IAttachments attachments, string template)
    {
        EmailEvent?.Invoke(this, new EmailEventArgs(attachments, template));
    }
}