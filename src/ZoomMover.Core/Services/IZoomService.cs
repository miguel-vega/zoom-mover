using System.Diagnostics;

namespace ZoomMover.Core.Services;

/// <summary>
/// Zoom service contract.
/// </summary>
public interface IZoomService
{
    /// <summary>
    /// Gets the <see cref="Process"/> of the Zoom application.
    /// </summary>
    /// <returns>Returns the <see cref="Process"/> of the Zoom application.</returns>
    Process GetZoomProcess();
}
