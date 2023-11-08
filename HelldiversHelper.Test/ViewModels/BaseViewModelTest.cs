using CommunityToolkit.Mvvm.Messaging;
using HelldiversHelper.Message;
using HelldiversHelper.ViewModels;
using Xunit;

// ReSharper disable ParameterOnlyUsedForPreconditionCheck.Local
namespace HelldiversHelper.Test.ViewModels;

/// <summary>
/// Unit tests for <see cref="BaseViewModel"/>.
/// </summary>
[Collection("HasMessaging")]
public class BaseViewModelTest
{
    [Theory]
    [InlineData("test")]
    public void OnPropertyChanged_InvokesPropertyChanged(string propertyName)
    {
        // arrange
        var vm = new ViewModel();

        vm.PropertyChanged += (sender, args) =>
        {
            // assert
            Assert.Equal(vm, sender);
            Assert.Equal(propertyName, args.PropertyName);
        };

        // act
        vm.CallOnPropertyChanged(propertyName);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    [InlineData(null)]
    public void Close_SendsCloseMessage(bool? dialogResult)
    {
        // arrange
        var vm = new ViewModel();

        WeakReferenceMessenger.Default.Register<ViewModel, CloseViewMessage>(vm, (r, m) =>
        {
            // assert
            Assert.Equal(vm, r);
            Assert.Equal(vm, m.DataContext);
            Assert.Equal(dialogResult, m.DialogResult);

            WeakReferenceMessenger.Default.UnregisterAll(vm);
        });

        // act
        vm.Close(dialogResult);
    }

    [Theory]
    [InlineData("title")]
    [InlineData("")]
    [InlineData(null)]
    public void Title_GetsAndSetsCorrectValue(string title)
    {
        // arrange
        var vm = new ViewModel();

        // act
        vm.Title = title;

        // assert
        Assert.Equal(title, vm.Title);
    }

    private class ViewModel : BaseViewModel
    {
        public void CallOnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(propertyName);
        }
    }
}
