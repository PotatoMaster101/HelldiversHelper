using System.ComponentModel;
using System.Windows;
using HelldiversHelper.Services;
using HelldiversHelper.ViewModels;
using NSubstitute;
using WindowsInput.Events;
using Xunit;

namespace HelldiversHelper.Test.ViewModels;

/// <summary>
/// Unit tests for <see cref="AddComboViewModel"/>.
/// </summary>
public class AddComboViewModelTest
{
    private readonly IKeyboardService _keyboardService = Substitute.For<IKeyboardService>();
    private readonly IViewCreator _viewCreator = Substitute.For<IViewCreator>();

    [Fact]
    public void Stratagems_NotEmpty()
    {
        // arrange
        var vm = new AddComboViewModel(_keyboardService, _viewCreator);

        // act
        var result = vm.Stratagems;

        // assert
        Assert.True(result.Count > 0);
    }

    [Theory]
    [InlineData(KeyCode.A, "A")]
    [InlineData(KeyCode.Z, "Z")]
    public void TriggerKeyButtonText_GetsCorrectValue(KeyCode triggerKey, string expected)
    {
        // arrange
        var vm = new AddComboViewModel(_keyboardService, _viewCreator)
        {
            TriggerKey = triggerKey
        };

        // act
        var result = vm.TriggerKeyButtonText;

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(KeyCode.A)]
    [InlineData(KeyCode.Z)]
    [InlineData(KeyCode.None)]
    public void TriggerKey_GetsAndSetsCorrectValue(KeyCode triggerKey)
    {
        // arrange
        var vm = new AddComboViewModel(_keyboardService, _viewCreator);

        // act
        vm.TriggerKey = triggerKey;

        // assert
        Assert.Equal(triggerKey, vm.TriggerKey);
    }

    [Theory]
    [InlineData(KeyCode.LShift, true)]
    [InlineData(KeyCode.LControl, true)]
    [InlineData(KeyCode.None, false)]
    public void CanRemoveModifier_GetsCorrectValue(KeyCode selectedKey, bool expected)
    {
        // arrange
        var vm = new AddComboViewModel(_keyboardService, _viewCreator)
        {
            SelectedModifierKey = selectedKey
        };

        // act
        var result = vm.CanRemoveModifier;

        // assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("Resupply")]
    [InlineData("")]
    [InlineData(null)]
    public void SelectedStratagem_GetsAndSetsCorrectValue(string stratagem)
    {
        // arrange
        var vm = new AddComboViewModel(_keyboardService, _viewCreator);

        // act
        vm.SelectedStratagem = stratagem;

        // assert
        Assert.Equal(stratagem, vm.SelectedStratagem);
    }

    [Theory]
    [InlineData(KeyCode.A)]
    [InlineData(KeyCode.LShift)]
    [InlineData(KeyCode.None)]
    public void SelectedModifierKey_GetsAndSetsCorrectValue(KeyCode modifierKey)
    {
        // arrange
        var vm = new AddComboViewModel(_keyboardService, _viewCreator);

        // act
        vm.SelectedModifierKey = modifierKey;

        // assert
        Assert.Equal(modifierKey, vm.SelectedModifierKey);
    }

    [Theory]
    [InlineData("text")]
    [InlineData("")]
    [InlineData(null)]
    public void AddModifierButtonText_GetsAndSetsCorrectValue(string text)
    {
        // arrange
        var vm = new AddComboViewModel(_keyboardService, _viewCreator);

        // act
        vm.AddModifierButtonText = text;

        // assert
        Assert.Equal(text, vm.AddModifierButtonText);
    }

    [Fact]
    public void GetCombo_ReturnsCorrectValue()
    {
        // arrange
        var vm = new AddComboViewModel(_keyboardService, _viewCreator)
        {
            TriggerKey = KeyCode.X,
            SelectedStratagem = "REP-80"
        };
        vm.ModifierKeys.Add(KeyCode.LControl);
        vm.ModifierKeys.Add(KeyCode.LShift);

        // act
        var result = vm.GetCombo();

        // assert
        Assert.Equal("REP-80", result.Stratagem);
        Assert.Equal(KeyCode.X, result.TriggerKey);
        Assert.NotNull(result.ModifierKeys);
        Assert.Equal(2, result.ModifierKeys.Count);
        Assert.Equal(KeyCode.LControl, result.ModifierKeys[0]);
        Assert.Equal(KeyCode.LShift, result.ModifierKeys[1]);
    }

    [Fact]
    public void OnClosing_ShowsWarningWhenModifyingKey()
    {
        // arrange
        var vm = new AddComboViewModel(_keyboardService, _viewCreator);
        var arg = new CancelEventArgs();
        vm.ModifyTriggerKeyCommand.Execute(null);

        // act
        vm.OnClosing(arg);

        // assert
        _viewCreator.ReceivedWithAnyArgs().ShowDialog(null!);
        Assert.True(arg.Cancel);
    }

    [Fact]
    public void OnClosing_ClosesWhenNotModifyingKey()
    {
        // arrange
        var vm = new AddComboViewModel(_keyboardService, _viewCreator);
        var arg = new CancelEventArgs();

        // act
        vm.OnClosing(arg);

        // assert
        Assert.False(arg.Cancel);
    }

    [Fact]
    public void ModifyTriggerKeyCommand_DisablesCanReadKey()
    {
        // arrange
        var vm = new AddComboViewModel(_keyboardService, _viewCreator);

        // act
        vm.ModifyTriggerKeyCommand.Execute(null);

        // assert
        Assert.False(vm.CanReadKey);
    }

    [Fact]
    public void AddModifierKeyCommand_DisablesCanReadKey()
    {
        // arrange
        var vm = new AddComboViewModel(_keyboardService, _viewCreator);

        // act
        vm.AddModifierKeyCommand.Execute(null);

        // assert
        Assert.False(vm.CanReadKey);
    }

    [Fact]
    public void OnRemoveModifierKey_RemovesModifier_SelectsFirstModifier()
    {
        // arrange
        var vm = new AddComboViewModel(_keyboardService, _viewCreator);
        vm.ModifierKeys.Add(KeyCode.LControl);
        vm.ModifierKeys.Add(KeyCode.LShift);
        vm.SelectedModifierKey = KeyCode.LShift;

        // act
        vm.RemoveModifierKeyCommand.Execute(null);

        // assert
        Assert.Equal(KeyCode.LControl, vm.SelectedModifierKey);
        Assert.Single(vm.ModifierKeys);
        Assert.Equal(KeyCode.LControl, vm.ModifierKeys[0]);
    }

    [Theory]
    [InlineData(KeyCode.A)]
    [InlineData(KeyCode.LControl)]
    [InlineData(KeyCode.None)]
    public void OnTriggerKeyDown_SetsTriggerKey(KeyCode triggerKey)
    {
        // arrange
        var vm = new AddComboViewModel(_keyboardService, _viewCreator);
        vm.ModifyTriggerKeyCommand.Execute(null);

        // act
        _keyboardService.KeyDown += Raise.Event<EventHandler<KeyCode>>(this, triggerKey);

        // assert
        Assert.Equal(triggerKey, vm.TriggerKey);
    }

    [Theory]
    [InlineData(KeyCode.A, false)]
    [InlineData(KeyCode.LControl, true)]
    public void OnModifierKeyDown_SetsModifierKey(KeyCode modifierKey, bool added)
    {
        // arrange
        _keyboardService.IsModifier(modifierKey).Returns(added);
        var vm = new AddComboViewModel(_keyboardService, _viewCreator);
        vm.AddModifierKeyCommand.Execute(null);

        if (Application.Current == null)    // fix dispatcher issue
            _ = new Application { ShutdownMode = ShutdownMode.OnExplicitShutdown };

        // act
        _keyboardService.KeyDown += Raise.Event<EventHandler<KeyCode>>(this, modifierKey);

        // assert
        if (added)
        {
            Assert.Single(vm.ModifierKeys);
            Assert.Equal(modifierKey, vm.ModifierKeys[0]);
        }
        else
        {
            Assert.True(vm.ModifierKeys.Count == 0);
        }
    }

    [Fact]
    public void OnModifierKeyDown_DoesNotAddDuplicate()
    {
        // arrange
        _keyboardService.IsModifier(KeyCode.LShift).Returns(true);
        var vm = new AddComboViewModel(_keyboardService, _viewCreator);

        if (Application.Current == null)    // fix dispatcher issue
            _ = new Application { ShutdownMode = ShutdownMode.OnExplicitShutdown };

        // act
        vm.AddModifierKeyCommand.Execute(null);
        _keyboardService.KeyDown += Raise.Event<EventHandler<KeyCode>>(this, KeyCode.LShift);
        vm.AddModifierKeyCommand.Execute(null);
        _keyboardService.KeyDown += Raise.Event<EventHandler<KeyCode>>(this, KeyCode.LShift);

        // assert
        Assert.Single(vm.ModifierKeys);
        Assert.Equal(KeyCode.LShift, vm.ModifierKeys[0]);
    }
}
