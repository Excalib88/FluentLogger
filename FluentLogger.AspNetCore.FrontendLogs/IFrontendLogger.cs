using FluentLogger.Core.Models;

namespace FluentLogger.AspNetCore.FrontendLogs;

public interface IFrontendLogger
{
    void Log(LogModel logModel);
}