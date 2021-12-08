using Shiny.Logging;
using Xamarin.Essentials;

namespace reb.Logging;

public class LimitedFileLogger : ILogger
{
    private readonly object _syncLock = new object();
    private readonly string _filePath;
    private readonly int _maxLines;
    private readonly LogLevel _logLevel;    

    public LimitedFileLogger(LogLevel logLevel, string logFileName, int logFileMaxLines)
    {
        _filePath = Path.Combine(FileSystem.AppDataDirectory, logFileName);
        _maxLines = logFileMaxLines;
        _logLevel = logLevel;
    }

    private void Write(string message)
    {
        lock (_syncLock)
        {
            if (!File.Exists(_filePath))
            {
                File.AppendAllText(_filePath, message);
                return;
            }

            var logEntries = File.ReadAllLines(_filePath).ToList();

            logEntries.Add(message);

            while (logEntries.Count > _maxLines)
                logEntries.RemoveAt(0);

            File.WriteAllLines(_filePath, logEntries);
        }
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (!this.IsEnabled(logLevel))
            return;

        if (formatter == null)
            throw new ArgumentNullException(nameof(formatter));

        var message = formatter(state, exception);
        if (string.IsNullOrEmpty(message))
            return;

        message = $"{logLevel}: {message}";
        if (exception != null)
            message += $"{Environment.NewLine}{Environment.NewLine} {exception}";

        Write(message);
    }

    public IDisposable BeginScope<TState>(TState state) => NullScope.Instance;
    public bool IsEnabled(LogLevel logLevel) => logLevel >= _logLevel;
}
