using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Windows;
using System.Windows.Threading;
using ZoomMover.Core.Extensions;
using ZoomMover.Core.Services;
using ZoomMover.UI.ViewModels;

namespace ZoomMover.UI;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly string appName = "ZoomMover";
    private ILogger logger;
    private Mutex mutex;

    /// <summary>
    /// Handles the <see cref="Application.Startup"/> event.
    /// </summary>
    /// <param name="e">Arguments for the <see cref="Application.Startup"/> event.</param>
    protected override void OnStartup(StartupEventArgs e)
    {
        ConfigureServices();
        if (IsInstanceRunning())
        {
            Shutdown();
        }

        Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
        logger?.LogInformation("Starting application.");
    }

    /// <summary>
    /// Handles the <see cref="Application.Exit"/> event.
    /// </summary>
    /// <param name="e">Arguments for the <see cref="Application.Exit"/> event.</param>
    protected override void OnExit(ExitEventArgs e)
    {
        mutex.Dispose();
        logger.LogInformation("Exiting application.");
    }

    /// <summary>
    /// Configures services for the application.
    /// </summary>
    private void ConfigureServices()
    {
        var services = new ServiceCollection();
        services.AddSingleton<IDisplayService, DisplayService>();
        services.AddSingleton<IWindowService, WindowService>();
        services.AddSingleton<IZoomService, ZoomService>();
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<OperatorViewModel>();
        services.AddSerilog(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), appName, "Logs"));

        var serviceProvider = services.BuildServiceProvider();
        logger = serviceProvider.GetRequiredService<ILogger<App>>();
        Ioc.Default.ConfigureServices(serviceProvider);
    }

    /// <summary>
    /// Indicates if an instance of the application is already running.
    /// </summary>
    /// <returns>Returns a <see cref="bool"/> value indicating if an instance of the application is already running.</returns>
    private bool IsInstanceRunning()
    {
        mutex = new Mutex(true, appName, out var createdNew);
        return !createdNew;
    }

    /// <summary>
    /// Handles any unhandled exception thrown from the UI thread.
    /// </summary>
    /// <param name="sender">Object raising the event.</param>
    /// <param name="e">Event arguments for the unhandled exception.</param>
    private void Current_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        e.Handled = true;
        logger.LogError(e.Exception, "Unhandled exception.");
        Current.Shutdown();
    }
}