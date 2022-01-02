using WindowsInput;
using WindowsInput.Events;
using WindowsInput.Events.Sources;

namespace HelldiversHelper;

/// <summary>
/// Represents a key mapped to a stratagem.
/// </summary>
public static class GlobalKeyMapper
{
    /// <summary>
    /// Gets the key mappings.
    /// </summary>
    /// <value>The key mappings.</value>
    public static IDictionary<KeyCode, Stratagem> KeyMappings { get; } = new Dictionary<KeyCode, Stratagem>();

    /// <summary>
    /// Keyboard used for listening global key presses.
    /// </summary>
    private static readonly IKeyboardEventSource Keyboard = Capture.Global.KeyboardAsync();

    /// <summary>
    /// Whether this object has been disposed.
    /// </summary>
    private static bool _disposed;

    /// <summary>
    /// Initialises <see cref="GlobalKeyMapper"/>.
    /// </summary>
    static GlobalKeyMapper()
    {
        Keyboard.KeyEvent += async (_, args) =>
        {
            var key = args.Data?.KeyDown?.Key;
            if (key is not null && KeyMappings.TryGetValue(key.Value, out var result))
                await result.Activate();
        };
    }

    /// <summary>
    /// Disposes this object.
    /// </summary>
    public static void Dispose()
    {
        if (!_disposed)
        {
            KeyMappings.Clear();
            Keyboard.Dispose();
            _disposed = true;
        }
    }
}
