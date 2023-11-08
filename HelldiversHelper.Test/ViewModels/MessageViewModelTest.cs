using HelldiversHelper.ViewModels;
using Xunit;

namespace HelldiversHelper.Test.ViewModels;

/// <summary>
/// Unit tests for <see cref="MessageViewModel"/>.
/// </summary>
public class MessageViewModelTest
{
    [Theory]
    [InlineData("message")]
    [InlineData("")]
    [InlineData(null)]
    public void Message_GetsAndSetsCorrectValue(string value)
    {
        // arrange
        var vm = new MessageViewModel
        {
            Message = string.Empty
        };

        // act
        vm.Message = value;

        // assert
        Assert.Equal(value, vm.Message);
    }
}
