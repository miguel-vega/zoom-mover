using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using ZoomMover.Core.Models;
using ZoomMover.Core.Services;
using ZoomMover.UI.Properties;
using ZoomMover.UI.Views;

namespace ZoomMover.UI.ViewModels;

/// <summary>
/// View model for the <see cref="OperatorView"/>.
/// </summary>
internal class OperatorViewModel : ObservableObject
{
    private readonly IZoomService zoomService;
    private readonly IDisplayService displayService;
    private readonly IWindowService windowService;
    private readonly ILogger logger;
    private IEnumerable<Display> displays;
    private Display selectedDisplay;
    private Process zoomProcess;
    private bool isDisplaySelected;
    private string status;

    /// <summary>
    /// Gets all displays.
    /// </summary>
    public IEnumerable<Display> Displays
    {
        get => displays;
        set
        {
            displays = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Gets or sets the selected display.
    /// </summary>
    public Display SelectedDisplay
    {
        get => selectedDisplay;
        set
        {
            selectedDisplay = value;
            IsDisplaySelected = true;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Gets a <see cref="bool"/> value indicating if a display was selected.
    /// </summary>
    public bool IsDisplaySelected
    {
        get => isDisplaySelected;
        set
        {
            isDisplaySelected = value;
            OnPropertyChanged();
            MoveZoomCommand?.NotifyCanExecuteChanged();
        }
    }

    /// <summary>
    /// Gets or sets the status of the user control.
    /// </summary>
    public string Status
    {
        get => status;
        set
        {
            status = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Gets the command to move the Zoom application.
    /// </summary>
    public RelayCommand MoveZoomCommand { get; }

    /// <summary>
    /// Gets the command to identify the displays.
    /// </summary>
    public RelayCommand IdentifyDisplaysCommand { get; }

    /// <summary>
    /// Gets the command to reinitialize the user control.
    /// </summary>
    public RelayCommand ReinitializeCommand { get; }

    /// <summary>
    /// Initializes an instance of the <see cref="OperatorViewModel"/> class.
    /// </summary>
    /// <param name="zoomService">Implementation of the <see cref="IZoomService"/> interface.</param>
    /// <param name="displayService">Implementation of the <see cref="IDisplayService"/> interface.</param>
    /// <param name="windowService">Implementation of the <see cref="IWindowsService"/> interface. </param>
    /// <param name="logger">Implementation of the <see cref="ILogger{OperatorViewModel}"/> interface.</param>
    public OperatorViewModel(IZoomService zoomService, IDisplayService displayService, IWindowService windowService, ILogger<OperatorViewModel> logger)
    {
        this.zoomService = zoomService;
        this.displayService = displayService;
        this.windowService = windowService;
        this.logger = logger;

        ReinitializeCommand = new RelayCommand(Initialize);
        MoveZoomCommand = new RelayCommand(MoveZoom, CanMoveZoom);
        IdentifyDisplaysCommand = new RelayCommand(IdentifyDisplays);

        Initialize();
    }

    /// <summary>
    /// Initializes the user control.
    /// </summary>
    public void Initialize()
    {
        Displays = displayService.GetDisplays();
        if (Displays.Count() < 2)
        {
            Status = Resource.MULTIPLE_DISPLAY_REQUIRED;
            return;
        }
        logger.LogInformation("Number of displays found: {displayCount}", displays.Count());

        zoomProcess = zoomService.GetZoomProcess();
        if (zoomProcess == null)
        {
            Status = Resource.ZOOM_IS_NOT_RUNNING;
            return;
        }
        logger.LogInformation("Zoom Process ID: {processId}", zoomProcess.Id);

        Status = String.Empty;
    }

    /// <summary>
    /// Moves the Zoom application to the coordinates of the selected screen.
    public void MoveZoom()
    {
        try
        {
            windowService.MoveWindow(zoomProcess.MainWindowHandle, selectedDisplay.X, selectedDisplay.Y, true);

            var message = string.Format(Resource.SUCCESSFULLY_MOVED, zoomProcess.ProcessName, selectedDisplay.Number);
            logger.LogInformation(message);
            Status = message;
        }
        catch (Exception exception)
        {
            logger.LogError(exception, exception.Message);
            Status = string.Format(Resource.FAILED_TO_MOVE, zoomProcess.ProcessName);
        }
    }

    /// <summary>
    /// Indicates if <see cref="MoveZoom"/> can be executed.
    /// </summary>
    /// <returns>Returns a <see cref="bool"/> value indicating if <see cref="MoveZoom"/> can be executed.</returns>
    public bool CanMoveZoom()
    {
        return zoomProcess != null && CanIdenfityDisplays() && IsDisplaySelected;
    }

    /// <summary>
    /// Identifies the displays.
    /// </summary>
    public void IdentifyDisplays()
    {
        foreach(var display in Displays)
        {
            var displayView = new DisplayView
            {
                DataContext = new DisplayViewModel
                {
                    DisplayNumber = display.Number
                }
            };
            displayView.Show();
        }
    }

    /// <summary>
    /// Indicates if <see cref="IdentifyDisplays"/> can be executed.
    /// </summary>
    /// <returns>Returns a <see cref="bool"/> value indicating if <see cref="IdentifyDisplays"/> can be executed.</returns>
    public bool CanIdenfityDisplays()
    {
        return Displays.Count() > 1;
    }
}
