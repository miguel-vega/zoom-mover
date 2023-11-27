using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.WindowsAndMessaging;

namespace ZoomMover.Core.Services;

/// <summary>
/// Implementation of the <see cref="IWindowService"/> interface.
/// </summary>
public class WindowService : IWindowService
{
    /// <inheritdoc/>
    public void MoveWindow(nint mainWindowHandle, int x, int y, bool maximize)
    {
        var wINDOWPLACEMENT = new WINDOWPLACEMENT();
        var hwnd = (HWND)mainWindowHandle;
        PInvoke.GetWindowPlacement(hwnd, ref wINDOWPLACEMENT);

        var height = wINDOWPLACEMENT.rcNormalPosition.bottom - wINDOWPLACEMENT.rcNormalPosition.top;
        var width = wINDOWPLACEMENT.rcNormalPosition.right - wINDOWPLACEMENT.rcNormalPosition.left;

        wINDOWPLACEMENT.rcNormalPosition.top = y;
        wINDOWPLACEMENT.rcNormalPosition.bottom = wINDOWPLACEMENT.rcNormalPosition.top + height;
        wINDOWPLACEMENT.rcNormalPosition.left = x;
        wINDOWPLACEMENT.rcNormalPosition.right = wINDOWPLACEMENT.rcNormalPosition.left + width;

        PInvoke.SetWindowPlacement(hwnd, wINDOWPLACEMENT);

        if (maximize)
        {
            PInvoke.ShowWindow(hwnd, SHOW_WINDOW_CMD.SW_SHOWMAXIMIZED);
        }
    }
}
