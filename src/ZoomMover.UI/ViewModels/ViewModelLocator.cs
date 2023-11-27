using CommunityToolkit.Mvvm.DependencyInjection;

namespace ZoomMover.UI.ViewModels;

/// <summary>
/// View model locator.
/// </summary>
internal class ViewModelLocator
{
    /// <summary>
    /// Gets the <see cref="MainViewModel"/>.
    /// </summary>
    public static MainViewModel Main => Ioc.Default.GetService<MainViewModel>();

    /// <summary>
    /// Gets the <see cref="OperatorViewModel"/>.
    /// </summary>
    public static OperatorViewModel Operator => Ioc.Default.GetService<OperatorViewModel>();

    /// <summary>
    /// Gets the <see cref="DisplayViewModel"/>.
    /// </summary>
    public static DisplayViewModel Display => Ioc.Default.GetService<DisplayViewModel>();
}