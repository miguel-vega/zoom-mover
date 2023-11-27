using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace ZoomMover.Core.Extensions;

/// <summary>
/// Extension methods for the <see cref="IServiceCollection"/> interface.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds Serilog.
    /// </summary>
    /// <param name="services">Implementation of the <see cref="IServiceCollection"/> interface.</param>
    /// <param name="directory">Path to the directory where the logs will be stored.</param>
    public static void AddSerilog(this IServiceCollection services, string directory)
    {
        var logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File(Path.Combine(directory, ".log"), rollingInterval: RollingInterval.Day)
            .CreateLogger();

        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.AddSerilog(logger, true);
        });
    }
}