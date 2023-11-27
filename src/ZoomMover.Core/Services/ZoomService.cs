using System.Diagnostics;

namespace ZoomMover.Core.Services;

/// <summary>
/// Implementation of the <see cref="IZoomService"/> interface.
/// </summary>
public class ZoomService : IZoomService
{
    private readonly string mainWindowsTitle = "Zoom Meeting";

    /// <inheritdoc />
    public Process GetZoomProcess()
    {
        return Process.GetProcesses().SingleOrDefault(p => p.MainWindowTitle.Equals(mainWindowsTitle, StringComparison.OrdinalIgnoreCase));
    }
}
