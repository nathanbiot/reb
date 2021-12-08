namespace reb.Settings.AppSettings;

public class AppCenterSettings
{
    public bool IsEnabled { get; set; }
    public LogLevel? LogLevel { get; private set; }
    public string Secret { get; private set; }
}
