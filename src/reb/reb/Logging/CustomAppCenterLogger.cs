using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Shiny.Logging;
using Xamarin.Essentials;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace reb.Logging;

public class CustomAppCenterLogger : ILogger
{
    private readonly string _categoryName;
    private readonly LogLevel _configLogLevel;
    private readonly string _filePath;
    private readonly string _fileName;

    public CustomAppCenterLogger(string categoryName, LogLevel configLogLevel, string attachmentFileName)
    {
        _categoryName = categoryName;
        _configLogLevel = configLogLevel;

        if (!string.IsNullOrEmpty(attachmentFileName))
        {
            _filePath = Path.Combine(FileSystem.AppDataDirectory, attachmentFileName);
            _fileName = attachmentFileName;
        }        

        Crashes.GetErrorAttachments = GetErrorAttachments;        
    }

    public IDisposable BeginScope<TState>(TState state) => NullScope.Instance;
    public bool IsEnabled(LogLevel logLevel) => logLevel >= _configLogLevel;
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        if (!IsEnabled(logLevel))
            return;

        var message = formatter(state, exception);
        if (logLevel >= LogLevel.Error)
        {
            exception ??= new Exception(message);

            Crashes.TrackError(
                exception,
                new Dictionary<string, string>
                {
                    { "Message", message }
                }
            );
        }
        else
        {
            Analytics.TrackEvent($"[{logLevel} - {_categoryName}] {message}");
        }
    }

    private IEnumerable<ErrorAttachmentLog> GetErrorAttachments(ErrorReport report)
    {
        var attachments = new List<ErrorAttachmentLog>();
        var attachment = GetErrorAttachment();
        if (attachment != null)
            attachments.Add(attachment);

        return attachments;
    }

    private ErrorAttachmentLog GetErrorAttachment()
    {
        ErrorAttachmentLog attachment = null;
        try
        {
            if (!string.IsNullOrEmpty(_filePath) && File.Exists(_filePath))
            {
                var logBytes = File.ReadAllBytes(_filePath);
                attachment = ErrorAttachmentLog.AttachmentWithBinary(logBytes, _fileName, "text/plain");
            }
        }
        catch (Exception ex)
        {
            attachment = ErrorAttachmentLog.AttachmentWithText($"Reading log file thrown error: {ex}", _fileName);
        }

        return attachment;
    }
}
