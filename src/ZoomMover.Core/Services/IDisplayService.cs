using ZoomMover.Core.Models;

namespace ZoomMover.Core.Services;

/// <summary>
/// Display service contract.
/// </summary>
public interface IDisplayService
{
    /// <summary>
    /// Gets all displays.
    /// </summary>
    /// <returns>Returns a collection of <see cref="Display"/>.</returns>
    IList<Display> GetDisplays();
}
