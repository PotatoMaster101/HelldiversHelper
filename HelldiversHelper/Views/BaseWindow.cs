using System.ComponentModel;
using System.Windows;
using HelldiversHelper.ViewModels;

namespace HelldiversHelper.Views;

/// <summary>
/// Represents the base window for all views.
/// </summary>
public class BaseWindow : Window
{
    /// <summary>
    /// Constructs a new instance of <see cref="BaseWindow"/>.
    /// </summary>
    protected BaseWindow()
    {
        ContentRendered += OnContentRendered;
        Closing += OnClosing;
    }

    /// <summary>
    /// Invoked when window content has rendered.
    /// </summary>
    protected virtual void OnContentRendered(object? sender, EventArgs e)
    {
        if (DataContext is BaseViewModel vm)
            vm.OnContentRendered();
    }

    /// <summary>
    /// Invoked when window is closing.
    /// </summary>
    protected virtual void OnClosing(object? sender, CancelEventArgs e)
    {
        if (DataContext is BaseViewModel vm)
            vm.OnClosing(e);
    }
}
