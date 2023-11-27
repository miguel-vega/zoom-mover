using CommunityToolkit.Mvvm.ComponentModel;

namespace ZoomMover.UI.ViewModels;

/// <summary>
/// View model for the <see cref="DisplayView"/>.
/// </summary>
internal class DisplayViewModel : ObservableObject
{
    private int displayNumber;

    /// <summary>
    /// Gets or sets the display number.
    /// </summary>
    public int DisplayNumber
    {
        get => displayNumber;
        set
        {
            displayNumber = value;
            OnPropertyChanged();
        }
    }

    
}
