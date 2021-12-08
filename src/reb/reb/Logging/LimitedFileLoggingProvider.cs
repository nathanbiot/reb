
namespace reb.Logging;

public class LimitedFileLoggingProvider : ILoggerProvider
{
    private readonly LogLevel _logLevel;
    private readonly string _logFileName;
    private readonly int _logFileMaxLines;
    private bool _isEnabled;

    public void Dispose() { }

    public LimitedFileLoggingProvider(
       IOptions<GeneralLoggingSettings> generalSettings,
       IOptions<FileLoggerSettings> fileLoggerSettings
       )
    {
        _isEnabled = fileLoggerSettings?.Value?.IsEnabled ?? false;
        if (!_isEnabled)
            return;

        _logLevel = fileLoggerSettings.Value.LogLevel ?? generalSettings.Value.LogLevel;
        _logFileName = fileLoggerSettings.Value.LogFileName;
        _logFileMaxLines = fileLoggerSettings.Value.LogFileMaxLines;
    }

    public LimitedFileLoggingProvider(LogLevel logLevel, string logFileName, int logFileMaxLines)
    {        
        _logLevel = logLevel;
        _logFileName = logFileName;
        _logFileMaxLines = logFileMaxLines;
    }

    public ILogger CreateLogger(string categoryName) => _isEnabled ? new LimitedFileLogger(_logLevel, _logFileName, _logFileMaxLines) : new DisabledLogger();
}
