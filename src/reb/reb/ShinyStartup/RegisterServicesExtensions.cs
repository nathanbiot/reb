using reb.Logging;

namespace reb.ShinyStartup;

public static class RegisterServicesExtensions
{

    public static void RegisterLoggers(this ILoggingBuilder builder)
    {
        builder.ClearProviders();
#if DEBUG        
        builder.AddConsole();
#endif

        builder.AddProvider<LimitedFileLoggingProvider>();
        builder.AddProvider<CustomAppCenterLoggingProvider>();
    }

    public static ILoggingBuilder AddProvider<T>(this ILoggingBuilder builder)
            where T : class, ILoggerProvider
    {
        builder.Services.AddSingleton<ILoggerProvider, T>();
        return builder;
    }

    public static ILoggingBuilder AddProvider<T>(this ILoggingBuilder builder, Func<IServiceProvider, T> factory)
    where T : class, ILoggerProvider
    {
        builder.Services.AddSingleton<ILoggerProvider, T>(factory);
        return builder;
    }

}
