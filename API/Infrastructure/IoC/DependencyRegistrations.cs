using System.Reflection;

namespace API.Infrastructure.IoC;

public static class DependencyRegistrations
{
    private static readonly Assembly[] _autoResolvedAssemblies = new []
    {
        Assembly.Load("API"),
        Assembly.Load("Application"),
        Assembly.Load("Data"),
    };
    
    public static IServiceCollection AddDependencyRegistrations(this IServiceCollection services)
    {
        services.Scan(scan =>
            scan
                .FromCallingAssembly()
                .FromAssemblies(_autoResolvedAssemblies)
                .AddClasses()
                .AsImplementedInterfaces()
                .WithTransientLifetime()
        );
        return services;
    }

    public static T Resolve<T>()
    {
        return Resolve<T>(new List<Action<IServiceCollection>>());
    }

    public static T Resolve<T>(List<Action<IServiceCollection>> registerResolverOverrides)
    {
        return (T) serviceProvider(registerResolverOverrides).GetService(typeof(T));
    }

    private static IServiceProvider serviceProvider(List<Action<IServiceCollection>> registerResolverOverrides)
    {
        var services = new ServiceCollection();
        services.AddDependencyRegistrations();
        registerResolverOverrides.ForEach(register => register(services));
        return services.BuildServiceProvider();
    }
}