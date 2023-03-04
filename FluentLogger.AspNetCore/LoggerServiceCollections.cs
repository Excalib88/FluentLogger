using FluentLogger.AspNetCore.FrontendLogs;
using FluentLogger.Core.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace FluentLogger.AspNetCore;

public static class LoggerServiceCollections
{
    public static ILogger AddFluentLogger(this WebApplicationBuilder builder, MinimumLevel minimumLevel = MinimumLevel.Debug, 
        string? filePath = null, string? fluentUrl = null)
    {
        var loggerBuilder = new FluentLoggerBuilder()
            .SetMinimumLevel(minimumLevel)
            .AddConsole();

        if (!string.IsNullOrWhiteSpace(filePath))
        {
            loggerBuilder.AddFile(filePath);
        }

        if (!string.IsNullOrWhiteSpace(fluentUrl))
        {
            loggerBuilder.AddFluentD(fluentUrl);
        }

        builder.Services.AddScoped<IFrontendLogger, FrontendLogger>();
        builder.Logging.AddSerilog();

        return loggerBuilder.Build();
    }

    public static IApplicationBuilder UseFluentLogger(this WebApplication app, string? frontendRoute = "logs")
    {
        app.MapPost($"/{frontendRoute}", (LogModel log, IFrontendLogger frontendLogger) =>
        {
            frontendLogger.Log(log);
            return Results.Ok();
        });

        return app;
    }
}