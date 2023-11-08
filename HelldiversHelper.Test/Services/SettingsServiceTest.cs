using System.Globalization;
using System.Text.Json;
using HelldiversHelper.Services;
using HelldiversHelper.Settings;
using Xunit;

namespace HelldiversHelper.Test.Services;

/// <summary>
/// Unit tests for <see cref="SettingsService"/>.
/// </summary>
public class SettingsServiceTest
{
    private const string SettingsFileName = "settings.json";
    private const string ZhCulture = "zh";
    private readonly CultureInfo _defaultCulture = CultureInfo.CurrentCulture;

    [Fact]
    public void SetCulture_SetsCulture()
    {
        // arrange
        var service = new SettingsService();

        // act
        service.SetCulture(SupportedCulture.Chinese);

        // assert
        Assert.Equal(ZhCulture, service.GetCulture().Name);
    }

    [Fact]
    public async Task Load_LoadsSettings()
    {
        // arrange
        var service = new SettingsService();
        var settings = new ApplicationSettings
        {
            Culture = SupportedCulture.Chinese
        };

        await File.WriteAllTextAsync(SettingsFileName, JsonSerializer.Serialize(settings));

        // act
        await service.Load();

        // assert
        Assert.Equal(ZhCulture, service.GetCulture().Name);
    }

    [Fact]
    public async Task Load_LoadsDefaultWhenNoSettingsFile()
    {
        // arrange
        var service = new SettingsService();

        if (File.Exists(SettingsFileName))
            File.Delete(SettingsFileName);

        // act
        await service.Load();

        // assert
        Assert.Equal(_defaultCulture, service.GetCulture());
    }

    [Fact]
    public async Task Load_LoadsDefaultWhenSettingsFileInvalid()
    {
        // arrange
        var service = new SettingsService();
        await File.WriteAllTextAsync(SettingsFileName, @"{invalid json");

        // act
        await service.Load();

        // assert
        Assert.Equal(_defaultCulture, service.GetCulture());
    }

    [Fact]
    public void ResetThreadCulture_SetsDefaultCultureWhenCultureUnknown()
    {
        // arrange
        var service = new SettingsService();

        // act
        service.ResetThreadCulture();

        // assert
        Assert.Equal(_defaultCulture, service.GetCulture());
    }

    [Fact]
    public void ResetThreadCulture_SetsInitialCultureWhenCultureKnown()
    {
        // arrange
        var service = new SettingsService();
        service.SetCulture(SupportedCulture.Chinese);
        _ = service.GetCulture();

        // act
        service.ResetThreadCulture();

        // assert
        Assert.Equal(ZhCulture, service.GetCulture().Name);
    }

    [Fact]
    public async Task Save_SavesSettings()
    {
        // arrange
        var service = new SettingsService();

        if (File.Exists(SettingsFileName))
            File.Delete(SettingsFileName);

        // act
        await service.Save();

        // assert
        Assert.True(File.Exists(SettingsFileName));
    }

    [Theory]
    [InlineData(Constants.MinimumKeyPressDelay, Constants.MinimumKeyPressDelay)]
    [InlineData(Constants.MinimumKeyPressDelay - 1, Constants.MinimumKeyPressDelay)]
    [InlineData(Constants.MinimumKeyPressDelay + 1, Constants.MinimumKeyPressDelay + 1)]
    public async Task GetKeyPressDelay_ReturnsCorrectValue(int delay, int expected)
    {
        // arrange
        var settings = new ApplicationSettings
        {
            KeyPressDelay = delay
        };
        await File.WriteAllTextAsync(SettingsFileName, JsonSerializer.Serialize(settings));

        var service = new SettingsService();
        await service.Load();

        // act
        var result = service.GetKeyPressDelay();

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(Constants.MinimumKeyPressDelay, Constants.MinimumKeyPressDelay)]
    [InlineData(Constants.MinimumKeyPressDelay - 1, Constants.MinimumKeyPressDelay)]
    [InlineData(Constants.MinimumKeyPressDelay + 1, Constants.MinimumKeyPressDelay + 1)]
    public async Task SetKeyPressDelay_SetsCorrectValue(int delay, int expected)
    {
        // arrange
        var service = new SettingsService();

        // act
        service.SetKeyPressDelay(delay);

        // assert
        await service.Save();
        var settings = JsonSerializer.Deserialize<ApplicationSettings>(await File.ReadAllTextAsync(SettingsFileName));
        Assert.NotNull(settings);
        Assert.Equal(expected, settings.KeyPressDelay);
    }
}
