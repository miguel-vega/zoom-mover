namespace ZoomMover.Core.Services;

/// <summary>
/// Window service contract.
/// </summary>
public interface IWindowService
{
    /// <summary>
    /// Moves the window to the specified coordinates.
    /// </summary>
    /// <param name="mainWindowHandle">Handle of the window.</param>
    /// <param name="x">X coordinate where the window will be moved to.</param>
    /// <param name="y">Y coordinate where the window will be moved to.</param>
    /// <param name="maximize">Indicates if the window should be maximized after it is moved.</param>
    void MoveWindow(nint mainWindowHandle, int x, int y, bool maximize);
}
