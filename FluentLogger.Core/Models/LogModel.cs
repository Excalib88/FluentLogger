using Microsoft.Extensions.Logging;

namespace FluentLogger.Core.Models;

public class LogModel
{
    public string Message { get; set; } = null!;
    public string Body { get; set; } = null!;
    public LogLevel LogLevel { get; set; } = LogLevel.Debug;
}