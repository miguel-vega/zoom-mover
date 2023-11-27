namespace ZoomMover.Core.Models;

/// <summary>
/// Model class for a display.
/// </summary>
public sealed class Display
{
    /// <summary>
    /// Gets or sets the ID of the display.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the number of the display.
    /// </summary>
    public int Number { get; set; }

    /// <summary>
    /// Gets or sets if this is the primary display.
    /// </summary>
    public bool IsPrimary { get; set; }

    /// <summary>
    /// Gets or sets the X coordinate of the display.
    /// </summary>
    public int X { get; set; }

    /// <summary>
    /// Gets or sets the Y coordinate of the display.
    /// </summary>
    public int Y { get; set; }

    /// <summary>
    /// Gets or sets the width of the display.
    /// </summary>
    public int Width { get; set; }

    /// <summary>
    /// Gets or sets the height of the display.
    /// </summary>
    public int Height { get; set; }
}
