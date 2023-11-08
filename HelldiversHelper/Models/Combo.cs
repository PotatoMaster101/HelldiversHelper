using System.Text.Json.Serialization;
using WindowsInput.Events;

namespace HelldiversHelper.Models;

/// <summary>
/// Represents a Helldiver stratagem trigger combo.
/// </summary>
public class Combo
{
    /// <summary>
    /// Gets or sets the Helldivers stratagem.
    /// </summary>
    public required string Stratagem { get; set; }

    /// <summary>
    /// Gets or sets the combo trigger key.
    /// </summary>
    public KeyCode TriggerKey { get; set; } = KeyCode.Z;

    /// <summary>
    /// Gets or sets the list of modifier keys.
    /// </summary>
    public List<KeyCode> ModifierKeys { get; set; } = new();

    /// <summary>
    /// Gets the modifier keys as a string.
    /// </summary>
    [JsonIgnore]
    public string ModifierKeysText => string.Join(" + ", ModifierKeys);

    /// <summary>
    /// Gets or sets whether this combo is enabled.
    /// </summary>
    [JsonIgnore]
    public bool Enabled { get; set; } = true;

    /// <summary>
    /// Determines whether this combo can be triggered.
    /// </summary>
    /// <param name="keys">The input keys.</param>
    /// <returns>Whether this combo can be triggered.</returns>
    public bool CanTrigger(IReadOnlySet<KeyCode> keys)
    {
        if (!Enabled || keys.Count != ModifierKeys.Count + 1)
            return false;
        return keys.Contains(TriggerKey) && ModifierKeys.All(keys.Contains);
    }

    /// <summary>
    /// Determines whether the trigger for this combo is the same as another combo.
    /// </summary>
    /// <param name="another">The other combo the check.</param>
    /// <returns>Whether the trigger for this combo is the same as another combo.</returns>
    public bool IsSameTrigger(Combo another)
    {
        return TriggerKey == another.TriggerKey && ModifierKeys.SequenceEqual(another.ModifierKeys);
    }
}
