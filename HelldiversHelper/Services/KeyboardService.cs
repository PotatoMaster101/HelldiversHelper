using WindowsInput;
using WindowsInput.Events;
using WindowsInput.Events.Sources;

namespace HelldiversHelper.Services;

/// <inheritdoc cref="IKeyboardService"/>
public class KeyboardService : IKeyboardService
{
    private readonly IKeyboardEventSource _keyboard = Capture.Global.KeyboardAsync();
    private readonly HashSet<KeyCode> _keysDown = new();

    /// <inheritdoc cref="IKeyboardService.KeyDown"/>
    public event EventHandler<KeyCode>? KeyDown;

    /// <inheritdoc cref="IKeyboardService.KeyUp"/>
    public event EventHandler<KeyCode>? KeyUp;

    /// <inheritdoc cref="IKeyboardService.KeysDown"/>
    public IReadOnlySet<KeyCode> KeysDown => _keysDown;

    /// <inheritdoc cref="IKeyboardService.Enabled"/>
    public bool Enabled
    {
        get => _keyboard.Enabled;
        set => _keyboard.Enabled = value;
    }

    /// <summary>
    /// Constructs a new instance of <see cref="KeyboardService"/>.
    /// </summary>
    public KeyboardService()
    {
        _keyboard.KeyEvent += OnReadKey;
        KeyDown += OnKeyDown;
        KeyUp += OnKeyUp;
    }

    /// <inheritdoc cref="IKeyboardService.IsModifier"/>
    public bool IsModifier(KeyCode key)
    {
        return key is KeyCode.LShift or KeyCode.LControl or KeyCode.LAlt;
    }

    /// <inheritdoc cref="IKeyboardService.InputKeys"/>
    public async Task InputKeys(IEnumerable<KeyCode> keys, int delay)
    {
        if (delay <= 0)
            throw new ArgumentOutOfRangeException(nameof(delay));

        var events = Simulate.Events();
        foreach (var key in keys)
            events.Click(key).Wait(delay);

        await events.Invoke().ConfigureAwait(false);
    }

    /// <inheritdoc cref="IDisposable.Dispose"/>
    public void Dispose()
    {
        GC.SuppressFinalize(this);
        _keyboard.KeyEvent -= OnReadKey;
        KeyDown -= OnKeyDown;
        KeyUp -= OnKeyUp;
        _keyboard.Dispose();
    }

    /// <summary>
    /// Invoked on keyboard input event.
    /// </summary>
    private void OnReadKey(object? sender, EventSourceEventArgs<KeyboardEvent> e)
    {
        if (e.Data.KeyDown?.Key is not null)
            KeyDown?.Invoke(this, e.Data.KeyDown.Key);
        if (e.Data.KeyUp?.Key is not null)
            KeyUp?.Invoke(this, e.Data.KeyUp.Key);
    }

    /// <summary>
    /// Invoked on key down.
    /// </summary>
    private void OnKeyDown(object? sender, KeyCode e)
    {
        _keysDown.Add(e);
    }

    /// <summary>
    /// Invoked on key up.
    /// </summary>
    private void OnKeyUp(object? sender, KeyCode e)
    {
        _keysDown.Remove(e);
    }
}
