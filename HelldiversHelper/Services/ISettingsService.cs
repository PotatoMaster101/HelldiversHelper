using System.Globalization;
using HelldiversHelper.Settings;

namespace HelldiversHelper.Services;

/// <summary>
/// Service for updating and saving application settings.
/// </summary>
public interface ISettingsService
{
    /// <summary>
    /// Gets the saved culture from application settings.
    /// </summary>
    /// <returns>The saved culture.</returns>
    CultureInfo GetCulture();

    /// <summary>
    /// Sets the application culture.
    /// </summary>
    /// <param name="culture">The application culture to set.</param>
    void SetCulture(SupportedCulture culture);

    /// <summary>
    /// Resets the culture of current thread to default.
    /// </summary>
    void ResetThreadCulture();

    /// <summary>
    /// Gets the saved key press delay from application settings.
    /// </summary>
    /// <returns>The saved key press delay.</returns>
    int GetKeyPressDelay();

    /// <summary>
    /// Sets the key press delay.
    /// </summary>
    /// <param name="value">The key press delay to set to.</param>
    void SetKeyPressDelay(int value);

    /// <summary>
    /// Loads the application settings.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token for cancelling the operation.</param>
    Task Load(CancellationToken cancellationToken = default);

    /// <summary>
    /// Saves the application settings.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token for cancelling the operation.</param>
    Task Save(CancellationToken cancellationToken = default);
}
