namespace reb.Settings.AppSettings;

public class FileLoggerSettings
{
    public bool IsEnabled { get; set; }
    public LogLevel? LogLevel { get; private set; }
    public string LogFileName { get; private set; }
    public int LogFileMaxLines { get; private set; }
}
