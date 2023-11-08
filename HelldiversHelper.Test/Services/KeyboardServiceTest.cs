using HelldiversHelper.Services;
using WindowsInput.Events;
using Xunit;

namespace HelldiversHelper.Test.Services;

/// <summary>
/// Unit tests for <see cref="KeyboardService"/>.
/// </summary>
public class KeyboardServiceTest
{
    [Theory]
    [InlineData(KeyCode.A, false)]
    [InlineData(KeyCode.Z, false)]
    [InlineData(KeyCode.LControl, true)]
    [InlineData(KeyCode.LShift, true)]
    [InlineData(KeyCode.LAlt, true)]
    public void IsModifier_ReturnsCorrectValue(KeyCode key, bool expected)
    {
        // arrange
        using var srv = new KeyboardService();

        // act
        var result = srv.IsModifier(key);

        // assert
        Assert.Equal(expected, result);
    }
}
