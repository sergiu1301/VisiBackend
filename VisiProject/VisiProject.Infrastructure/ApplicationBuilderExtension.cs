using Microsoft.Extensions.DependencyInjection;
using Validation;
using VisiProject.Contracts.Services;
using VisiProject.Contracts.Stores;
using VisiProject.Contracts.Transactions;
using VisiProject.Contracts.Validators;
using VisiProject.Infrastructure.Options;
using VisiProject.Infrastructure.Services;
using VisiProject.Infrastructure.Stores;
using VisiProject.Infrastructure.Transactions;
using VisiProject.Infrastructure.Validators;

namespace VisiProject.Infrastructure;

public static class ApplicationBuilderExtension
{
    public static IServiceCollection InfrastructureConfigurations(
        this IServiceCollection services,
        string defaultConnection)
    {
        Requires.NotNull(services, nameof(services));
        Requires.NotNull(defaultConnection, nameof(defaultConnection));

        SqlServerOptions sqlServerOptions = new()
        {
            DefaultConnection = defaultConnection
        };
        services.AddSingleton(sqlServerOptions);
        services.AddSingleton<IAtomicScopeFactory, AtomicScopeFactory>();

        services.AddTransient<IRoleValidator, RoleValidator>();
        services.AddTransient<IContextService, ContextService>();
        services.AddTransient<IUserStore, UserStore>();
        services.AddTransient<IRoleStore, RoleStore>();
        services.AddTransient<IUserRoleStore, UserRoleStore>();
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IRoleService, RoleService>();
        services.AddTransient<IUserRoleService, UserRoleService>();
        services.AddTransient<IConversationStore, ConversationStore>();
        services.AddTransient<IConversationService, ConversationService>();
        services.AddTransient<IMessageStore, MessageStore>();
        services.AddTransient<IMessageService, MessageService>();
        services.AddSingleton<IEncryptDecryptService>(new EncryptDecryptService(""));

        return services;
    }
}