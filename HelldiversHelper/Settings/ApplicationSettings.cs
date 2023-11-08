namespace HelldiversHelper.Settings;

/// <summary>
/// Represents the application settings.
/// </summary>
public class ApplicationSettings
{
    /// <summary>
    /// Gets or sets the application culture.
    /// </summary>
    public SupportedCulture Culture { get; set; } = SupportedCulture.Default;

    /// <summary>
    /// Gets or sets the key press delay.
    /// </summary>
    public int KeyPressDelay { get; set; } = Constants.MinimumKeyPressDelay;
}
