using FluentLogger.Core.Models;
using Serilog;
using Serilog.Sinks.Fluentd;

namespace FluentLogger.AspNetCore;

public class FluentLoggerBuilder
{
    private LoggerConfiguration _loggerConfiguration = new();
    private const string DefaultOutputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u}] [{SourceContext}] {Message}{NewLine}{Exception}";

    public FluentLoggerBuilder SetMinimumLevel(MinimumLevel minimumLevel)
    {
        switch (minimumLevel)
        {
            case MinimumLevel.Debug:
                _loggerConfiguration.MinimumLevel.Debug();
                break;
            case MinimumLevel.Information:
                _loggerConfiguration.MinimumLevel.Information();
                break;
            case MinimumLevel.Warning:
                _loggerConfiguration.MinimumLevel.Warning();
                break;
            case MinimumLevel.Error:
                _loggerConfiguration.MinimumLevel.Error();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(minimumLevel), minimumLevel, null);
        }
        
        return this;
    }
    
    public FluentLoggerBuilder AddFile(string? path, RollingInterval rollingInterval = RollingInterval.Day, string outputTemplate = DefaultOutputTemplate)
    {
        _loggerConfiguration.WriteTo.File(path!, rollingInterval: rollingInterval, outputTemplate: outputTemplate);
        return this;
    }

    public FluentLoggerBuilder AddConsole(string outputTemplate = DefaultOutputTemplate)
    {
        _loggerConfiguration.WriteTo.Console(outputTemplate: outputTemplate);
        return this;
    }

    public FluentLoggerBuilder AddFluentD(string url)
    {
        _loggerConfiguration.WriteTo.Fluentd(url: url);

        return this;
    }

    public ILogger Build()
    {
        return _loggerConfiguration.CreateLogger();
    }
}