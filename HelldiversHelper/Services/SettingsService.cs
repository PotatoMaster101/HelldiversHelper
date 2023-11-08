using System.Globalization;
using System.IO;
using System.Text.Json;
using HelldiversHelper.Settings;

namespace HelldiversHelper.Services;

/// <inheritdoc cref="ISettingsService"/>
public class SettingsService : ISettingsService
{
    private const string SettingsFileName = "settings.json";
    private const string ZhCulture = "zh";
    private readonly CultureInfo _defaultCulture = CultureInfo.CurrentCulture;
    private CultureInfo? _initialCulture;
    private ApplicationSettings _settings = new();

    /// <inheritdoc cref="ISettingsService.GetCulture"/>
    public CultureInfo GetCulture()
    {
        var culture = _settings.Culture == SupportedCulture.Default ? _defaultCulture : new CultureInfo(ZhCulture);
        _initialCulture ??= culture;
        return culture;
    }

    /// <inheritdoc cref="ISettingsService.SetCulture"/>
    public void SetCulture(SupportedCulture culture)
    {
        _settings.Culture = culture;
    }

    /// <inheritdoc cref="ISettingsService.ResetThreadCulture"/>
    public void ResetThreadCulture()
    {
        var culture = _initialCulture ?? _defaultCulture;
        Thread.CurrentThread.CurrentCulture = culture;
        Thread.CurrentThread.CurrentUICulture = culture;
    }

    /// <inheritdoc cref="ISettingsService.GetKeyPressDelay"/>
    public int GetKeyPressDelay()
    {
        if (_settings.KeyPressDelay < Constants.MinimumKeyPressDelay)
            _settings.KeyPressDelay = Constants.MinimumKeyPressDelay;

        return _settings.KeyPressDelay;
    }

    /// <inheritdoc cref="ISettingsService.SetKeyPressDelay"/>
    public void SetKeyPressDelay(int value)
    {
        if (value < Constants.MinimumKeyPressDelay)
            value = Constants.MinimumKeyPressDelay;

        _settings.KeyPressDelay = value;
    }

    /// <inheritdoc cref="ISettingsService.Load"/>
    public async Task Load(CancellationToken cancellationToken = default)
    {
        if (!File.Exists(SettingsFileName))
        {
            _settings = new ApplicationSettings();
            return;
        }

        try
        {
            var content = await File.ReadAllTextAsync(SettingsFileName, cancellationToken).ConfigureAwait(false);
            _settings = JsonSerializer.Deserialize<ApplicationSettings>(content) ?? new ApplicationSettings();
        }
        catch
        {
            _settings = new ApplicationSettings();
        }
    }

    /// <inheritdoc cref="ISettingsService.Save"/>
    public Task Save(CancellationToken cancellationToken = default)
    {
        var content = JsonSerializer.Serialize(_settings);
        return File.WriteAllTextAsync(SettingsFileName, content, cancellationToken);
    }
}
