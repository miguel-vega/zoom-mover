using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Windows.Win32;
using Windows.Win32.Graphics.Gdi;
using ZoomMover.Core.Models;

namespace ZoomMover.Core.Services;

/// <summary>
/// Implementation of the <see cref="IDisplayService"/> interface.
/// </summary>
public sealed class DisplayService : IDisplayService
{
    private readonly string pattern = @"DISPLAY(\d+)";

    /// <inheritdoc />
    public IList<Display> GetDisplays()
    {
        var displayDevices = new List<Display>();
        var screens = Screen.AllScreens;

        DISPLAY_DEVICEW dISPLAY_DEVICEW = new();
        dISPLAY_DEVICEW.cb = (uint)Marshal.SizeOf(dISPLAY_DEVICEW);

        for (uint i = 0; PInvoke.EnumDisplayDevices(null, i, ref dISPLAY_DEVICEW, 0); i++)
        {
            if (dISPLAY_DEVICEW.StateFlags == 0)
            {
                continue;
            }

            dISPLAY_DEVICEW = new();
            dISPLAY_DEVICEW.cb = (uint)Marshal.SizeOf(dISPLAY_DEVICEW);
            PInvoke.EnumDisplayDevices(screens[i].DeviceName, 0, ref dISPLAY_DEVICEW, 0);

            var regex = new Regex(pattern);
            var match = regex.Match(screens[i].DeviceName);

            displayDevices.Add(new Display
            {
                Id = $"{dISPLAY_DEVICEW.DeviceID}",
                Number = Convert.ToInt32(match.Groups[1].Value),
                X = screens[i].WorkingArea.X,
                Y = screens[i].WorkingArea.Y,
                Width = screens[i].WorkingArea.Width,
                Height = screens[i].WorkingArea.Height
            });
        }

        return displayDevices;
    }
}
