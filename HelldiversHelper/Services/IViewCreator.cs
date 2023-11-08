using HelldiversHelper.ViewModels;

namespace HelldiversHelper.Services;

/// <summary>
/// Service for creating and showing views.
/// </summary>
public interface IViewCreator
{
    /// <summary>
    /// Shows a view.
    /// </summary>
    /// <param name="vm">The view model of the view.</param>
    void Show(BaseViewModel vm);

    /// <summary>
    /// Shows a view as a dialog.
    /// </summary>
    /// <param name="vm">The view model of the view.</param>
    /// <returns>The view's dialog result.</returns>
    bool? ShowDialog(BaseViewModel vm);
}
