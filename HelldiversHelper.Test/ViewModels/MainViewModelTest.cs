using HelldiversHelper.Models;
using HelldiversHelper.Services;
using HelldiversHelper.Settings;
using HelldiversHelper.ViewModels;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using WindowsInput.Events;
using Xunit;

namespace HelldiversHelper.Test.ViewModels;

/// <summary>
/// Unit tests for <see cref="MainViewModel"/>.
/// </summary>
public class MainViewModelTest
{
    private readonly IImportExportService _importExportService = Substitute.For<IImportExportService>();
    private readonly IKeyboardService _keyboardService = Substitute.For<IKeyboardService>();
    private readonly ISettingsService _settingsService = Substitute.For<ISettingsService>();
    private readonly IViewCreator _viewCreator = Substitute.For<IViewCreator>();

    [Fact]
    public void AddComboCommand_AddsCombo()
    {
        // arrange
        _viewCreator.ShowDialog(null!).ReturnsForAnyArgs(true);
        var vm = new MainViewModel(_settingsService, _viewCreator, _keyboardService, _importExportService);

        // act
        vm.AddComboCommand.Execute(null);

        // assert
        Assert.Single(vm.Combos);
    }

    [Fact]
    public void AddComboCommand_DoesNothingOnCancel()
    {
        // arrange
        _viewCreator.ShowDialog(null!).ReturnsForAnyArgs(false);
        var vm = new MainViewModel(_settingsService, _viewCreator, _keyboardService, _importExportService);

        // act
        vm.AddComboCommand.Execute(null);

        // assert
        Assert.Empty(vm.Combos);
    }

    [Fact]
    public void AddComboCommand_DoesNotAddDuplicateTrigger()
    {
        // arrange
        _viewCreator.ShowDialog(null!).ReturnsForAnyArgs(true);
        var vm = new MainViewModel(_settingsService, _viewCreator, _keyboardService, _importExportService);

        // act
        vm.AddComboCommand.Execute(null);
        vm.AddComboCommand.Execute(null);

        // assert
        Assert.Single(vm.Combos);
    }

    [Theory]
    [InlineData("EN", SupportedCulture.Default)]
    [InlineData("ZH", SupportedCulture.Chinese)]
    public void SetLanguageCommand_SetsLanguage(string lang, SupportedCulture culture)
    {
        // arrange
        var vm = new MainViewModel(_settingsService, _viewCreator, _keyboardService, _importExportService);

        // act
        vm.SetLanguageCommand.Execute(lang);

        // assert
        _settingsService.Received().SetCulture(culture);
        _viewCreator.ReceivedWithAnyArgs().ShowDialog(null!);
    }

    [Fact]
    public async Task ImportCombosCommand_ImportsCombo()
    {
        // arrange
        const string filename = "test.json";
        _importExportService.OpenDialog(true).Returns(filename);
        _importExportService.ImportCombos(filename).Returns(new[] { new Combo { Stratagem = string.Empty } });
        var vm = new MainViewModel(_settingsService, _viewCreator, _keyboardService, _importExportService);

        // act
        await vm.ImportCombosCommand.ExecuteAsync(null);

        // assert
        Assert.Single(vm.Combos);
    }

    [Fact]
    public async Task ImportCombosCommand_ShowsErrorMessage()
    {
        // arrange
        const string filename = "test.json";
        _importExportService.OpenDialog(true).Returns(filename);
        _importExportService.ImportCombos(filename).Throws(new Exception());
        var vm = new MainViewModel(_settingsService, _viewCreator, _keyboardService, _importExportService);

        // act
        await vm.ImportCombosCommand.ExecuteAsync(null);

        // assert
        Assert.Empty(vm.Combos);
        _viewCreator.ReceivedWithAnyArgs().ShowDialog(null!);
    }

    [Fact]
    public async Task ImportCombosCommand_DoesNotImportOnCancel()
    {
        // arrange
        _importExportService.OpenDialog(true).Returns(string.Empty);
        var vm = new MainViewModel(_settingsService, _viewCreator, _keyboardService, _importExportService);

        // act
        await vm.ImportCombosCommand.ExecuteAsync(null);

        // assert
        Assert.Empty(vm.Combos);
        _viewCreator.DidNotReceiveWithAnyArgs().ShowDialog(null!);
    }

    [Theory]
    [InlineData(Constants.MinimumKeyPressDelay)]
    [InlineData(int.MaxValue)]
    public void SetKeyPressDelayCommand_SetKeyPressDelay(int delay)
    {
        // arrange
        _viewCreator.ShowDialog(Arg.Any<KeyPressDelayViewModel>()).Returns(x =>
        {
            x.ArgAt<KeyPressDelayViewModel>(0).Delay = delay;
            return true;
        });

        var vm = new MainViewModel(_settingsService, _viewCreator, _keyboardService, _importExportService);

        // act
        vm.SetKeyPressDelayCommand.Execute(null);

        // assert
        _settingsService.Received().SetKeyPressDelay(delay);
    }

    [Fact]
    public void SetKeyPressDelayCommand_DoesNothingOnCancel()
    {
        // arrange
        _viewCreator.ShowDialog(null!).ReturnsForAnyArgs(false);
        var vm = new MainViewModel(_settingsService, _viewCreator, _keyboardService, _importExportService);

        // act
        vm.SetKeyPressDelayCommand.Execute(null);

        // assert
        _settingsService.DidNotReceiveWithAnyArgs().SetKeyPressDelay(0);
    }

    [Fact]
    public async Task OnExportCombos_ExportsCombo()
    {
        // arrange
        const string filename = "test.json";
        _importExportService.OpenDialog(false).Returns(filename);
        var vm = new MainViewModel(_settingsService, _viewCreator, _keyboardService, _importExportService);
        vm.Combos.Add(new Combo { Stratagem = string.Empty });

        // act
        await vm.ExportCombosCommand.ExecuteAsync(null);

        // assert
        await _importExportService.Received().ExportCombos(vm.Combos, filename);
    }

    [Fact]
    public async Task OnExportCombos_DoesNotExportOnCancel()
    {
        // arrange
        _importExportService.OpenDialog(false).Returns(string.Empty);
        var vm = new MainViewModel(_settingsService, _viewCreator, _keyboardService, _importExportService);
        vm.Combos.Add(new Combo { Stratagem = string.Empty });

        // act
        await vm.ExportCombosCommand.ExecuteAsync(null);

        // assert
        await _importExportService.DidNotReceiveWithAnyArgs().ExportCombos(null!, null!);
    }

    [Fact]
    public void OnKeyDown_TriggersCombo()
    {
        // arrange
        _keyboardService.KeysDown.Returns(new HashSet<KeyCode> { KeyCode.Z });
        var vm = new MainViewModel(_settingsService, _viewCreator, _keyboardService, _importExportService);
        vm.Combos.Add(new Combo { Stratagem = string.Empty, TriggerKey = KeyCode.Z });

        // act
        _keyboardService.KeyDown += Raise.Event<EventHandler<KeyCode>>(this, KeyCode.Z);

        // assert
        _keyboardService.ReceivedWithAnyArgs().InputKeys(null!, 0);
    }

    [Fact]
    public void OnKeyDown_DoesNothingWhenNoTrigger()
    {
        // arrange
        _keyboardService.KeysDown.Returns(new HashSet<KeyCode> { KeyCode.Z });
        var vm = new MainViewModel(_settingsService, _viewCreator, _keyboardService, _importExportService);
        vm.Combos.Add(new Combo { Stratagem = string.Empty, TriggerKey = KeyCode.X });

        // act
        _keyboardService.KeyDown += Raise.Event<EventHandler<KeyCode>>(this, KeyCode.Z);

        // assert
        _keyboardService.DidNotReceiveWithAnyArgs().InputKeys(null!, 0);
    }

    [Fact]
    public void OnContentRendered_ResetsCulture()
    {
        // arrange
        var vm = new MainViewModel(_settingsService, _viewCreator, _keyboardService, _importExportService);

        // act
        vm.OnContentRendered();

        // assert
        _settingsService.Received().ResetThreadCulture();
    }
}
