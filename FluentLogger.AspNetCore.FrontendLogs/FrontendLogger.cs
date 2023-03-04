using FluentLogger.Core.Models;
using Microsoft.Extensions.Logging;

namespace FluentLogger.AspNetCore.FrontendLogs;

public class FrontendLogger : IFrontendLogger
{
    private readonly ILogger _logger;

    public FrontendLogger(ILogger<FrontendLogger> logger)
    {
        _logger = logger;
    }

    public void Log(LogModel logModel)
    {
        _logger.Log(logModel.LogLevel, logModel.Body);
    }
}