using Microsoft.Extensions.DependencyInjection;
using Validation;
using VisiProject.Notifications.Events;
using VisiProject.Notifications.Handlers;
using VisiProject.Notifications.Services;

namespace VisiProject.Notifications;

public static class ApplicationBuilderExtension
{
    public static IServiceCollection NotificationConfigurations(
        this IServiceCollection services)
    {
        Requires.NotNull(services, nameof(services));

        services.AddTransient<IEmailSenderService, EmailSenderService>();
        services.AddTransient<EmailEventPublisher>();
        services.AddTransient<EmailHandler>();

        return services;
    }
}