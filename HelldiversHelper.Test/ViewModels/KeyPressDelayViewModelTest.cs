using HelldiversHelper.Services;
using HelldiversHelper.ViewModels;
using NSubstitute;
using Xunit;

namespace HelldiversHelper.Test.ViewModels;

/// <summary>
/// Unit tests for <see cref="KeyPressDelayViewModel"/>.
/// </summary>
public class KeyPressDelayViewModelTest
{
    private readonly IViewCreator _viewCreator = Substitute.For<IViewCreator>();

    [Theory]
    [InlineData(Constants.MinimumKeyPressDelay)]
    [InlineData(int.MaxValue)]
    public void Delay_GetsAndSetsCorrectValue(int delay)
    {
        // arrange
        var vm = new KeyPressDelayViewModel(_viewCreator, delay);

        // act
        vm.Delay = delay;

        // assert
        Assert.Equal(delay, vm.Delay);
    }

    [Theory]
    [InlineData(Constants.MinimumKeyPressDelay - 1, Constants.MinimumKeyPressDelay)]
    [InlineData(0, Constants.MinimumKeyPressDelay)]
    [InlineData(-1, Constants.MinimumKeyPressDelay)]
    public void OnOk_ShowsErrorWhenDelayOutOfRange(int delay, int minDelay)
    {
        // arrange
        var vm = new KeyPressDelayViewModel(_viewCreator, delay, minDelay);

        // act
        vm.OkCommand.Execute(null);

        // assert
        _viewCreator.ReceivedWithAnyArgs().ShowDialog(null!);
    }

    [Theory]
    [InlineData(Constants.MinimumKeyPressDelay, Constants.MinimumKeyPressDelay)]
    [InlineData(int.MaxValue, Constants.MinimumKeyPressDelay)]
    public void OnOk_DoesNotShowErrorOnGoodDelay(int delay, int minDelay)
    {
        // arrange
        var vm = new KeyPressDelayViewModel(_viewCreator, delay, minDelay);

        // act
        vm.OkCommand.Execute(null);

        // assert
        _viewCreator.DidNotReceiveWithAnyArgs().ShowDialog(null!);
    }
}
