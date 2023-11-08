using HelldiversHelper.Resources;
using HelldiversHelper.Services;

namespace HelldiversHelper.ViewModels;

/// <summary>
/// View model for key press delay view.
/// </summary>
public class KeyPressDelayViewModel : DialogViewModel
{
    private readonly IViewCreator _viewCreator;
    private readonly int _minDelay;
    private int _delay;

    /// <summary>
    /// Gets or sets the key press delay.
    /// </summary>
    public int Delay
    {
        get => _delay;
        set
        {
            _delay = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Constructs a new instance of <see cref="KeyPressDelayViewModel"/>.
    /// </summary>
    public KeyPressDelayViewModel(IViewCreator viewCreator, int delay, int minDelay = Constants.MinimumKeyPressDelay)
    {
        _viewCreator = viewCreator;
        _minDelay = minDelay;
        Delay = delay;
        Title = ResourceStrings.SetKeyPressDelay;
    }

    /// <inheritdoc cref="DialogViewModel.OnOk"/>
    protected override void OnOk()
    {
        if (Delay < _minDelay)
        {
            _viewCreator.ShowDialog(new MessageViewModel
            {
                Message = string.Format(ResourceStrings.DelayOutOfRange, _minDelay),
                ShowCancelButton = false,
                Title = ResourceStrings.Error
            });
            return;
        }

        base.OnOk();
    }
}
