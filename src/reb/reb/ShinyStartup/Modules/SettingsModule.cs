using System.Reflection;

namespace reb.ShinyStartup.Modules;

internal class SettingsModule : ShinyModule
{
    public override void Register(IServiceCollection services)
    {
        var stream = Assembly.GetAssembly(typeof(App)).GetManifestResourceStream($"{typeof(App).Namespace}.Settings.AppSettings.SettingsFiles.appsettings.json");
        if (stream == null)
            throw new Exception("Configuration file 'appsettings.json' is missing");

        var configuration = new ConfigurationBuilder()
            .AddJsonStream(stream)
            .Build();

        services.Configure<GeneralLoggingSettings>(configuration.GetSection($"Logging:{nameof(GeneralLoggingSettings)}"), options => options.BindNonPublicProperties = true);
        services.Configure<FileLoggerSettings>(configuration.GetSection($"Logging:{nameof(FileLoggerSettings)}"), options => options.BindNonPublicProperties = true);
        services.Configure<AppCenterSettings>(configuration.GetSection($"Logging:{nameof(AppCenterSettings)}"), options => options.BindNonPublicProperties = true);
    }
}
