using System.ComponentModel;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.Messaging;
using HelldiversHelper.Message;
using HelldiversHelper.Resources;

namespace HelldiversHelper.ViewModels;

/// <summary>
/// Base for all view models.
/// </summary>
public abstract class BaseViewModel : INotifyPropertyChanged
{
    private string _title = ResourceStrings.HelldiversHelper;

    /// <summary>
    /// Gets or sets the window title.
    /// </summary>
    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            OnPropertyChanged();
        }
    }

    /// <inheritdoc cref="INotifyPropertyChanged.PropertyChanged"/>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Closes the view.
    /// </summary>
    /// <param name="result">The dialog result of the view.</param>
    public void Close(bool? result)
    {
        WeakReferenceMessenger.Default.Send(new CloseViewMessage
        {
            DataContext = this,
            DialogResult = result
        });
    }

    /// <summary>
    /// Invoked when the view content is loaded.
    /// </summary>
    public virtual void OnContentRendered()
    {
    }

    /// <summary>
    /// Invoked when the view is closing.
    /// </summary>
    public virtual void OnClosing(CancelEventArgs e)
    {
    }

    /// <summary>
    /// Invoked when a property has changed.
    /// </summary>
    /// <param name="propertyName">The name of the property.</param>
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
