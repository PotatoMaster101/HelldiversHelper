using WindowsInput.Events;

namespace HelldiversHelper.Services;

/// <summary>
/// Service for reading/sending keyboard inputs.
/// </summary>
public interface IKeyboardService : IDisposable
{
    /// <summary>
    /// Fired when a key is pressed.
    /// </summary>
    event EventHandler<KeyCode> KeyDown;

    /// <summary>
    /// Fired when a key is released.
    /// </summary>
    event EventHandler<KeyCode> KeyUp;

    /// <summary>
    /// Gets all the keys currently held down.
    /// </summary>
    IReadOnlySet<KeyCode> KeysDown { get; }

    /// <summary>
    /// Gets or sets whether the key listener is enabled.
    /// </summary>
    bool Enabled { get; set; }

    /// <summary>
    /// Gets whether the specified key is a modifier key.
    /// </summary>
    /// <param name="key">The key to check.</param>
    /// <returns>Whether the specified key is a modifier key.</returns>
    bool IsModifier(KeyCode key);

    /// <summary>
    /// Inputs the keys specified.
    /// </summary>
    /// <param name="keys">The keys to input.</param>
    /// <param name="delay">The delay between each key press.</param>
    Task InputKeys(IEnumerable<KeyCode> keys, int delay);
}
