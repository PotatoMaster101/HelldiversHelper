using System.Globalization;
using System.Windows;
using HelldiversHelper.Services;
using HelldiversHelper.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace HelldiversHelper;

/// <inheritdoc cref="Application"/>
public partial class App
{
    private readonly IServiceProvider _serviceProvider = GetServiceProvider();

    /// <inheritdoc cref="Application.OnStartup"/>
    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // load and apply settings
        var settings = _serviceProvider.GetRequiredService<ISettingsService>();
        await settings.Load();
        SetApplicationCulture(settings.GetCulture());

        // start main view
        var viewCreator = _serviceProvider.GetRequiredService<IViewCreator>();
        viewCreator.Show(new MainViewModel(
                             settings,
                             viewCreator,
                             _serviceProvider.GetRequiredService<IKeyboardService>(),
                             _serviceProvider.GetRequiredService<IImportExportService>()));
    }

    /// <inheritdoc cref="Application.OnExit"/>
    protected override async void OnExit(ExitEventArgs e)
    {
        var settings = _serviceProvider.GetRequiredService<ISettingsService>();
        await settings.Save();

        var keyboard = _serviceProvider.GetRequiredService<IKeyboardService>();
        keyboard.Dispose();
        base.OnExit(e);
    }

    /// <summary>
    /// Creates a new <see cref="IServiceProvider"/> configured with application services.
    /// </summary>
    /// <returns>A new <see cref="IServiceProvider"/> configured with application services.</returns>
    private static IServiceProvider GetServiceProvider()
    {
        var services = new ServiceCollection();
        services.AddSingleton<ISettingsService, SettingsService>();
        services.AddSingleton<IViewCreator, ViewCreator>();
        services.AddSingleton<IKeyboardService, KeyboardService>();
        services.AddSingleton<IImportExportService, ImportExportService>();
        return services.BuildServiceProvider();
    }

    /// <summary>
    /// Sets the application culture.
    /// </summary>
    /// <param name="culture">The culture to set to.</param>
    private static void SetApplicationCulture(CultureInfo culture)
    {
        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;
        Thread.CurrentThread.CurrentCulture = culture;
        Thread.CurrentThread.CurrentUICulture = culture;
    }
}
