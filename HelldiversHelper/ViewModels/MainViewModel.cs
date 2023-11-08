using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using HelldiversHelper.Models;
using HelldiversHelper.Resources;
using HelldiversHelper.Services;
using HelldiversHelper.Settings;
using WindowsInput.Events;

namespace HelldiversHelper.ViewModels;

/// <summary>
/// View model for main view.
/// </summary>
public class MainViewModel : BaseViewModel
{
    private readonly IImportExportService _importExportService;
    private readonly IKeyboardService _keyboardService;
    private readonly ISettingsService _settingsService;
    private readonly IViewCreator _viewCreator;

    /// <summary>
    /// Gets the list of combos.
    /// </summary>
    public ObservableCollection<Combo> Combos { get; } = new();

    /// <summary>
    /// Gets the command for adding a combo.
    /// </summary>
    public IRelayCommand AddComboCommand { get; }

    /// <summary>
    /// Gets the command for changing application language.
    /// </summary>
    public IRelayCommand<string> SetLanguageCommand { get; }

    /// <summary>
    /// Gets the command for importing a config.
    /// </summary>
    public IAsyncRelayCommand ImportCombosCommand { get; }

    /// <summary>
    /// Gets the command for exporting a config.
    /// </summary>
    public IAsyncRelayCommand ExportCombosCommand { get; }

    /// <summary>
    /// Gets the command for setting key press delay.
    /// </summary>
    public IRelayCommand SetKeyPressDelayCommand { get; }

    /// <summary>
    /// Constructs a new instance of <see cref="MainViewModel"/>.
    /// </summary>
    public MainViewModel(
        ISettingsService settingsService,
        IViewCreator viewCreator,
        IKeyboardService keyboardService,
        IImportExportService importExportService)
    {
        _settingsService = settingsService;
        _viewCreator = viewCreator;
        _keyboardService = keyboardService;
        _keyboardService.KeyDown += OnKeyDown;
        _importExportService = importExportService;

        AddComboCommand = new RelayCommand(OnAddCombo);
        SetLanguageCommand = new RelayCommand<string>(OnSetLanguage);
        ImportCombosCommand = new AsyncRelayCommand(OnImportCombos);
        ExportCombosCommand = new AsyncRelayCommand(OnExportCombos);
        SetKeyPressDelayCommand = new RelayCommand(OnSetKeyPressDelay);
    }

    /// <inheritdoc cref="BaseViewModel.OnContentRendered"/>
    public override void OnContentRendered()
    {
        base.OnContentRendered();

        // update culture after rendering to ensure culture does not change
        _settingsService.ResetThreadCulture();
    }

    /// <summary>
    /// Invoked when user adds a combo.
    /// </summary>
    private void OnAddCombo()
    {
        _keyboardService.KeyDown -= OnKeyDown;
        try
        {
            var vm = new AddComboViewModel(_keyboardService, _viewCreator);
            if (_viewCreator.ShowDialog(vm) != true)
                return;

            var combo = vm.GetCombo();
            if (Combos.Any(x => x.IsSameTrigger(combo)))
            {
                _viewCreator.ShowDialog(new MessageViewModel
                {
                    Title = ResourceStrings.Warning,
                    Message = ResourceStrings.SameTriggerExists,
                    ShowCancelButton = false
                });
                return;
            }
            Combos.Add(vm.GetCombo());
        }
        finally
        {
            _keyboardService.KeyDown += OnKeyDown;
        }
    }

    /// <summary>
    /// Invoked when user changes application language.
    /// </summary>
    /// <param name="obj">The new language to change to.</param>
    private void OnSetLanguage(string? obj)
    {
        _settingsService.SetCulture(obj == "ZH" ? SupportedCulture.Chinese : SupportedCulture.Default);
        _viewCreator.ShowDialog(new MessageViewModel
        {
            Title = ResourceStrings.Warning,
            Message = ResourceStrings.SetLanguageWarning,
            ShowCancelButton = false
        });
    }

    /// <summary>
    /// Invoked when user imports a config.
    /// </summary>
    private async Task OnImportCombos()
    {
        var file = _importExportService.OpenDialog(true);
        if (string.IsNullOrEmpty(file))
            return;

        Combos.Clear();
        try
        {
            foreach (var combo in await _importExportService.ImportCombos(file))
                Combos.Add(combo);
        }
        catch (Exception ex)
        {
            _viewCreator.ShowDialog(new MessageViewModel
            {
                Message = ex.Message,
                Title = ResourceStrings.Error,
                ShowCancelButton = false
            });
        }
    }

    /// <summary>
    /// Invoked when user exports a config.
    /// </summary>
    private async Task OnExportCombos()
    {
        var file = _importExportService.OpenDialog(false);
        if (!string.IsNullOrEmpty(file))
            await _importExportService.ExportCombos(Combos, file);
    }

    /// <summary>
    /// Invoked when user sets the key press delay.
    /// </summary>
    private void OnSetKeyPressDelay()
    {
        var vm = new KeyPressDelayViewModel(_viewCreator, _settingsService.GetKeyPressDelay());
        if (_viewCreator.ShowDialog(vm) == true)
            _settingsService.SetKeyPressDelay(vm.Delay);
    }

    /// <summary>
    /// Invoked on keyboard key down.
    /// </summary>
    private async void OnKeyDown(object? sender, KeyCode e)
    {
        var combo = Combos.FirstOrDefault(x => x.CanTrigger(_keyboardService.KeysDown));
        if (combo is not null)
        {
            var delay = _settingsService.GetKeyPressDelay();
            await _keyboardService.InputKeys(StratagemCollection.GetStratagemKeyCombo(combo.Stratagem), delay);
        }
    }
}
