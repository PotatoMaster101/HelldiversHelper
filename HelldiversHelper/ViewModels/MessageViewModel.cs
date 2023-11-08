namespace HelldiversHelper.ViewModels;

/// <summary>
/// View model for message view.
/// </summary>
public class MessageViewModel : DialogViewModel
{
    private string _message = default!;

    /// <summary>
    /// Gets or sets the message.
    /// </summary>
    public required string Message
    {
        get => _message;
        set
        {
            _message = value;
            OnPropertyChanged();
        }
    }
}
