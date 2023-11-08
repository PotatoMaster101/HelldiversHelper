using CommunityToolkit.Mvvm.Input;
using HelldiversHelper.Resources;

namespace HelldiversHelper.ViewModels;

/// <summary>
/// View model for dialog views.
/// </summary>
public class DialogViewModel : BaseViewModel
{
    private string _cancelButtonText = ResourceStrings.Cancel;
    private string _okButtonText = ResourceStrings.Ok;
    private bool _showCancelButton = true;

    /// <summary>
    /// Gets or sets the cancel button text.
    /// </summary>
    public string CancelButtonText
    {
        get => _cancelButtonText;
        set
        {
            _cancelButtonText = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Gets or sets the OK button text.
    /// </summary>
    public string OkButtonText
    {
        get => _okButtonText;
        set
        {
            _okButtonText = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Gets or sets whether the cancel button is showing.
    /// </summary>
    public bool ShowCancelButton
    {
        get => _showCancelButton;
        set
        {
            _showCancelButton = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Gets the command for cancel button.
    /// </summary>
    public IRelayCommand CancelCommand { get; }

    /// <summary>
    /// Gets the command for OK button.
    /// </summary>
    public IRelayCommand OkCommand { get; }

    /// <summary>
    /// Creates a new instance of <see cref="DialogViewModel"/>.
    /// </summary>
    public DialogViewModel()
    {
        CancelCommand = new RelayCommand(OnCancel);
        OkCommand = new RelayCommand(OnOk);
    }

    /// <summary>
    /// Invoked when user clicks cancel.
    /// </summary>
    protected virtual void OnCancel()
    {
        Close(false);
    }

    /// <summary>
    /// Invoked when user clicks OK.
    /// </summary>
    protected virtual void OnOk()
    {
        Close(true);
    }
}
