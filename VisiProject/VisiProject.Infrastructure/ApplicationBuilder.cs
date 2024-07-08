using Microsoft.Extensions.DependencyInjection;

namespace VisiProject.Infrastructure;

public class ApplicationBuilder
{
    public IServiceCollection Services { get; }

    public ApplicationBuilder(IServiceCollection services)
    {
        Services = services;
    }
}