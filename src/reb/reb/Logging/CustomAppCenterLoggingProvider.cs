using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace reb.Logging;

public class CustomAppCenterLoggingProvider : ILoggerProvider
{
    private readonly bool _isEnabled;
    private readonly LogLevel _logLevel;
    private readonly string _attachmentFileName;

    public CustomAppCenterLoggingProvider(
        IOptions<GeneralLoggingSettings> generalSettings,
        IOptions<AppCenterSettings> appCenterSettings,
        IOptions<FileLoggerSettings> fileLoggerSettings
        )
    {
        _isEnabled = appCenterSettings?.Value?.IsEnabled ?? false;
        if (!_isEnabled)
            return;

        _logLevel = appCenterSettings.Value.LogLevel ?? generalSettings.Value.LogLevel;
        _attachmentFileName = fileLoggerSettings.Value.LogFileName;
        if(!string.IsNullOrWhiteSpace(appCenterSettings?.Value?.Secret))
        {
            AppCenter.Start(appCenterSettings.Value.Secret, typeof(Crashes), typeof(Analytics));
        }
    }

    public ILogger CreateLogger(string categoryName) => _isEnabled ? new CustomAppCenterLogger(categoryName, _logLevel, _attachmentFileName) : new DisabledLogger();
    public void Dispose() { }
}
