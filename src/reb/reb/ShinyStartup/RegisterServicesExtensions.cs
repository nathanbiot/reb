namespace reb.ShinyStartup;

public static class RegisterServicesExtensions
{
    public static void RegisterSettings(this IServiceCollection services, IConfigurationRoot configuration)
    {
        services.Configure<GeneralLoggingSettings>(configuration.GetSection($"Logging:{nameof(GeneralLoggingSettings)}"), options => options.BindNonPublicProperties = true);
        services.Configure<FileLoggerSettings>(configuration.GetSection($"Logging:{nameof(FileLoggerSettings)}"), options => options.BindNonPublicProperties = true);
        services.Configure<AppCenterSettings>(configuration.GetSection($"Logging:{nameof(AppCenterSettings)}"), options => options.BindNonPublicProperties = true);
    }
}
