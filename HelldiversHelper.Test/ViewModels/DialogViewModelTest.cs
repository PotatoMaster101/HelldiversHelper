using CommunityToolkit.Mvvm.Messaging;
using HelldiversHelper.Message;
using HelldiversHelper.ViewModels;
using Xunit;

// ReSharper disable ParameterOnlyUsedForPreconditionCheck.Local
namespace HelldiversHelper.Test.ViewModels;

/// <summary>
/// Unit tests for <see cref="DialogViewModel"/>.
/// </summary>
[Collection("HasMessaging")]
public class DialogViewModelTest
{
    [Fact]
    public void OnOk_ClosesWindow_SetsTrueResult()
    {
        // arrange
        var vm = new DialogViewModel();

        WeakReferenceMessenger.Default.Register<DialogViewModel, CloseViewMessage>(vm, (r, m) =>
        {
            // assert
            Assert.Equal(vm, r);
            Assert.Equal(vm, m.DataContext);
            Assert.Equal(true, m.DialogResult);

            WeakReferenceMessenger.Default.UnregisterAll(vm);
        });

        // act
        vm.OkCommand.Execute(null);
    }

    [Fact]
    public void OnCancel_ClosesWindow_SetsFalseResult()
    {
        // arrange
        var vm = new DialogViewModel();

        WeakReferenceMessenger.Default.Register<DialogViewModel, CloseViewMessage>(vm, (r, m) =>
        {
            // assert
            Assert.Equal(vm, r);
            Assert.Equal(vm, m.DataContext);
            Assert.Equal(false, m.DialogResult);

            WeakReferenceMessenger.Default.UnregisterAll(vm);
        });

        // act
        vm.CancelCommand.Execute(null);
    }

    [Theory]
    [InlineData("ok")]
    [InlineData("")]
    [InlineData(null)]
    public void OkButtonText_GetsAndSetsCorrectValue(string value)
    {
        // arrange
        var vm = new DialogViewModel();

        // act
        vm.OkButtonText = value;

        // assert
        Assert.Equal(value, vm.OkButtonText);
    }

    [Theory]
    [InlineData("ok")]
    [InlineData("")]
    [InlineData(null)]
    public void CancelButtonText_GetsAndSetsCorrectValue(string value)
    {
        // arrange
        var vm = new DialogViewModel();

        // act
        vm.CancelButtonText = value;

        // assert
        Assert.Equal(value, vm.CancelButtonText);
    }


    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void ShowCancelButton_GetsAndSetsCorrectValue(bool value)
    {
        // arrange
        var vm = new DialogViewModel();

        // act
        vm.ShowCancelButton = value;

        // assert
        Assert.Equal(value, vm.ShowCancelButton);
    }
}
